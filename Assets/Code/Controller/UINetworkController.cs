using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


namespace WORLDGAMEDEVELOPMENT
{
    public sealed class UINetworkController : MonoBehaviour
    {
        [SerializeField] private UINetworkView _uINetworkView;

        private void Awake()
        {
            _uINetworkView.ButtonHost.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
            _uINetworkView.ButtonServer.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
            _uINetworkView.ButtonClient.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
        }

        private void OnDisable()
        {
            _uINetworkView.ButtonHost.onClick.RemoveAllListeners();
            _uINetworkView.ButtonServer.onClick.RemoveAllListeners();
            _uINetworkView.ButtonClient.onClick.RemoveAllListeners();
        }
    }
}