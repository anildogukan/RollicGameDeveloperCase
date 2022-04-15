using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollicDeveloperCase {
    public class SphereToGround : MonoBehaviour
    {
        [SerializeField] private GameObject _miniSpheres;
        private void OnCollisionEnter(Collision collision)
        {
            CreateMiniSpheres(collision);
            
        }
        private void CreateMiniSpheres(Collision collision)
        {
            GameObject miniSpheres = Instantiate(_miniSpheres, transform.position, Quaternion.identity);
            miniSpheres.transform.parent = transform.parent;
            transform.GetComponent<MeshRenderer>().enabled = false;
            transform.GetComponent<SphereCollider>().enabled = false;

        }
        public void MoveSphereToPGround()
        {
            GetComponent<Rigidbody>().velocity = Vector3.down * 10f;
        }
    }
}