using System.Collections.Generic;
using UnityEngine;

namespace MaterialShift
{
    public class Coin_Tracker : MonoBehaviour
    {
        public static Coin_Tracker Instance { get; private set; }

        private readonly HashSet<Coin_Pickup> collectedCoins =
            new HashSet<Coin_Pickup>();
        private Coin_Pickup[] sceneCoins = System.Array.Empty<Coin_Pickup>();

        [SerializeField, InspectorName("Collected Coins")]
        private int collectedCoinsDebug;

        [SerializeField, InspectorName("Total Coins")]
        private int totalCoinsDebug;

        public int CollectedCount => collectedCoins.Count;
        public int TotalCount => sceneCoins.Length;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            RefreshSceneCoins();
            SyncDebugCounts();
        }

        public bool TryCollect(Coin_Pickup coin)
        {
            if (coin == null || coin.gameObject.scene != gameObject.scene)
            {
                return false;
            }

            if (!collectedCoins.Add(coin))
            {
                return false;
            }

            SyncDebugCounts();
            return true;
        }

        public void ResetForRetry()
        {
            Coin_Pickup[] coinsToRestore = new List<Coin_Pickup>(collectedCoins).ToArray();
            collectedCoins.Clear();

            foreach (Coin_Pickup coin in coinsToRestore)
            {
                if (coin != null)
                {
                    coin.ResetCollection();
                }
            }

            RefreshSceneCoins();
            SyncDebugCounts();
        }

        private void SyncDebugCounts()
        {
            collectedCoinsDebug = CollectedCount;
            totalCoinsDebug = TotalCount;
        }

        private void RefreshSceneCoins()
        {
            Coin_Pickup[] allCoins = FindObjectsByType<Coin_Pickup>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None);
            List<Coin_Pickup> coinsInScene = new List<Coin_Pickup>();

            foreach (Coin_Pickup coin in allCoins)
            {
                if (coin.gameObject.scene == gameObject.scene)
                {
                    coinsInScene.Add(coin);
                }
            }

            sceneCoins = coinsInScene.ToArray();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}