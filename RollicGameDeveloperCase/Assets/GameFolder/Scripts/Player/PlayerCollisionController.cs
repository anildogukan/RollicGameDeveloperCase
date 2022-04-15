using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollicDeveloperCase.Player
{
    public class PlayerCollisionController : MonoBehaviour
    {
        public static Action OnHitElevator;
        public static Action OnHitTriggerColletctorAsistant;
        public static Action OnChopperTriggerBox;
        public static Action OnHitBumpEndLine;

        private void OnTriggerEnter(Collider other)
        {
            //**Elevator
            if (other.gameObject.layer == 14)
            {
                OnHitElevator?.Invoke();
                other.GetComponent<Collider>().enabled = false;
                Elevator.currentCollectableArea = other.GetComponent<Elevator>();
            }
            //**CollectorAsistant Layer
            else if (other.gameObject.layer == 13)
            {
                Destroy(other.gameObject);
                OnHitTriggerColletctorAsistant?.Invoke();
            }
            //** Chopper layer
            else if (other.gameObject.layer == 17)
            {
                OnChopperTriggerBox?.Invoke();

                MobileSpawner.currentmobileSpawner = other.GetComponent<MobileSpawner>();
            }

            //**BumpFinisline Layer
            else if (other.gameObject.layer == 18)
            {
                GameManager.OnEndGameStarted?.Invoke();
                other.GetComponent<Collider>().enabled = false;
            }
            //**BumpFinisline Layer
            else if (other.gameObject.layer == 21)
            {
                OnHitBumpEndLine.Invoke();
                other.GetComponent<Collider>().enabled = false;
            }
            // end game door layer
            else if (other.gameObject.layer == 22)
            {
                other.GetComponent<Collider>().enabled = false;
                other.GetComponent<Door>().OpenDoor();

            }

            // finishGorund
            else if (other.gameObject.layer == 23)
            {
                GameManager.OnLevelCompleted?.Invoke();
                other.GetComponent<Collider>().enabled = false;

            }
            // scoreGround
            else if (other.gameObject.layer == 25)
            {
                if (!GetComponent<PlayerMovement>().canMovingStartPosition)
                {
                    EarningManager.reachedDiamond = 0;
                    string _scoreText = other.GetComponent<GroundScore>().scoreText.text;
                    EarningManager.reachedDiamond = int.Parse(_scoreText);
                    other.GetComponent<Collider>().enabled = false;


                }
            }
            else if (other.gameObject.layer == 26)
            {
                other.GetComponent<Pyramid>().CreateMiniPyramids();
                other.GetComponent<Collider>().enabled = false;
            }

            //sphere
            else if (other.gameObject.layer == 27)
            {
                other.GetComponent<Sphere>().MoveSphereToPlayer();
                other.GetComponent<BoxCollider>().enabled = false;

            }
            //sphereToGround
            else if (other.gameObject.layer == 28)
            {
                other.GetComponent<SphereToGround>().MoveSphereToPGround();
                other.GetComponent<BoxCollider>().enabled = false;

            }
           

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.gameObject.layer == 27)
            {
                collision.transform.GetComponent<Sphere>().CreateMiniSpheres();
                collision.transform.GetComponent<SphereCollider>().enabled = false;
            }
        }
    }




}