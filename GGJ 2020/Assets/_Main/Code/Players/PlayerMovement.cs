using UnityEngine;
using UnityEngine.InputSystem;

using Zenject;

using Game;

namespace Players
{
    public class PlayerMovement : MonoBehaviour
    {
        #region FIELDS

        [Inject] private GameManager gameManager = null;

        [SerializeField] private float speed = 1;

        private new Rigidbody rigidbody = null;
        private Vector3 movement = default(Vector3);
        private bool movementEnable = true;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            gameManager.onStartGame += Restart;
            gameManager.onEndGame += Stop;
        }

        private void OnDestroy()
        {
            gameManager.onStartGame -= Restart;
            gameManager.onEndGame -= Stop;
        }

        private void Start()
        {
            Stop();
        }

        private void FixedUpdate()
        {
            if (!movementEnable)
                return;

            rigidbody.MovePosition(rigidbody.position + (movement * speed * Time.deltaTime));
        }

        private void OnMove(InputValue value)
        {
            movement = value.Get<Vector2>();
            movement.z = movement.y;
            movement.y = 0;
        }

        public void Stop()
        {
            movementEnable = false;
        }

        public void Restart()
        {
            movementEnable = true;
        }

        #endregion
    }
}
