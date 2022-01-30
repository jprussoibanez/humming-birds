using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a single flower with nectar.
/// <summary>
public class Flower : MonoBehaviour
{
    [Tooltip("Color when the flower is full")]
    public Color fullFlowerColor = new Color(1f, 0f, .3f);

    [Tooltip("Color when the flower is empty")]
    public Color emptyFlowerColor = new Color(.5f, 0f, 1f);

    /// <summary>
    /// The trigger collider representing the nectar.
    /// <summary>
    [HideInInspector]
    public Collider nectarCollider;

    // The solid collider representing the flower petals.
    private Collider flowerCollider;

    // The flower's material.
    private Material flowerMaterial;

    /// <summary>
    /// A vector pointing straight out of the flower.
    /// <summary>
    public Vector3 FlowerUpVector
    {
        get
        {
            return nectarCollider.transform.up;
        }
    }

    /// <summary>
    /// A vector with the flower center position.
    /// <summary>
    public Vector3 FlowerCenterPosition
    {
        get
        {
            return nectarCollider.transform.position;
        }
    }

    /// <summary>
    /// The amount of nectar remaining on the flower.
    /// <summary>
    public float NectarAmount
    {
        get;
        private set;
    }

    /// <summary>
    /// Wether the flower has nectar.
    /// </summary>
    public bool HasNectar
    {
        get
        {
            return NectarAmount > 0f;
        }
    }

    /// <summary>
    /// Attempts to feed on nectar from the flower.
    /// </summary>
    /// <param name="amount">Amount of nectar to remove from the flower</param>
    /// <returns>The actual amount feed from the flower</returns>
    public float Feed(float amount)
    {
        float nectarTaken = Mathf.Clamp(amount, 0f, NectarAmount);

        NectarAmount -= nectarTaken;

        if (NectarAmount <= 0)
        {
            NectarAmount = 0;

            // Simplifies for the hummingbird the flight out.
            flowerCollider.gameObject.SetActive(false);
            nectarCollider.gameObject.SetActive(false);

            // Change flower color to indicate that is empty
            flowerMaterial.SetColor("_BaseColor", emptyFlowerColor);
        }

        return nectarTaken;
    }

    /// <summary>
    /// Reset flower to initial state.
    /// </summary>
    public void ResetFlower()
    {
        NectarAmount = 1f;

        flowerCollider.gameObject.SetActive(true);
        nectarCollider.gameObject.SetActive(true);

        flowerMaterial.SetColor("_BaseColor", fullFlowerColor);
    }

    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        flowerMaterial = meshRenderer.material;

        flowerCollider = transform.Find("FlowerCollider").GetComponent<Collider>();
        nectarCollider = transform.Find("FlowerNectarCollider").GetComponent<Collider>();
    }
}
