using UnityEngine;
using UnityEngine.UI;

namespace Fixables
{
    public class FixableImage : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private Image image = null;

        #endregion

        #region BEHAVIORS

        public FixableImage Initialize(Fixable fixable)
        {
            image.sprite = fixable.Sprite;
            return this;
        }

        #endregion
    }
}
