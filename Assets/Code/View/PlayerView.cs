using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


namespace WORLDGAMEDEVELOPMENT
{
    public sealed class PlayerView : NetworkBehaviour
    {
        [SerializeField] private Transform _playerObject;
        [SerializeField] private Transform _playerOrientation;
        [SerializeField] private Collider _playerColider;
        [SerializeField] private Transform _lockAtCombat;
        [SerializeField] private Rigidbody _rigidbody;
    }
}