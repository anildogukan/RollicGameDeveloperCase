using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using RollicDeveloperCase.Player;


namespace RollicDeveloperCase
{
    public class Elevator : MonoBehaviour
    {

        [SerializeField] private TextMeshPro collectedNumberText;
        [SerializeField] private int totalCollectedCount;
        [SerializeField] private ElevatorGround _elevatorground;
        [SerializeField] private Door _door;

        public List<GameObject> collectedObjects = new List<GameObject>();
        [SerializeField] GameObject _piecesPrefab;
        Transform pieces;


        private int currentCollectedCount;        

        public Action OnAnObjectCollected;

        public static Elevator currentCollectableArea;    
        


        private void Start()
        {
            currentCollectedCount = 0;
            EarningManager.collectedDiamondInthisRound=0;
            collectedNumberText.text = currentCollectedCount + "" + "/" + totalCollectedCount;
        }
        private void IncreaseCollectedCount()
        {
            currentCollectedCount++;
            EarningManager.collectedDiamondInthisRound++;
            collectedNumberText.text = currentCollectedCount + "" + "/" + totalCollectedCount;            

        }

        private void OnEnable()
        {
            OnAnObjectCollected += IncreaseCollectedCount;
            PlayerMovement.OnCheckPassCondition += CheckPass;



        }
        private void OnDisable()
        {
            OnAnObjectCollected -= IncreaseCollectedCount;
            PlayerMovement.OnCheckPassCondition -= CheckPass;

        }


        private void CheckPass()
        {
            StartCoroutine(CheckPassConditions());
        }
        private IEnumerator CheckPassConditions()
        {
            if (this == currentCollectableArea)
            {
                if (currentCollectedCount < totalCollectedCount)
                    GameManager.OnLevelFailed?.Invoke();
                else
                {
                    _elevatorground.Move();
                    BreakIntoPiecesCollectedObject();

                    yield return new WaitForSeconds(1f);
                    _door.OpenDoor();
                    


                }
            }
          
        }
        private void BreakIntoPiecesCollectedObject()
        {
            Debug.Log("Girdi");
            foreach (GameObject collectedObject in this.collectedObjects)
            {
                pieces = Instantiate(_piecesPrefab.transform, collectedObject.transform.position, Quaternion.identity);
                pieces.transform.parent = transform;           

                collectedObject.SetActive(false);
                foreach (Transform child in pieces)
                {
                    if (child.transform.GetComponent<Rigidbody>() != null)
                    {                        
                        child.transform.GetComponent<Rigidbody>().AddExplosionForce(20f, child.transform.position , 5f);
                        pieces.transform.GetComponentInChildren<Collider>().enabled = false;
                    }
                }
            
            }
        }



    }


}

