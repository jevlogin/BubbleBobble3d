using UnityEngine;
using UnityEngine.UI;


namespace WORLDGAMEDEVELOPMENT
{
    public sealed class UINetworkView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Button _buttonHost;
        [SerializeField] private Button _buttonServer;
        [SerializeField] private Button _buttonClient;

        #endregion


        #region Properties

        public Button ButtonHost => _buttonHost;
        public Button ButtonServer => _buttonServer;
        public Button ButtonClient => _buttonClient;

        #endregion

        private void Awake()
        {
            _buttonHost.onClick.AddListener(() => gameObject.SetActive(false));
            _buttonServer.onClick.AddListener(() => gameObject.SetActive(false));
            _buttonClient.onClick.AddListener(() => gameObject.SetActive(false));
        }

        private void OnDisable()
        {
            _buttonHost.onClick.RemoveAllListeners();
            _buttonServer.onClick.RemoveAllListeners();
            _buttonClient.onClick.RemoveAllListeners();
        }
    }
}