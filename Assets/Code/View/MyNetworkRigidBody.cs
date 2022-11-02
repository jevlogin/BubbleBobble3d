using Unity.Netcode.Components;
using UnityEngine;


namespace WORLDGAMEDEVELOPMENT
{
    public sealed class MyNetworkRigidBody : NetworkRigidbody
    {
        [SerializeField] private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
        }
    }
}