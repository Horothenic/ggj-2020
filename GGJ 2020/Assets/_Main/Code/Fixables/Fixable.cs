using UnityEngine;
using UnityEngine.Events;

namespace Fixables
{
    public class Fixable : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private int ticks = 5;
        [SerializeField] private int points = 5;

        #endregion

        #region EVENTS

        public event UnityAction<string> onGain;

        #endregion

        #region PROPERTIES

        public int Ticks { get => ticks; }
        public int Points { get => points; }

        #endregion

        #region BEHAVIORS

        public void DestroyFixable(string playerId)
        {
            onGain?.Invoke(playerId);
            Destroy(gameObject);
        }

        #endregion
    }
}
