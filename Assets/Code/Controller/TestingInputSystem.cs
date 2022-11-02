using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


namespace WORLDGAMEDEVELOPMENT
{
    public sealed class TestingInputSystem : NetworkBehaviour
    {
        [SerializeField] private float _forceJump;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _rotationSpeed;

        private PlayerControls _playerInputActionsControls;
        private Rigidbody _rigidBody;
        private PlayerInput _playerInput;
        private Vector2 _inputVector = Vector2.zero;
        private Camera _camera;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                Destroy(this);
            }
        }

        private void Awake()
        {
            _camera = Camera.main;
            _playerInput = GetComponent<PlayerInput>();
            _rigidBody = GetComponent<Rigidbody>();
            _playerInputActionsControls = new PlayerControls();
            _playerInputActionsControls.Player.Jump.performed += Jump;
            _playerInputActionsControls.Player.Movement.performed += Movement_performed;
            _playerInputActionsControls.Player.Movement.canceled += Movement_canceled;
            _playerInputActionsControls.Enable();
        }

        private void Movement_canceled(CallbackContext obj)
        {
            _inputVector = Vector2.zero;
        }

        private void Movement_performed(CallbackContext context)
        {
            _inputVector = context.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            _playerInputActionsControls.Player.Jump.performed -= Jump;
            _playerInputActionsControls.Player.Movement.performed -= Movement_performed;
            _playerInputActionsControls.Player.Movement.canceled -= Movement_canceled;
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleRotation()
        {
            var dir = _camera.transform.forward;
            var rot = Quaternion.LookRotation(dir);
            transform.localRotation = Quaternion.RotateTowards(transform.rotation, rot, _rotationSpeed);
            //TODO - переделать вращение игрока относительно камеры
        }

        /// <summary>
        /// Movement Input System 
        /// </summary>
        private void HandleMovement()
        {
            var input = new Vector3(_inputVector.x, 0.0f, _inputVector.y);
            _rigidBody.velocity += input.normalized * _speed * Time.fixedDeltaTime;
            _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, _maxVelocity);
        }

        public void Jump(CallbackContext context)
        {
            _rigidBody.AddForce(Vector3.up * _forceJump, ForceMode.Impulse);
        }
    }
}