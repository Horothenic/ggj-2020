using UnityEngine;

namespace Utilities
{
    public class AlwaysZeroRotation : MonoBehaviour
    {
        #region BEHAVIORS

        private void Update()
        {
            transform.eulerAngles = Vector3.zero;
        }

        #endregion
    }
}
