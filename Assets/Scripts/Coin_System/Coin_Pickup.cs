using UnityEngine;

namespace MaterialShift
{
    public class Coin_Pickup : MonoBehaviour
    {
        private bool isCollected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isCollected || !other.CompareTag("Player"))
            {
                return;
            }

            Coin_Tracker tracker = Coin_Tracker.Instance;

            if (tracker == null || !tracker.TryCollect(this))
            {
                return;
            }

            isCollected = true;
            gameObject.SetActive(false);
        }

        internal void ResetCollection()
        {
            isCollected = false;
            gameObject.SetActive(true);
        }
    }
}