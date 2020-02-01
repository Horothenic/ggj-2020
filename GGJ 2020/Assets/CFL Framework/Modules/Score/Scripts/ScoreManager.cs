using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

using Zenject;
using CFLFramework.Data;
using Utilities.Extensions;

namespace CFLFramework.Score
{
    public class ScoreManager : MonoBehaviour
    {
        #region FIELDS

        private const string ScoreDataKey = "score_data";
        private const string ScoreKey = "score";
        private const string HighScoreKey = "high_score";
        private const float DefaultValue = 0;

        [Inject] private DataManager dataManager = null;

        private Dictionary<string, object> scores = new Dictionary<string, object>();

        #endregion

        #region EVENTS

        public event UnityAction<string[], float> scoreUpdated;
        public event UnityAction<string[], float> highScoreUpdated;

        #endregion

        #region BEHAVIORS

        public void IncreaseScore(string[] keys, float value)
        {
            float score = scores.IncreaseValue(keys, ScoreKey, value);
            scoreUpdated?.Invoke(keys, score);

            if (score > GetHighScore(keys))
                SaveHighScore(keys, score);
        }

        public void ResetScore(string[] keys)
        {
            scores.SetValue(keys, ScoreKey, DefaultValue);
            scoreUpdated?.Invoke(keys, DefaultValue);
        }

        public void ResetHighScore(string[] keys)
        {
            dataManager.SetData(GetFullHighScoreKeys(keys), DefaultValue);
            highScoreUpdated?.Invoke(keys, DefaultValue);
        }

        public float GetScore(string[] keys)
        {
            return scores.GetValue<float>(keys, ScoreKey);
        }

        public float GetHighScore(string[] keys)
        {
            return dataManager.GetData<float>(GetFullHighScoreKeys(keys), DefaultValue);
        }

        private void SaveHighScore(string[] keys, float score)
        {
            dataManager.SetData(GetFullHighScoreKeys(keys), score);
            highScoreUpdated?.Invoke(keys, score);
        }

        private string[] GetFullHighScoreKeys(string[] keys)
        {
            List<string> fullKeys = new List<string>(keys);
            fullKeys.Insert(0, ScoreDataKey);
            fullKeys.Insert(fullKeys.Count, HighScoreKey);
            return fullKeys.ToArray();
        }

        #endregion
    }
}
