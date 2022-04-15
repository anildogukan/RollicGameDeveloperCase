using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollicDeveloperCase
{
    public class EarningManager : MonoBehaviour
    {
        private static int totalDiamond;
        public static int reachedDiamond;
        public static int collectedDiamondInthisRound;

        public static Action OnIncreaseDiamond;

        private void Start()
        {
            totalDiamond = PlayerPrefs.GetInt("TotalDiamond");            
        }

        public void IncreaseDiamondOnContinueButton()
        {
            PlayerPrefs.SetInt("TotalDiamond", PlayerPrefs.GetInt("TotalDiamond") + collectedDiamondInthisRound);
            totalDiamond = PlayerPrefs.GetInt("TotalDiamond");
        }

        private void OnEnable()
        {
            GameManager.OnContinueLevel += IncreaseDiamondOnContinueButton;
        }
        private void OnDisable()
        {
            GameManager.OnContinueLevel -= IncreaseDiamondOnContinueButton;
        }

        
    }

    
}
