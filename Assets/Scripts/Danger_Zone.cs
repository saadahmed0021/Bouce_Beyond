using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Danger_Zone : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;

    private Vector2 respawnPosition;
    private bool isRespawning;

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Start()
    {
        if (playerRigidbody == null)
        {
            Debug.LogError("Danger_Zone requires the player's Rigidbody2D reference.", this);
            return;
        }

        respawnPosition = playerRigidbody.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isRespawning || playerRigidbody == null || other.attachedRigidbody != playerRigidbody)
        {
            return;
        }

        isRespawning = true;
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        playerRigidbody.linearVelocity = Vector2.zero;
        playerRigidbody.position = respawnPosition;

        yield return new WaitForFixedUpdate();

        isRespawning = false;
    }

    public void SetRespawnPosition(Vector2 position)
    {
        respawnPosition = position;
    }
}