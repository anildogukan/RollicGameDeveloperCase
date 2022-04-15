using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RollicDeveloperCase.Player;

namespace RollicDeveloperCase.Camera
{
    public class CameraController : MonoBehaviour
    {
        [Range(0,1)]
        [SerializeField] private float smoothFactor;


        Vector3 offset;       
        Transform player;

        Vector3 desiredPosition;
        


        private void Start()
        {           
            player = PlayerController.Instance.transform;
            offset = transform.position - player.position;
        }

        private void LateUpdate()
        {           
            
          desiredPosition= player.position+ offset;

          transform.position = Vector3.Lerp(transform.position,
          new Vector3(transform.position.x, desiredPosition.y, desiredPosition.z), smoothFactor);
           

        }

    }

    
}
