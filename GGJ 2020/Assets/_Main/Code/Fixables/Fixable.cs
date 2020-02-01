using UnityEngine;
using UnityEngine.Events;

namespace Fixables
{
    public class Fixable : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private Sprite sprite = null;
        [SerializeField] private int ticks = 5;
        [SerializeField] private int points = 5;

        #endregion

        #region EVENTS

        public event UnityAction<string> onGain;

        #endregion

        #region PROPERTIES

        public int Ticks { get => ticks; }
        public int Points { get => points; }
        public Sprite Sprite { get => sprite; }

        #endregion

        #region BEHAVIORS

        public void Grabbed(string playerId)
        {
            onGain?.Invoke(playerId);
            gameObject.SetActive(false);
        }

        #endregion
    }
}
