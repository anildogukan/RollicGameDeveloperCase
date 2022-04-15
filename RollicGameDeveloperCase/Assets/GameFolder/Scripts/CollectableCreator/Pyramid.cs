using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollicDeveloperCase
{
    public class Pyramid : MonoBehaviour
    {
        [SerializeField] private GameObject _miniPyramids;

        public void CreateMiniPyramids()
        {
          GameObject pyramids=  Instantiate(_miniPyramids, transform.position+Vector3.left, Quaternion.identity);
            pyramids.transform.parent = transform.parent;
            transform.GetComponent<MeshRenderer>().enabled = false;
            foreach (Transform child in pyramids.transform)
            {
                if (child.transform.GetComponent<Rigidbody>() != null)
                {
                    child.transform.GetComponent<Rigidbody>().AddForce(Vector3.up*10f);
                    
                }
            }
        }
    }
}