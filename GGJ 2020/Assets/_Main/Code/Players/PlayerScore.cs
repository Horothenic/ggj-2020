using UnityEngine;
using UnityEngine.UI;

using Zenject;
using CFLFramework.Score;
using Utilities.Extensions;

namespace Players
{
    public class PlayerScore : MonoBehaviour
    {
        #region FIELDS

        private const string ScoreFormat = "Player {0}: {1}";

        [Inject] private ScoreManager scoreManager = null;

        [SerializeField] private string id = "1";
        [SerializeField] private Text playerScore = null;

        private string[] keys = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            keys = new string[] { id };
            scoreManager.scoreUpdated += UpdateScore;
        }

        private void Start()
        {
            UpdateScore(scoreManager.GetScore(keys));
        }

        private void OnDestroy()
        {
            scoreManager.scoreUpdated -= UpdateScore;
        }

        private void UpdateScore(string[] keys, float value)
        {
            if (keys.IsEqual(this.keys))
                return;

            UpdateScore(value);
        }

        private void UpdateScore(float value)
        {
            playerScore.text = string.Format(ScoreFormat, id, value);
        }

        #endregion
    }
}
