using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private string id = "1";

        #endregion

        #region PROPERTIES

        public string Id { get => id; }

        #endregion
    }
}
