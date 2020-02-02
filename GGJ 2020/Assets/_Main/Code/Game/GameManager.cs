using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        #region EVENTS

        public event UnityAction onStartGame;
        public event UnityAction onEndGame;

        private GameState gameState = GameState.BeforeStart;

        #endregion

        #region PROPERTIES

        public GameState GameState { get => gameState; }

        #endregion

        #region BEHAVIORS

        public void StartGame()
        {
            gameState = GameState.Playing;
            onStartGame?.Invoke();
        }

        public void EndGame()
        {
            gameState = GameState.GameEnded;
            onEndGame?.Invoke();
        }

        public void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        #endregion
    }
}
