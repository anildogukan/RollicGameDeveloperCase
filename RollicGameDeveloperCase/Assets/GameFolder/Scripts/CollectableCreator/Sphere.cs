using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RollicDeveloperCase
{
    public class Sphere : MonoBehaviour
    {
        [SerializeField] private GameObject _miniSpheres;



        public void MoveSphereToPlayer()
        {
            GetComponent<Rigidbody>().velocity = Vector3.back * 10f;
        }
        public void CreateMiniSpheres()
        {
            GameObject pyramids = Instantiate(_miniSpheres, transform.position + Vector3.left, Quaternion.identity);
            pyramids.transform.parent = transform.parent;
            transform.GetComponent<MeshRenderer>().enabled = false;
           
        }
    }
}