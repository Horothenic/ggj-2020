using UnityEngine;
using UnityEngine.UI;

using Zenject;
using CFLFramework.Score;
using Utilities.Extensions;

namespace Teams
{
    public class TeamScore : MonoBehaviour
    {
        #region FIELDS

        private const string ScoreFormat = "Team {0}: {1}";

        [Inject] private ScoreManager scoreManager = null;

        [SerializeField] private string teamId = "1";
        [SerializeField] private Text teamScoreText = null;

        private string[] keys = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            keys = new string[] { teamId };
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
            teamScoreText.text = string.Format(ScoreFormat, teamId, value);
        }

        #endregion
    }
}
