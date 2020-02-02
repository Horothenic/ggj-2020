using UnityEngine;
using UnityEngine.InputSystem;

using Zenject;

using Game;

namespace Players
{
    public class Player : MonoBehaviour
    {
        #region FIELDS

        [Inject] private GameManager gameManager = null;

        [SerializeField] private string id = "1";

        #endregion

        #region PROPERTIES

        public string Id { get => id; }

        #endregion

        #region BEHVAIORS

        private void OnStart(InputValue value)
        {
            switch (gameManager.GameState)
            {
                case GameState.BeforeStart:
                    gameManager.StartGame();
                    break;
                case GameState.GameEnded:
                    gameManager.ResetScene();
                    break;
            }
        }

        #endregion
    }
}
