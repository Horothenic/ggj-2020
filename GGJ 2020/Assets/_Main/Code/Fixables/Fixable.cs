using UnityEngine;
using UnityEngine.Events;

namespace Fixables
{
    public class Fixable : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private Sprite normalSprite = null;
        [SerializeField] private Sprite brokenSprite = null;
        [SerializeField] private SpriteRenderer spriteRenderer = null;
        [SerializeField] private Collider flyingCollider = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private string floorTag = null;
        [SerializeField] private int ticks = 5;
        [SerializeField] private int points = 5;

        private bool grabEnable = false;

        #endregion

        #region EVENTS

        public event UnityAction<string> onGain;

        #endregion

        #region PROPERTIES

        public int Ticks { get => ticks; }
        public int Points { get => points; }
        public Sprite Sprite { get => normalSprite; }
        public bool IsGrabbable { get => grabEnable; }

        #endregion

        #region BEHAVIORS

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == floorTag)
                Broken();
        }

        private void Broken()
        {
            flyingCollider.enabled = false;
            grabEnable = true;
            spriteRenderer.sprite = brokenSprite;
        }

        public void Grabbed(string playerId)
        {
            onGain?.Invoke(playerId);
            gameObject.SetActive(false);
        }

        public void DestroyFixable()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
