using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RollicDeveloperCase.Player
{
    public class PhysicsController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float jumpingGravity;
        private Vector3 startGravity;


        private void Awake()
        {
            startGravity = Physics.gravity;
        }
        private void OnEnable()
        {
            GameManager.OnEndGameStarted += SetRigidboySettingsOnEndGame;
            PlayerCollisionController.OnHitBumpEndLine += SetGravityOnJump;
        }
        private void OnDisable()
        {
            GameManager.OnEndGameStarted -= SetRigidboySettingsOnEndGame;
            PlayerCollisionController.OnHitBumpEndLine += SetGravityOnJump;
        }

        private void SetRigidboySettingsOnEndGame()
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            _rigidbody.useGravity = true;

        }
        public void SetRigidboySettingsOnStart()
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            _rigidbody.useGravity = false;
            _rigidbody.drag = 0.1f;
            _rigidbody.angularDrag = 0.05f;
            SetGravityOnStart();


        }

        private void SetGravityOnJump()
        {
            Physics.gravity = Vector3.down * jumpingGravity;
        }

        private void SetGravityOnStart()
        {
            Physics.gravity = startGravity;
        }
    }
}