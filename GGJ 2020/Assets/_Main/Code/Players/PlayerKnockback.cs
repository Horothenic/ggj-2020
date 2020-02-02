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
        [SerializeField] private string flyingFixableTag = null;
        [SerializeField] private float strength = 5;
        [SerializeField] private float knockbackDuration = 0.6f;

        private new Rigidbody rigidbody = null;
        private bool stunned = false;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (stunned || other.gameObject.tag != flyingFixableTag)
                return;

            playerFixer.TruncateFix(player.Id);
            StartCoroutine(StunRoutine());
        }

        public void Knockback(Vector3 hitPosition, string playerId)
        {
            if (stunned)
                return;

            Vector3 direction = transform.position - hitPosition;
            rigidbody.AddForce(direction * strength, ForceMode.Impulse);

            playerFixer.TruncateFix(playerId);
            StartCoroutine(StunRoutine());
        }

        private IEnumerator StunRoutine()
        {
            stunned = true;
            animator.SetTrigger(StunnedKey);
            player.Stop();
            yield return new WaitForSeconds(knockbackDuration);
            animator.SetTrigger(EndStunKey);
            player.Restart();
            stunned = false;
        }

        #endregion
    }
}
