using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private float speed = 1;

        private new Rigidbody rigidbody = null;
        private Vector3 movement = default(Vector3);

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rigidbody.MovePosition(rigidbody.position + (movement * speed * Time.deltaTime));
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
