using UnityEngine;

using Zenject;
using CFLFramework.Score;

namespace Fixables
{
    public class Box : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ScoreManager scoreManager = null;

        [SerializeField] private string teamId = "1";

        #endregion

        #region BEHAVIORS

        public void Score(Fixable fixable)
        {
            scoreManager.IncreaseScore(new string[] { teamId }, fixable.Points);
            fixable.DestroyFixable();
        }

        #endregion
    }
}
