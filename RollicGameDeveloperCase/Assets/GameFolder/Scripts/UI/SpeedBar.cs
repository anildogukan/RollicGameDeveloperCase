using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RollicDeveloperCase.Player;

namespace RollicDeveloperCase.UI
{
    public class SpeedBar : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField]private Image speedBarFiller;
        [SerializeField]private TextMeshProUGUI speedText;

        

        float lerpSpeed;
        const float lerpSpeedMultiplier=3f;


        private void Start()
        {
            _playerMovement = PlayerController.Instance.GetComponent<PlayerMovement>();
        }
        private void Update()
        {

            UpdateSpeedText();
            lerpSpeed = lerpSpeedMultiplier * Time.deltaTime;
            SpeedBarFiller();
            ColorChanger();
        }


        private void SpeedBarFiller()
        {
            speedBarFiller.fillAmount = Mathf.Lerp(speedBarFiller.fillAmount, _playerMovement.currentVelocity / _playerMovement.maxVelocity, lerpSpeed);
        }

        private void ColorChanger()
        {
            Color speedColor = Color.Lerp(Color.red, Color.green, (_playerMovement.currentVelocity / _playerMovement.maxVelocity));
            speedBarFiller.color = speedColor;

        }
        private void UpdateSpeedText()
        {
            int currentVelocity = Mathf.CeilToInt(_playerMovement.currentVelocity / _playerMovement.maxVelocity * 100);
            speedText.text = "%" + currentVelocity.ToString();
        }
    }

    
}