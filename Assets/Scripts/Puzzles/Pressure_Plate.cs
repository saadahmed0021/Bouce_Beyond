using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Pressure_Plate : MonoBehaviour
{
    [SerializeField] private Bridge_Controller bridgeController;

    private readonly HashSet<Collider2D> playerColliders = new HashSet<Collider2D>();
    private BoxCollider2D plateCollider;

    private void Reset()
    {
        plateCollider = GetComponent<BoxCollider2D>();
        plateCollider.isTrigger = true;
    }

    private void Awake()
    {
        plateCollider = GetComponent<BoxCollider2D>();
        plateCollider.isTrigger = true;

        if (bridgeController == null)
        {
            Debug.LogError("Pressure_Plate requires a Bridge_Controller reference.", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (playerColliders.Add(other) && playerColliders.Count == 1)
        {
            bridgeController?.SetPlateOccupied(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerColliders.Remove(other) && playerColliders.Count == 0)
        {
            bridgeController?.SetPlateOccupied(false);
        }
    }

    private void OnDisable()
    {
        if (playerColliders.Count > 0)
        {
            playerColliders.Clear();
            bridgeController?.SetPlateOccupied(false);
        }
    }
}