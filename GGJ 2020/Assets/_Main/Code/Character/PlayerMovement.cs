using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private float speed = 1;

        private Vector3 movement = default(Vector3);

        #endregion

        #region BEHAVIORS

        private void Update()
        {
            transform.Translate(movement * speed * Time.deltaTime, Space.Self);
        }

        private void OnMove(InputValue value)
        {
            movement = value.Get<Vector2>();
            movement.z = movement.y;
            movement.y = 0;
        }

        #endregion
    }
}
