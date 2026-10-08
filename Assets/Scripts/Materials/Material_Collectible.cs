using UnityEngine;

namespace MaterialShift
{
    public class Material_Collectible : MonoBehaviour
    {
        [SerializeField] private Material_Type materialType;

        private bool isCollected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isCollected || !other.CompareTag("Player"))
            {
                return;
            }

            Ball_Material_Controller ballMaterialController =
                other.GetComponent<Ball_Material_Controller>();

            if (ballMaterialController == null)
            {
                return;
            }

            ballMaterialController.ChangeMaterial(materialType);

            isCollected = true;
            gameObject.SetActive(false);
        }
    }
}
