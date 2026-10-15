using UnityEngine;

namespace MaterialShift
{
    [CreateAssetMenu(fileName = "BallMaterialData", menuName = "Material Shift/Ball Material Data")]
    public class Ball_Material_Data : ScriptableObject
    {
        [SerializeField] private Material_Type materialType;
        [SerializeField] private PhysicsMaterial2D physicsMaterial;
        [SerializeField] private Sprite sprite;

        [Min(0f)]
        [SerializeField] private float movementMultiplier = 1f;

        [Min(0f)]
        [SerializeField] private float maxSpeedMultiplier = 1f;

        [Min(0f)]
        [SerializeField] private float accelerationMultiplier = 1f;

        [Min(0f)]
        [SerializeField] private float decelerationMultiplier = 1f;

        [Min(0f)]
        [SerializeField] private float jumpMultiplier = 1f;

        public Material_Type MaterialType => materialType;
        public PhysicsMaterial2D PhysicsMaterial => physicsMaterial;
        public Sprite Sprite => sprite;
        public float MovementMultiplier => movementMultiplier;
        public float MaxSpeedMultiplier => maxSpeedMultiplier;
        public float AccelerationMultiplier => accelerationMultiplier;
        public float DecelerationMultiplier => decelerationMultiplier;
        public float JumpMultiplier => jumpMultiplier;
    }
}
