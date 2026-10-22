using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MaterialShift
{
    public class Level_Completion_Controller : MonoBehaviour
    {
        [Header("Results UI")]
        [SerializeField] private GameObject resultsPanel;
        [SerializeField] private TMP_Text collectedCoinsText;
        [SerializeField] private TMP_Text totalCoinsText;
        [SerializeField] private Image[] starDisplays = new Image[3];

        [Header("Scene Configuration")]
        [SerializeField] private string mainMenuSceneName;
        [SerializeField] private string nextLevelSceneName;

        public int CollectedCoinCount { get; private set; }
        public int TotalCoinCount { get; private set; }
        public int EarnedStarCount { get; private set; }

        private bool completionTriggered;
        private float timeScaleBeforeCompletion = 1f;

        private void Awake()
        {
            if (resultsPanel != null)
            {
                resultsPanel.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                CompleteLevel();
            }
        }

        public void CompleteLevel()
        {
            if (completionTriggered)
            {
                return;
            }

            Coin_Tracker coinTracker = Coin_Tracker.Instance;

            if (coinTracker == null)
            {
                Debug.LogError("Level completion requires a Coin_Tracker in the active scene.", this);
                return;
            }

            completionTriggered = true;
            timeScaleBeforeCompletion = Time.timeScale;
            CollectedCoinCount = coinTracker.CollectedCount;
            TotalCoinCount = coinTracker.TotalCount;
            EarnedStarCount = Star_Rating_Calculator.CalculateRating(coinTracker);

            UpdateResultsUI();

            if (resultsPanel != null)
            {
                resultsPanel.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Assign the results panel on Level_Completion_Controller.", this);
            }

            Time.timeScale = 0f;
        }

        public void RetryLevel()
        {
            Coin_Tracker coinTracker = Coin_Tracker.Instance;

            if (coinTracker == null)
            {
                Debug.LogError("Retry requires a Coin_Tracker in the active scene.", this);
                ResumeGameplay();
                ResetCompletionState();
                return;
            }

            string currentSceneName = SceneManager.GetActiveScene().name;
            coinTracker.ResetForRetry();
            ResumeGameplay();

            if (CanLoadScene(currentSceneName))
            {
                SceneManager.LoadScene(currentSceneName);
                return;
            }

            Debug.LogWarning(
                "The current scene is not enabled in Build Settings, so it cannot be restarted. " +
                "Coins were reset for this attempt instead.",
                this);
            ResetCompletionState();
        }

        public void LoadMainMenu()
        {
            LoadConfiguredScene(mainMenuSceneName, "main menu");
        }

        public void LoadNextLevel()
        {
            LoadConfiguredScene(nextLevelSceneName, "next level");
        }

        private void UpdateResultsUI()
        {
            if (collectedCoinsText != null)
            {
                collectedCoinsText.text = CollectedCoinCount.ToString();
            }

            if (totalCoinsText != null)
            {
                totalCoinsText.text = TotalCoinCount.ToString();
            }

            if (starDisplays == null)
            {
                return;
            }

            for (int index = 0; index < starDisplays.Length; index++)
            {
                if (starDisplays[index] != null)
                {
                    starDisplays[index].enabled = index < EarnedStarCount;
                }
            }
        }

        private void LoadConfiguredScene(string sceneName, string destinationDescription)
        {
            if (!CanLoadScene(sceneName))
            {
                Debug.LogWarning(
                    $"Cannot load the {destinationDescription}: assign a valid scene name and " +
                    "include that scene in Build Settings.",
                    this);
                return;
            }

            ResumeGameplay();
            SceneManager.LoadScene(sceneName);
        }

        private bool CanLoadScene(string sceneName)
        {
            return !string.IsNullOrWhiteSpace(sceneName) &&
                Application.CanStreamedLevelBeLoaded(sceneName);
        }

        private void ResumeGameplay()
        {
            Time.timeScale = timeScaleBeforeCompletion;
        }

        private void ResetCompletionState()
        {
            completionTriggered = false;
            CollectedCoinCount = 0;
            TotalCoinCount = 0;
            EarnedStarCount = 0;

            if (resultsPanel != null)
            {
                resultsPanel.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            if (completionTriggered)
            {
                ResumeGameplay();
            }
        }
    }
}