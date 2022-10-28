using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


namespace WORLDGAMEDEVELOPMENT
{
    public sealed class TestingInputSystem : MonoBehaviour
    {
        [SerializeField] private float _forceJump;
        [SerializeField] private float _speed;

        private PlayerControls _playerInputActionsControls;
        private Rigidbody _rigidBody;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _rigidBody = GetComponent<Rigidbody>();
            _playerInputActionsControls = new PlayerControls();
            _playerInputActionsControls.Player.Jump.performed += Jump;
            _playerInputActionsControls.Enable();

            #region RebindKeyInputAction

            //RebindKeyInputAction
            //_playerInputActionsControls.Player.Disable();
            //_playerInputActionsControls.Player.Jump.PerformInteractiveRebinding().OnComplete(callback =>
            //{
            //    Debug.Log(callback.action.bindings[0]);
            //    callback.Dispose();
            //    _playerInputActionsControls.Player.Enable();
            //}).Start();

            #endregion

            #region Save and Load player Input Action

            ////Save _playerInput.actions to JSON
            //var rebinds = _playerInput.actions.SaveBindingOverridesAsJson();
            //PlayerPrefs.SetString("rebinds", rebinds);

            ////Load _playerInput.actions in PlayerPrefs
            //var reRebinds = PlayerPrefs.GetString("rebinds");
            //_playerInput.actions.LoadBindingOverridesFromJson(rebinds); 

            #endregion
        }

        private void OnDisable()
        {
            _playerInputActionsControls.Player.Jump.performed -= Jump;
        }

        private void FixedUpdate()
        {
            var inputVector = _playerInputActionsControls.Player.Movement.ReadValue<Vector2>();
            _rigidBody.AddForce(new Vector3(inputVector.x, 0.0f, inputVector.y) * _speed, ForceMode.Force);
        }

        private void Update()
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                _playerInput.SwitchCurrentActionMap("UI");
                _playerInputActionsControls.Player.Disable();
                _playerInputActionsControls.UI.Enable();
            }
            if (Keyboard.current.yKey.wasPressedThisFrame)
            {
                _playerInput.SwitchCurrentActionMap("Player");
                _playerInputActionsControls.UI.Disable();
                _playerInputActionsControls.Player.Enable();
            }
        }

        public void Submit(CallbackContext context)
        {
            Debug.Log($"Submit + {context}");
        }

        public void Jump(CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                Debug.Log($"Jump + {callbackContext.phase}");
                _rigidBody.AddForce(Vector3.up * _forceJump, ForceMode.Impulse);
            }
        }
    }
}