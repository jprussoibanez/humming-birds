using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using System;
using Unity.MLAgents.Sensors;

/// <summary>
/// Hummingbird machine learning agent.
/// </summary>
public class HummingbirdAgent : Agent
{
    [Tooltip("Force to apply when moving")]
    public float moveForce = 2f;

    [Tooltip("Speed to pitch up or down")]
    public float pitchSpeed = 100f;

    [Tooltip("Speed to rotate around the up axis")]
    public float yawSpeed = 100f;

    [Tooltip("Transform at the tip of the beak")]
    public Transform beakTip;

    [Tooltip("The agent's camera")]
    public Camera agentCamera;

    [Tooltip("Wether we are training or gameplay mode")]
    public bool trainingMode;

    // The rigid body of the agent.
    new private Rigidbody rigidbody;

    // The flow are that the agent is in.
    private FlowerArea flowerArea;

    // The nearest flower to the agent.
    private Flower nearestFlower;

    // Allows for smoother pitch changes.
    private float smoothPitchChange = 0f;

    // Allows for smoother yaw changes.
    private float smoothYawChange = 0f;

    // Maximum angle that the bird can pitch up or down.
    private const float MaxPitchAngle = 80f;

    // Maximum distance from the beak tip to accept collision.
    private const float BeakTipRadius = 0.008f;

    // Wether the agent is frozen (intentionally not flying)
    private bool frozen = false;

    /// <summary>
    /// Amount of nectar obtained during the current episode.
    /// </summary>
    public float NectarObtained { get; private set; }

    /// <summary>
    /// Initialize agent.
    /// </summary>
    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();
        flowerArea = GetComponentInParent<FlowerArea>();

        if (!trainingMode) MaxStep = 0;
    }

    public override void OnEpisodeBegin()
    {
        if (trainingMode)
        {
            flowerArea.ResetFlowers();
        }

        NectarObtained = 0;

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        bool inFrontOfFlower = true;
        if (trainingMode)
        {
            // Spawn 50% chance to start or not in front of flower
            inFrontOfFlower = UnityEngine.Random.value > .5f;
        }

        MoveToSafeRandomPosition(inFrontOfFlower);

        UpdateNearestFlower();
    }

    /// <summary>
    /// Called when and action is received from either the player input or the neural network
    /// 
    /// vectorAction[i] represents:
    /// Index 0: move vector x (+1 = right, -1 = left)
    /// Index 1: move vector y (+1 = up, -1 = down)
    /// Index 2: move vector z (+1 = forward, -1 = backward)
    /// Index 3: pitch angle (+1 = pitch up, -1 = pitch down)
    /// Index 4: yaw angle (+1 = turn right, -1 = turn left)
    /// </summary>
    /// <param name="vectorAction">The actions to take</param>
    public override void OnActionReceived(float[] vectorAction)
    {
        if (frozen) return;

        // Calculate movement vector
        Vector3 move = new Vector3(vectorAction[0], vectorAction[1], vectorAction[2]);

        // Add force to the movement vector
        rigidbody.AddForce(move * moveForce);

        // Get the current rotation
        Vector3 rotationVector = transform.rotation.eulerAngles;

        // Calculate pitch and yaw rotation
        float pitchChange = vectorAction[3];
        float yawChange = vectorAction[4];

        // Calculate smooth rotation changes
        smoothPitchChange = Mathf.MoveTowards(smoothPitchChange, pitchChange, 2f * Time.fixedDeltaTime);
        smoothYawChange = Mathf.MoveTowards(smoothYawChange, yawChange, 2f * Time.fixedDeltaTime);

        // Calculate new pitch and yaw based on smoothed values
        // Clamp  pitch to avoid flipping upside down
        float pitch = rotationVector.x + smoothPitchChange * Time.fixedDeltaTime * pitchSpeed;
        if (pitch > 180f) pitch -= 360f;
        pitch = Mathf.Clamp(pitch, -MaxPitchAngle, MaxPitchAngle);

        float yaw = rotationVector.y + smoothYawChange * Time.fixedDeltaTime * yawSpeed;

        // Apply the new rotation
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    /// <summary>
    /// Collect agent's observation over time.
    /// </summary>
    /// <param name="sensor">The vector sensor with observations</param>
    public override void CollectObservations(VectorSensor sensor)
    {
        // If nearestFlower is null, observe an empty array and return early
        if (nearestFlower == null)
        {
            sensor.AddObservation(new float[10]);
            return;
        }

        // Observe the agent's local rotation (4 observations)
        sensor.AddObservation(transform.localRotation.normalized);

        // Get a vector from the beak tip to the nearest flower
        Vector3 toFlower = nearestFlower.FlowerCenterPosition - beakTip.position;

        // Observe a normalized vector pointing to the nearest flower (3 observations)
        sensor.AddObservation(toFlower.normalized);

        // Observe a dot product that indicates whether the beak tip is in front of the flower (1 observation)
        // (+1 means that the beak tip is directly in front of the flower, -1 means directly behind)
        sensor.AddObservation(Vector3.Dot(toFlower.normalized, -nearestFlower.FlowerUpVector.normalized));

        // Observe a dot product that indicates whether the beak is pointing toward the flower (1 observation)
        // (+1 means that the beak is pointing directly at the flower, -1 means directly away)
        sensor.AddObservation(Vector3.Dot(beakTip.forward.normalized, -nearestFlower.FlowerUpVector.normalized));

        // Observe the relative distance from the beak tip to the flower (1 observation)
        sensor.AddObservation(toFlower.magnitude / FlowerArea.AreaDiameter);

        // 10 total observations
    }

    /// <summary>
    /// Update nearest flower to agent.
    /// </summary>
    private void UpdateNearestFlower()
    {
        foreach (Flower flower in flowerArea.Flowers)
        {
            if (nearestFlower == null && flower.HasNectar)
            {
                nearestFlower = flower;
            }
            else if (flower.HasNectar)
            {
                float distanceToFlower = Vector3.Distance(flower.transform.position, beakTip.position);
                float distanceToCurrentFlower = Vector3.Distance(nearestFlower.transform.position, beakTip.position);
                if (!nearestFlower.HasNectar || distanceToFlower < distanceToCurrentFlower)
                {
                    nearestFlower = flower;
                }
            }
        }
    }

    /// <summary>
    /// Move the agent to a random position.
    /// If agent in front of flower, then also point beak to nectar.
    /// </summary>
    /// <param name="inFrontOfFlower">Wether to choose a position in front of a flower</param>
    private void MoveToSafeRandomPosition(bool inFrontOfFlower)
    {
        bool safePositionFound = false;
        int attemptsRemaining = 100;

        Vector3 potentialPosition = Vector3.zero;
        Quaternion potentialRotation = new Quaternion();

        while (!safePositionFound && attemptsRemaining > 0)
        {
            attemptsRemaining--;
            if (inFrontOfFlower)
            {
                Flower randomFlower = flowerArea.Flowers[UnityEngine.Random.Range(0, flowerArea.Flowers.Count)];

                float distanceFromFlower = UnityEngine.Random.Range(.1f, .2f);
                potentialPosition = randomFlower.transform.position + randomFlower.FlowerUpVector * distanceFromFlower;

                Vector3 toFlower = randomFlower.FlowerCenterPosition - potentialPosition;
                potentialRotation = Quaternion.LookRotation(toFlower, Vector3.up);
            }
            else
            {
                float height = UnityEngine.Random.Range(1.2f, 2.5f);

                float radius = UnityEngine.Random.Range(2f, 7f);

                Quaternion direction = Quaternion.Euler(0f, UnityEngine.Random.Range(-180f, 180f), 0f);

                potentialPosition = flowerArea.transform.position + Vector3.up * height + direction * Vector3.forward * radius;

                float pitch = UnityEngine.Random.Range(-60f, 60f);
                float yaw = UnityEngine.Random.Range(-180f, 180f);
                potentialRotation = Quaternion.Euler(pitch, yaw, 0f);
            }

            // Calculate if potential position is safe
            Collider[] colliders = Physics.OverlapSphere(potentialPosition, 0.05f);
            safePositionFound = colliders.Length == 0;
        }

        Debug.Assert(safePositionFound, "No safe position found");

        transform.position = potentialPosition;
        transform.rotation = potentialRotation;
    }
}
