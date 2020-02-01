using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

using Zenject;
using Utilities.Inspector;

using Fixables;

namespace Players
{
    public class PlayerFixer : MonoBehaviour
    {
        #region FIELDS

        private const string RadialTickingKey = "RadialTick";
        private const string RadialEndedKey = "RadialEnded";

        [Inject] private Player player = null;
        [Inject] private PlayerMovement playerMovement = null;
        [Inject] private PlayerStorage playerStorage = null;

        [Header("COMPONENTS")]
        [SerializeField] private GameObject radialContainer = null;
        [SerializeField] private Image radialImage = null;
        [SerializeField] private Animator radialAnimator = null;

        [Header("CONFIGURATION")]
        [SerializeField] private LayerMask fixableLayer = default(LayerMask);
        [SerializeField] private float radius = 1;

        [Header("STATES")]
        [ReadOnly] [SerializeField] private int ticks = 0;

        private Fixable currentFixable = null;

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            DisappearRadial();
        }

        private void OnRepair(InputValue value)
        {
            if (playerStorage.IsStorageFull)
                return;

            if (currentFixable == null)
            {
                currentFixable = GetClosestFixable();

                if (currentFixable != null)
                {
                    currentFixable.onGain += TruncateFix;
                    playerMovement.Stop();
                    radialAnimator.SetTrigger(RadialTickingKey);
                    AddTick();
                }
            }
            else
            {
                AddTick();
            }
        }

        private Fixable GetClosestFixable()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, fixableLayer);

            if (hitColliders != null && hitColliders.Length > 0)
                return hitColliders[0].gameObject.GetComponent<Fixable>();
            else
                return null;
        }

        private void AddTick()
        {
            ticks++;
            AdjustUI();
            if (ticks >= currentFixable.Ticks)
                FixableGained();
        }

        public void TruncateFix(string playerId)
        {
            if (playerId == player.Id)
                return;

            playerMovement.Restart();
            DisappearRadial();
            ResetTicks();
        }

        private void FixableGained()
        {
            playerStorage.StoreFixable(currentFixable);
            currentFixable.Grabbed(player.Id);
            radialAnimator.SetTrigger(RadialEndedKey);
            playerMovement.Restart();
            ResetTicks();
        }

        private void AdjustUI()
        {
            radialImage.fillAmount = Mathf.InverseLerp(0, currentFixable.Ticks, ticks);
            radialContainer.SetActive(true);
        }

        private void DisappearRadial()
        {
            radialContainer.SetActive(false);
        }

        private void ResetTicks()
        {
            ticks = 0;
            currentFixable = null;
        }

        #endregion
    }
}
