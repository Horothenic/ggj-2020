using UnityEngine;
using System.Collections;

using Zenject;

namespace Players
{
    public class PlayerKnockback : MonoBehaviour
    {
        #region FIELDS

        private const string StunnedKey = "Stunned";
        private const string EndStunKey = "EndStun";

        [Inject] private Player player = null;
        [Inject] private PlayerFixer playerFixer = null;

        [Header("COMPONENTS")]
        [SerializeField] private Animator animator = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private float strength = 5;
        [SerializeField] private float knockbackDuration = 0.6f;

        private new Rigidbody rigidbody = null;
        private bool knocked = false;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Knockback(Vector3 hitPosition, string playerId)
        {
            if (knocked)
                return;

            knocked = true;
            playerFixer.TruncateFix(playerId);
            Vector3 direction = transform.position - hitPosition;
            rigidbody.AddForce(direction * strength, ForceMode.Impulse);
            StartCoroutine(StunRoutine());
        }

        private IEnumerator StunRoutine()
        {
            animator.SetTrigger(StunnedKey);
            player.Stop();
            yield return new WaitForSeconds(knockbackDuration);
            animator.SetTrigger(EndStunKey);
            player.Restart();
            knocked = false;
        }

        #endregion
    }
}
