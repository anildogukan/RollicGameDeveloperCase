using System;
using System.Collections;

using UnityEngine;
using DG.Tweening;


namespace RollicDeveloperCase.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public static event Action OnCheckPassCondition;

        [Header("Movement Settings")]
        public float forwardSpeed;
        [SerializeField] private float swerveSpeed;
        [SerializeField] private float maxSwerveAmount;

        public Rigidbody _rigidbody;
        [SerializeField] PlayerInput _playerInput;

        private float swerveAmount;
        Vector3 localVel;

        float startSpeed;

        float playerTransformX;

        const float constForce = 10f;

        public float currentVelocity;
        public float maxVelocity = 40;


        [SerializeField] private float jumpingGravity;
        private Vector3 startGravity;

        public bool canMovingStartPosition;



        private void Awake()
        {
            startSpeed = forwardSpeed;
            startGravity = Physics.gravity;
            
        }

        private void OnEnable()
        {
            PlayerCollisionController.OnHitElevator += StopPlayerWait;
            PlayerCollisionController.OnHitBumpEndLine += SetGravityOnJump;
            GameManager.OnEndGameStarted += StopPlayer;
            GameManager.OnEndGameStarted += SetRigidboySettingsOnEndGame;
            GameManager.OnLevelCompleted += StoplPlayerWithDrag;
            GameManager.OnLevelStarted += CannotMoveStartPosition;


        }
        private void OnDisable()
        {
            PlayerCollisionController.OnHitElevator -= StopPlayerWait;
            PlayerCollisionController.OnHitBumpEndLine += SetGravityOnJump;
            GameManager.OnEndGameStarted -= StopPlayer;
            GameManager.OnEndGameStarted -= SetRigidboySettingsOnEndGame;
            GameManager.OnLevelCompleted -= StoplPlayerWithDrag;
            GameManager.OnLevelStarted -= CannotMoveStartPosition;
        }

        private void Update()
        {
            SetBorderForMovement();

        }

       
        private void CannotMoveStartPosition()
        {
            canMovingStartPosition = false;
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.gameState == GameState.GamePlay)
            {

                MovePlayer();
            }

            else if (GameManager.Instance.gameState == GameState.EndGame)
            {

                MovePlayerWithTapping();
            }

        }

        public IEnumerator MoveToStartPosition(Vector3 startPosition)
        {
            yield return new WaitForSeconds(3f);
            canMovingStartPosition = true;
            transform.eulerAngles = Vector3.zero;
            transform.position = new Vector3(0, 0, transform.position.z);
            transform.DOMove(startPosition, 3f);
            forwardSpeed = startSpeed;
            SetRigidboySettingsOnStart();


        }


        private void MovePlayerWithTapping()
        {
            currentVelocity = _rigidbody.velocity.z;

            if (maxVelocity > currentVelocity)
            {
                _rigidbody.AddForce((transform.forward * constForce), ForceMode.Force);
                _rigidbody.AddForce((transform.forward * _playerInput.ForceFactor), ForceMode.Impulse);
            }
            else
            {
                currentVelocity = maxVelocity;

            }
        }

        private void SetRigidboySettingsOnEndGame()
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            _rigidbody.useGravity = true;

        }
        private void SetRigidboySettingsOnStart()
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;          
            _rigidbody.useGravity = false;
            _rigidbody.drag = 0.1f;
            _rigidbody.angularDrag = 0.05f;
            SetGravityOnStart();


        }

        //public void FreezeRots()
        //{
            
        //    _rigidbody.constraints= RigidbodyConstraints.FreezeRotationX;
        //    _rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
        //}

        IEnumerator StopAndWait()
        {
            CalculateLocalVelx();

            StopPlayer();

            yield return new WaitForSeconds(3);
            OnCheckPassCondition?.Invoke();

            yield return new WaitForSeconds(1);
            forwardSpeed = startSpeed;
        }

        private void StopPlayer()
        {
            _rigidbody.velocity = Vector3.zero;
            forwardSpeed = 0f;
        }

        private void StoplPlayerWithDrag()
        {
            _rigidbody.drag = 3;
            _rigidbody.angularDrag = 3;
        }

        private void SetGravityOnJump()
        {
            Physics.gravity = Vector3.down * jumpingGravity;
        }

        private void SetGravityOnStart()
        {
            Physics.gravity = startGravity;
        }

        private void SetBorderForMovement()
        {
            playerTransformX = transform.position.x;
            playerTransformX = Mathf.Clamp(playerTransformX, -3f, 3f);
            transform.position = new Vector3(playerTransformX, transform.position.y, transform.position.z);
        }




        private void MovePlayer()
        {
            CalculateLocalVelx();

            localVel.z = forwardSpeed * Time.fixedDeltaTime * 100;
            _rigidbody.velocity = transform.TransformDirection(localVel);
        }

        private void StopPlayerWait()
        {
            StartCoroutine(StopAndWait());
        }


        private void CalculateLocalVelx()
        {
            swerveAmount = Time.fixedDeltaTime * swerveSpeed * _playerInput.MovefactorX;
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);

            localVel = transform.InverseTransformDirection(_rigidbody.velocity);
            localVel.x = swerveAmount;
        }







    }
}
