using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.Netcode;
using UnityEngine;


namespace WORLDGAMEDEVELOPMENT
{
    public sealed class ThirdPersonCamera : MonoBehaviour
    {
        [Header("Reference")]
        public Transform Player;
        public Rigidbody RigidbodyPlayer;
        public Transform PlayerObject;
        public Transform Orientation;
        public Transform CombatLockAt;

        public float RotationSpeed;

        public CameraStyle CurrentCameraStyle;

        public GameObject CameraBasic;
        public GameObject CameraCombat;
        public GameObject CameraTopDown;

        private void Start()
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }

        private void OnDisable()
        {
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }

        private void Update()
        {
            SwitchCamera();

            if (CurrentCameraStyle == CameraStyle.Basic || CurrentCameraStyle == CameraStyle.TopDown)
            {
                var viewDirection = Player.transform.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
                Orientation.forward = viewDirection.normalized;

                var horizontal = Input.GetAxis("Horizontal");
                var vertical = Input.GetAxis("Vertical");

                var inputDirection = Orientation.forward * vertical + Orientation.right * horizontal;
                if (inputDirection != Vector3.zero)
                {
                    PlayerObject.forward = Vector3.Slerp(PlayerObject.forward, inputDirection.normalized, RotationSpeed * Time.deltaTime);
                }
            }
            else if (CurrentCameraStyle == CameraStyle.Combat)
            {
                var directionToLockAt = CombatLockAt.position - new Vector3(transform.position.x, CombatLockAt.position.y, transform.position.z);
                Orientation.forward = directionToLockAt.normalized;

                PlayerObject.forward = directionToLockAt.normalized;
            }
        }

        private void SwitchCamera()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CombatLockAt.gameObject.SetActive(false);
                SwitchCameraStyle(CameraStyle.Basic);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CombatLockAt.gameObject.SetActive(true);
                SwitchCameraStyle(CameraStyle.Combat);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CombatLockAt.gameObject.SetActive(false);
                SwitchCameraStyle(CameraStyle.TopDown);
            }

        }

        private void SwitchCameraStyle(CameraStyle newCameraStyle)
        {
            CameraBasic.SetActive(false);
            CameraCombat.SetActive(false);
            CameraTopDown.SetActive(false);

            switch (newCameraStyle)
            {
                case CameraStyle.Basic:
                    CameraBasic.SetActive(true);
                    break;
                case CameraStyle.Combat:
                    CameraCombat.SetActive(true);
                    break;
                case CameraStyle.TopDown:
                    CameraTopDown.SetActive(true);
                    break;
            }

            CurrentCameraStyle = newCameraStyle;
        }
    }
}