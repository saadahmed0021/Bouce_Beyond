using System.Collections;
using UnityEngine;

namespace MaterialShift
{
    [RequireComponent(typeof(Collider2D))]
    public class Wall_Rock_Fall : MonoBehaviour
    {
        [Header("Rock Settings")]
        [SerializeField] private GameObject rockPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField, Min(1)] private int minimumRocks = 3;
        [SerializeField, Min(1)] private int maximumRocks = 4;
        [SerializeField, Min(0f)] private float spawnDelay = 0.01f;

        [Header("Impact Settings")]
        [SerializeField, Min(0f)] private float minimumImpactSpeed = 3f;

        private bool isSpawning;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.relativeVelocity.magnitude < minimumImpactSpeed)
            {
                return;
            }

            Ball_Material_Controller materialController =
                collision.collider.GetComponent<Ball_Material_Controller>();

            if (materialController == null)
            {
                materialController =
                    collision.collider.GetComponentInParent<Ball_Material_Controller>();
            }

            if (materialController == null || isSpawning)
            {
                return;
            }

            if (rockPrefab == null || spawnPoint == null)
            {
                Debug.LogWarning(
                    "Wall_Rock_Fall: Assign the rock prefab and spawn point.",
                    this);
                return;
            }

            StartCoroutine(SpawnRocks());
        }

        private IEnumerator SpawnRocks()
        {
            isSpawning = true;

            int rockCount = Random.Range(minimumRocks, maximumRocks + 1);

            for (int i = 0; i < rockCount; i++)
            {
                Instantiate(
                    rockPrefab,
                    spawnPoint.position,
                    spawnPoint.rotation);

                if (i < rockCount - 1 && spawnDelay > 0f)
                {
                    yield return new WaitForSeconds(spawnDelay);
                }
            }

            isSpawning = false;
        }

        private void OnValidate()
        {
            minimumRocks = Mathf.Max(1, minimumRocks);
            maximumRocks = Mathf.Max(minimumRocks, maximumRocks);
            spawnDelay = Mathf.Max(0f, spawnDelay);
        }
    }
}