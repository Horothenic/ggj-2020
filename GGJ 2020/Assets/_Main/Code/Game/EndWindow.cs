using UnityEngine;

using Zenject;

namespace Game
{
    public class EndWindow : MonoBehaviour
    {
        #region FIELDS

        [Inject] private GameManager gameManager = null;

        [SerializeField] private GameObject container = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            gameManager.onEndGame += Appear;
        }

        private void OnDestroy()
        {
            gameManager.onEndGame -= Appear;
        }

        private void Appear()
        {
            container.SetActive(true);
        }

        #endregion
    }
}
