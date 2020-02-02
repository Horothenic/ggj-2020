using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LoadGameMode : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private string sceneName = null;

        private Button loadButton = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            loadButton = GetComponent<Button>();
            loadButton.onClick.AddListener(LoadMode);
        }

        private void LoadMode()
        {
            SceneManager.LoadScene(sceneName);
        }

        #endregion
    }
}
