using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private int numberOfPlayers = default(int);

        private GameState gameState = GameState.BeforeStart;
        private List<string> playersRegistered = new List<string>();

        #endregion

        #region EVENTS

        public event UnityAction onStartGame;
        public event UnityAction onEndGame;

        #endregion

        #region PROPERTIES

        public GameState GameState { get => gameState; }

        #endregion

        #region BEHAVIORS

        public void StartGame(string playerId)
        {
            if (!playersRegistered.Contains(playerId))
                playersRegistered.Add(playerId);

            if (playersRegistered.Count != numberOfPlayers)
                return;

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
