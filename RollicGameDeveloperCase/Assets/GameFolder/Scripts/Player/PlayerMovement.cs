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


       

        public bool canMovingStartPosition;



        private void Awake()
        {
            startSpeed = forwardSpeed;
                   
        }

        private void OnEnable()
        {
            PlayerCollisionController.OnHitElevator += StopPlayerWait;
           
            GameManager.OnEndGameStarted += StopPlayer;
            
            GameManager.OnLevelCompleted += StoplPlayerWithDrag;
            GameManager.OnLevelStarted += CannotMoveStartPosition;
        }
        private void OnDisable()
        {
            PlayerCollisionController.OnHitElevator -= StopPlayerWait;
            
            GameManager.OnEndGameStarted -= StopPlayer;
            
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
            GetComponent<PhysicsController>().SetRigidboySettingsOnStart();


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
