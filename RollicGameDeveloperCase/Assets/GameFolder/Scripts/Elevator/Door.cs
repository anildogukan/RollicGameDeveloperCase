using System.Collections;
using UnityEngine;

namespace RollicDeveloperCase
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject rightConfetti;
        [SerializeField] private GameObject leftConfetti;
        [SerializeField] private GameObject[] textForDoor;       
        

        private void Start()
        {
            GetComponent<Animator>().enabled = false;
        }
        public void OpenDoor()
        {
            GetComponent<Animator>().enabled = true;
            PlayConfetties();
            StartCoroutine(ShowText());
        }


        private void PlayConfetties()
        {
            rightConfetti.SetActive(true);
            leftConfetti.SetActive(true);
            
        }

        private IEnumerator ShowText()
        {
            int textCount;

            textCount = Random.Range(0, textForDoor.Length);
            
            textForDoor[textCount].SetActive(true);
            yield return new WaitForSeconds(0.8f);
            textForDoor[textCount].SetActive(false);

        }


    }
}
