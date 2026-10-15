using System.Collections;
using UnityEngine;

namespace MaterialShift
{
    [RequireComponent(typeof(Collider2D))]
    public class Wall_Break : MonoBehaviour
    {
        [SerializeField] private Material_Type requiredMaterial = Material_Type.Rock;
        [SerializeField, Min(1)] private int hitsToBreak = 3;
        [SerializeField, Min(0f)] private float minimumImpactSpeed = 3f;
        [SerializeField, Min(0f)] private float shakeDuration = 0.15f;
        [SerializeField, Min(0f)] private float shakeStrength = 0.08f;
        [SerializeField] private GameObject breakEffect;

        private int currentHits;
        private bool isBroken;
        private Vector3 initialLocalPosition;
        private Coroutine shakeCoroutine;

        private void Awake()
        {
            initialLocalPosition = transform.localPosition;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isBroken || collision.relativeVelocity.magnitude < minimumImpactSpeed)
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

            if (materialController == null ||
                materialController.CurrentMaterialType != requiredMaterial)
            {
                return;
            }

            currentHits++;

            if (currentHits >= hitsToBreak)
            {
                isBroken = true;

                if (breakEffect != null)
                {
                    Instantiate(breakEffect, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
                return;
            }

            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
            }

            shakeCoroutine = StartCoroutine(ShakeWall());
        }

        private IEnumerator ShakeWall()
        {
            float elapsed = 0f;

            while (elapsed < shakeDuration)
            {
                elapsed += Time.deltaTime;

                Vector2 offset = Random.insideUnitCircle * shakeStrength;
                transform.localPosition = initialLocalPosition +
                    new Vector3(offset.x, offset.y, 0f);

                yield return null;
            }

            transform.localPosition = initialLocalPosition;
            shakeCoroutine = null;
        }

        private void OnDisable()
        {
            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
                shakeCoroutine = null;
            }

            transform.localPosition = initialLocalPosition;
        }
    }
}