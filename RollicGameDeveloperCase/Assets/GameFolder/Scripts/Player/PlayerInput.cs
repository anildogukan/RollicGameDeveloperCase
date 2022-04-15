using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollicDeveloperCase.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private float lastFrameFingerPositionX;
        private float moveFactorx;
        private float forceFactor;
        public float MovefactorX => moveFactorx;

        public float ForceFactor => forceFactor;


        // Update is called once per frame
        void Update()
        {

            if (GameManager.Instance.gameState == GameState.GamePlay)
            {
                SwerveMmovementInput();
            }
            else if (GameManager.Instance.gameState == GameState.EndGame)
            {               
                TapMovementInput();
            }


        }

        private void SwerveMmovementInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastFrameFingerPositionX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                moveFactorx = Input.mousePosition.x - lastFrameFingerPositionX;

                lastFrameFingerPositionX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                moveFactorx = 0f;
            }
            else
            {
                moveFactorx = 0f;

            }
        }

        private void TapMovementInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                forceFactor += 10;
            }
            else
            {
                forceFactor=0;
            }
        }


    }
}