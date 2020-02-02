using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

using Zenject;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        #region FIELDS

        private const string TimerFormat = "{0}";

        [Inject] private GameManager gameManager = null;

        [Header("COMPONENTS")]
        [SerializeField] private Text timerText = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private float gameDurationSeconds = 30;
        [SerializeField] private float fewTimeLeftSeconds = 15;
        [SerializeField] private Color fewTimeColor = Color.white;

        private float remainingSeconds = default(float);

        #endregion

        #region EVENTS

        public event UnityAction onFewTime;

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
            timerText.text = string.Format(TimerFormat, remainingSeconds);
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

                if (remainingSeconds == fewTimeLeftSeconds)
                    FewTimeReached();
            }

            gameManager.EndGame();
        }

        private void FewTimeReached()
        {
            timerText.color = fewTimeColor;
            onFewTime?.Invoke();
        }

        #endregion
    }
}
