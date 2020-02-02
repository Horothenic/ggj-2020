using UnityEngine;

using Zenject;

namespace Game
{
    public class StartWindow : MonoBehaviour
    {
        #region FIELDS

        [Inject] private GameManager gameManager = null;

        [SerializeField] private GameObject container = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            gameManager.onStartGame += Disappear;
        }

        private void OnDestroy()
        {
            gameManager.onStartGame -= Disappear;
        }

        private void Disappear()
        {
            container.SetActive(false);
        }

        #endregion
    }
}
