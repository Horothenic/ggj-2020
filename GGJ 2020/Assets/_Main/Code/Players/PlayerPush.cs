using UnityEngine;

using Zenject;

using Fixables;

namespace Players
{
    public class PlayerPush : MonoBehaviour
    {
        #region FIELDS

        [Inject] private Player player = null;
        [Inject] private PlayerStorage playerStorage = null;
        [Inject] private PlayerKnockback playerKnockback = null;

        [SerializeField] private LayerMask playerLayer = default(LayerMask);
        [SerializeField] private float radius = 1;

        #endregion

        #region BEHAVIORS

        private void OnAction()
        {
            if (!player.PlayerEnable)
                return;

            Box box = playerStorage.GetClosestBox();
            if (box != null)
                return;

            PlayerKnockback playerKnockback = GetClosestPlayerKnockback();
            if (playerKnockback == null)
                return;

            playerKnockback.Knockback(transform.position, player.Id);
        }

        public PlayerKnockback GetClosestPlayerKnockback()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, playerLayer);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                PlayerKnockback found = hitColliders[i].gameObject.GetComponent<PlayerKnockback>();
                if (found != playerKnockback)
                    return found;
            }

            return null;
        }

        #endregion
    }
}
