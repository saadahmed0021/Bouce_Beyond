using System.Collections.Generic;
using UnityEngine;
using MaterialShift;

[RequireComponent(typeof(BoxCollider2D))]
public class Pressure_Plate : MonoBehaviour
{
    [SerializeField] private Bridge_Controller bridgeController;
    [SerializeField] private Material_Type requiredMaterial = Material_Type.Rock;

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

        UpdatePlayerCollider(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        UpdatePlayerCollider(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        SetPlayerColliderEligible(other, false);
    }

    private void UpdatePlayerCollider(Collider2D other)
    {
        Ball_Material_Controller materialController =
            other.GetComponent<Ball_Material_Controller>();

        if (materialController == null)
        {
            materialController = other.GetComponentInParent<Ball_Material_Controller>();
        }

        bool hasRequiredMaterial = materialController != null &&
            materialController.CurrentMaterialType == requiredMaterial;

        SetPlayerColliderEligible(other, hasRequiredMaterial);
    }

    private void SetPlayerColliderEligible(Collider2D playerCollider, bool eligible)
    {
        bool wasOccupied = playerColliders.Count > 0;

        if (eligible)
        {
            playerColliders.Add(playerCollider);
        }
        else
        {
            playerColliders.Remove(playerCollider);
        }

        bool isOccupied = playerColliders.Count > 0;

        if (wasOccupied != isOccupied)
        {
            bridgeController?.SetPlateOccupied(isOccupied);
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