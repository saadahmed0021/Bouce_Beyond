using UnityEngine;

namespace MaterialShift
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball_Material_Controller : MonoBehaviour
    {
        [SerializeField] private Ball_Material_Data currentMaterialData;
        [SerializeField] private Ball_Material_Data[] availableMaterialData;

        private CircleCollider2D circleCollider;
        private Rigidbody2D ballRigidbody;
        private SpriteRenderer spriteRenderer;

        public Ball_Material_Data CurrentMaterialData { get; private set; }
        public Material_Type CurrentMaterialType { get; private set; }
        public float CurrentMovementMultiplier => CurrentMaterialData?.MovementMultiplier ?? 1f;
        public float CurrentMaxSpeedMultiplier => CurrentMaterialData?.MaxSpeedMultiplier ?? 1f;
        public float CurrentAccelerationMultiplier => CurrentMaterialData?.AccelerationMultiplier ?? 1f;
        public float CurrentDecelerationMultiplier => CurrentMaterialData?.DecelerationMultiplier ?? 1f;
        public float CurrentJumpMultiplier => CurrentMaterialData?.JumpMultiplier ?? 1f;

        private void Awake()
        {
            ballRigidbody = GetComponent<Rigidbody2D>();
            circleCollider = GetComponent<CircleCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            CurrentMaterialData = currentMaterialData;
            CurrentMaterialType = CurrentMaterialData?.MaterialType ?? default;

            ApplyCurrentMaterial();
        }

        public void ChangeMaterial(Material_Type materialType)
        {
            if (availableMaterialData == null)
            {
                return;
            }

            Ball_Material_Data requestedMaterialData = null;

            for (int index = 0; index < availableMaterialData.Length; index++)
            {
                Ball_Material_Data materialData = availableMaterialData[index];

                if (materialData != null && materialData.MaterialType == materialType)
                {
                    requestedMaterialData = materialData;
                    break;
                }
            }

            if (requestedMaterialData == null || requestedMaterialData == CurrentMaterialData)
            {
                return;
            }

            CurrentMaterialData = requestedMaterialData;
            CurrentMaterialType = requestedMaterialData.MaterialType;
            ApplyCurrentMaterial();
        }

        private void ApplyCurrentMaterial()
        {
            if (CurrentMaterialData == null)
            {
                return;
            }

            if (ballRigidbody != null)
            {
                ballRigidbody.mass = Mathf.Max(0.0001f, CurrentMaterialData.Mass);
            }

            if (circleCollider != null && CurrentMaterialData.PhysicsMaterial != null &&
                circleCollider.sharedMaterial != CurrentMaterialData.PhysicsMaterial)
            {
                circleCollider.sharedMaterial = CurrentMaterialData.PhysicsMaterial;
            }

            if (spriteRenderer != null && CurrentMaterialData.Sprite != null)
            {
                spriteRenderer.sprite = CurrentMaterialData.Sprite;
            }
        }
    }
}
