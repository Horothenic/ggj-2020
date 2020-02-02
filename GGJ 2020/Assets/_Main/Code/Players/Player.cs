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

        private bool playerEnable = false;

        #endregion

        #region PROPERTIES

        public string Id { get => id; }
        public bool PlayerEnable { get => playerEnable; }

        #endregion

        #region BEHVAIORS

        private void Awake()
        {
            gameManager.onStartGame += Restart;
            gameManager.onEndGame += Stop;
        }

        private void OnDestroy()
        {
            gameManager.onStartGame -= Restart;
            gameManager.onEndGame -= Stop;
        }

        private void OnStart(InputValue value)
        {
            switch (gameManager.GameState)
            {
                case GameState.BeforeStart:
                    gameManager.StartGame(id);
                    break;
                case GameState.GameEnded:
                    gameManager.ResetScene();
                    break;
            }
        }

        public void Stop()
        {
            playerEnable = false;
        }

        public void Restart()
        {
            playerEnable = true;
        }

        #endregion
    }
}
