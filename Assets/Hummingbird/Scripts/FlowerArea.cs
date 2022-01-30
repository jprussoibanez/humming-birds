using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a collection of flower plants.
/// </summary>
public class FlowerArea : MonoBehaviour
{
    // Largest distance on the island from agent to flower.
    public const float AreaDiameter = 20f;

    // List of plants on the area or cluster.
    private List<GameObject> flowerPlants;

    // Map between nectars to flower.
    private Dictionary<Collider, Flower> nectarFlowerDictionary;

    public List<Flower> Flowers
    {
        get; private set;
    }

    /// <summary>
    /// Reset all flowers in the area with random rotations.
    /// </summary>
    public void ResetFlowers()
    {
        foreach (GameObject flowerPlant in flowerPlants)
        {
            float xRotation = UnityEngine.Random.Range(-5f, 5f);
            float yRotation = UnityEngine.Random.Range(-180f, 180f);
            float zRotation = UnityEngine.Random.Range(-5f, 5f);
            flowerPlant.transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        foreach (Flower flower in Flowers)
        {
            flower.ResetFlower();
        }
    }

    /// <summary>
    /// Get <see cref="Flower"> matching with the nectar.
    /// </summary>
    /// <param name="nectar">Nectar collider</param>
    /// <returns>Matching flower</returns>
    public Flower GetFlowerFromNectar(Collider nectar)
    {
        return nectarFlowerDictionary[nectar];
    }

    /// <summary>
    /// Called from Unity engine when flower area wakes up.
    /// </summary>
    private void Awake()
    {
        flowerPlants = new List<GameObject>();
        nectarFlowerDictionary = new Dictionary<Collider, Flower>();
        Flowers = new List<Flower>();
    }

    /// <summary>
    /// Called from Unity engine when game starts.
    /// </summary>
    private void Start()
    {
        FindChildFlowers(transform);
    }

    private void FindChildFlowers(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (child.CompareTag("flower_plant"))
            {
                flowerPlants.Add(child.gameObject);

                FindChildFlowers(child);
            }
            else
            {
                Flower flower = child.GetComponent<Flower>();
                if (flower != null)
                {
                    Flowers.Add(flower);

                    nectarFlowerDictionary.Add(flower.nectarCollider, flower);

                    //Note: No flower should contain flowers as children
                }
                else
                {
                    FindChildFlowers(child);
                }
            }
        }
    }
}
