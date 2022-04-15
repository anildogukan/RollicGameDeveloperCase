using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RollicDeveloperCase.Player;
using DG.Tweening;

namespace RollicDeveloperCase
{
    public  abstract class MobileSpawner : MonoBehaviour
    {

        [SerializeField] private float speed;
        [SerializeField] private float amplitude;
        [SerializeField] private float dropRate;

        [SerializeField] private GameObject collectablePrefab;

        [SerializeField] private float distance = 40f;
        [SerializeField] private float duration = 2.5f;

        float sinWaweValue;
        bool isCanMove;
        bool allowDrop;

        public static MobileSpawner currentmobileSpawner;


        private void OnEnable()
        {
            PlayerCollisionController.OnChopperTriggerBox += Move;
        }

        private void OnDisable()
        {
            PlayerCollisionController.OnChopperTriggerBox -= Move;
        }
        private void Awake()
        {
            isCanMove = false;
            allowDrop = false;
        }
        private void Update()
        {
            if (isCanMove)
            {
                MoveVertical();

                if (allowDrop)
                {
                    StartCoroutine(SetDropRateAndDrop());
                }
            }
        }

        private void Move()
        {
            if (this != currentmobileSpawner)
                return;
            StartCoroutine(SetSequenceOfMovementEvents());
            allowDrop = true;
        }        
        public virtual void MoveVertical()
        {
            sinWaweValue = Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = new Vector3(sinWaweValue, transform.position.y, transform.position.z);

        }
        public virtual IEnumerator SetSequenceOfMovementEvents()
        {
            isCanMove = true;
            transform.DOMoveZ(transform.position.z + distance, duration);

            yield return new WaitForSeconds(duration);
            isCanMove = false;
            transform.DOMoveX(transform.position.x + 20, 4f);
            transform.DOMoveY(transform.position.y + 60, 4f);

            yield return new WaitForSeconds(4f);
            Destroy(gameObject);
        }

        public virtual void SpawnObjects()
        {
            GameObject _collectable = Instantiate(collectablePrefab, transform.position, Quaternion.identity);
            _collectable.transform.parent = transform.parent;
            allowDrop = false;
        }
        private IEnumerator SetDropRateAndDrop()
        {
            SpawnObjects();
            yield return new WaitForSeconds(dropRate);
            allowDrop = true;
        }
    }
}
