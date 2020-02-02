using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Zenject;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        #region FIELDS

        private const string TimerFormat = "{0:00}:{1:00}";

        [Inject] private GameManager gameManager = null;

        [Header("COMPONENTS")]
        [SerializeField] private Text timerText = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private float gameDurationSeconds = 30;

        private float remainingSeconds = default(float);

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            DisappearTimer();
            gameManager.onStartGame += StartTimer;
        }

        private void OnDestroy()
        {
            gameManager.onStartGame -= StartTimer;
        }

        private void AppearTimer()
        {
            timerText.gameObject.SetActive(true);
        }

        private void DisappearTimer()
        {
            timerText.gameObject.SetActive(false);
        }

        private void UpdateUI()
        {
            timerText.text = string.Format(TimerFormat, remainingSeconds / 60, remainingSeconds % 60);
        }

        private void StartTimer()
        {
            remainingSeconds = gameDurationSeconds;
            UpdateUI();
            AppearTimer();
            StartCoroutine(TimerRoutine());
        }

        private IEnumerator TimerRoutine()
        {
            for (int i = 0; i < gameDurationSeconds; i++)
            {
                yield return new WaitForSeconds(1);
                remainingSeconds--;
                UpdateUI();
            }

            gameManager.EndGame();
        }

        #endregion
    }
}
