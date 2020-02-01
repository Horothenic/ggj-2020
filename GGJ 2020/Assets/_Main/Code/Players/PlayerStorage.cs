using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Fixables;

namespace Players
{
    public class PlayerStorage : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private Transform fixableImagesParent = null;
        [SerializeField] private FixableImage fixableImagePrefab = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private int maxStorage = 3;
        [SerializeField] private LayerMask boxLayer = default(LayerMask);
        [SerializeField] private float radius = 1;

        private List<Fixable> storedFixables = new List<Fixable>();
        private List<FixableImage> fixableImages = new List<FixableImage>();

        #endregion

        #region PROPERTIES

        public bool IsStorageFull { get => storedFixables.Count >= maxStorage; }

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            UpdateUI();
        }

        public void StoreFixable(Fixable fixable)
        {
            storedFixables.Add(fixable);
            UpdateUI();
        }

        private void OnAction(InputValue value)
        {
            if (storedFixables.Count == 0)
                return;

            Box box = GetClosestBox();
            if (box == null)
                return;

            box.Score(storedFixables[0]);
            storedFixables.RemoveAt(0);
            UpdateUI();
        }

        private Box GetClosestBox()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, boxLayer);

            if (hitColliders != null && hitColliders.Length > 0)
                return hitColliders[0].gameObject.GetComponent<Box>();
            else
                return null;
        }

        private void UpdateUI()
        {
            fixableImagesParent.gameObject.SetActive(storedFixables.Count > 0);

            for (int i = 0; i < fixableImages.Count; i++)
                Destroy(fixableImages[i].gameObject);

            fixableImages.Clear();

            for (int i = 0; i < storedFixables.Count; i++)
                fixableImages.Add(Instantiate(fixableImagePrefab, fixableImagesParent).Initialize(storedFixables[i]));
        }

        #endregion
    }
}
