using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollicDeveloperCase
{
    public class PoolArea : MonoBehaviour
    {
        
        [SerializeField] Elevator _elevator;
       
        
        private void OnCollisionEnter(Collision collision)
        {
            if (GameManager.Instance.gameState != GameState.GamePlay)
                return;
            if (collision.gameObject.layer==10)
            {
                _elevator.OnAnObjectCollected?.Invoke();
                collision.gameObject.layer = 15;
                _elevator.collectedObjects.Add(collision.gameObject);




            }
        }

      

    }
}