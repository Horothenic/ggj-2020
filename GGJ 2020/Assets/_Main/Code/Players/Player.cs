using UnityEngine;

using Zenject;
using CFLFramework.Score;

namespace Players
{
    public class Player : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ScoreManager scoreManager = null;

        [SerializeField] private string id = "1";

        #endregion

        #region PROPERTIES

        public string Id { get => id; }

        #endregion

        #region BEHAVIORS

        public void IncreaseScore(float increment)
        {
            scoreManager.IncreaseScore(new string[] { id }, increment);
        }

        #endregion
    }
}
