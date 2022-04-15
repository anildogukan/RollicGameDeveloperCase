using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RollicDeveloperCase.Player;
using TMPro;

namespace RollicDeveloperCase.UI
{
    public class GameCanvas : MonoBehaviour
    {
        [Header("GameCanvas Architecture")]
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject failScreen;
        [SerializeField] private GameObject playScreen;
        [SerializeField] private GameObject CompletedScreen;
        [SerializeField] private GameObject speedBar;

        [SerializeField] private TextMeshProUGUI diamondText_playScreen;
        [SerializeField] private TextMeshProUGUI diamondText_completedScreen;
        [SerializeField] private TextMeshProUGUI currentLevelText;
        [SerializeField] private TextMeshProUGUI nextLevelText;
        [SerializeField] private TextMeshProUGUI rewardText;
        [SerializeField] private TextMeshProUGUI CompletedRewardDiamondText;

        [SerializeField] private GameObject[] passedMiddleBox;
        [SerializeField] private GameObject rewardDiamonds;


        int currentStage;
        

        private void Awake()
        {
            startScreen.SetActive(true);
            playScreen.SetActive(false);
            currentStage = 0;
        }


        private void SetScreen_OnStart()
        {
            startScreen.SetActive(false);
            playScreen.SetActive(true);

        }

        private void SetScreen_OnFail()
        {
            playScreen.SetActive(false);
            failScreen.SetActive(true);
        }
        private void SetScreen_OnCompleted()
        {
            StartCoroutine(DelayOpenCompletedScreen());
        }
        private void CloseCompletedScreen()
        {
            CompletedScreen.SetActive(false);
            startScreen.SetActive(true);
        }
        private IEnumerator DelayOpenCompletedScreen()
        {

            yield return new WaitForSeconds(5.5f);
            playScreen.SetActive(false);
            CompletedScreen.SetActive(true);
        }
        private void OpenSpeedBar()
        {
            speedBar.SetActive(true);
        }
        private void CloseSpeedBar()
        {
            speedBar.SetActive(false);
        }
        private void OpenPassedMiddleBox()
        {
            StartCoroutine(DelayOpenPassedMiddleBox());
        }
        private IEnumerator  DelayOpenPassedMiddleBox()
        {
            yield return new WaitForSeconds(1f);
            passedMiddleBox[currentStage].SetActive(true);
            currentStage++;            
        }
        private void ClosePassedMiddleBox()
        {           
            for (int i = 0; i < passedMiddleBox.Length; i++)
            {
                passedMiddleBox[i].SetActive(false);
            }
            currentStage = 0;
        }

        private void WriteLevelText()
        {
            currentLevelText.text = (PlayerPrefs.GetInt("CurrentLevel") + 1).ToString();
            nextLevelText.text = (PlayerPrefs.GetInt("CurrentLevel") + 2).ToString();
        }

        private void WriteLevelTextOnEndGame()
        {
            currentLevelText.text = (PlayerPrefs.GetInt("CurrentLevel") +2).ToString();
            nextLevelText.text = (PlayerPrefs.GetInt("CurrentLevel") + 3).ToString();
        }

        private void WriteTotalDiamondOnStart()
        {
            diamondText_playScreen.text = PlayerPrefs.GetInt("TotalDiamond").ToString();
            diamondText_completedScreen.text = PlayerPrefs.GetInt("TotalDiamond").ToString();
        }
        private void UpdateCollectedDiamond()
        {
            diamondText_playScreen.text = (PlayerPrefs.GetInt("TotalDiamond")+EarningManager.collectedDiamondInthisRound).ToString();
            diamondText_completedScreen.text = (PlayerPrefs.GetInt("TotalDiamond")+EarningManager.collectedDiamondInthisRound).ToString();
        }

        private void WriteLevelCompletedRewarDiamondText()
        {
            CompletedRewardDiamondText.text = EarningManager.collectedDiamondInthisRound.ToString();
        }

        private void ShowReward()
        {
            StartCoroutine(showReachedText());
        }
        private IEnumerator showReachedText()
        {
            yield return new WaitForSeconds(1.5f);
            PlayerPrefs.SetInt("TotalDiamond", PlayerPrefs.GetInt("TotalDiamond") + EarningManager.reachedDiamond);
            rewardText.text = "+"+ EarningManager.reachedDiamond.ToString();
            rewardText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            rewardDiamonds.SetActive(true);
            yield return new WaitForSeconds(1f);
            diamondText_playScreen.text = PlayerPrefs.GetInt("TotalDiamond").ToString();
            diamondText_completedScreen.text = PlayerPrefs.GetInt("TotalDiamond").ToString();
            yield return new WaitForSeconds(1f);
            rewardText.gameObject.SetActive(false);
            rewardDiamonds.gameObject.SetActive(false);


        }


        private void OnEnable()
        {
            GameManager.OnLevelStarted += SetScreen_OnStart;
            GameManager.OnLevelStarted += WriteLevelText;
            GameManager.OnLevelStarted += WriteTotalDiamondOnStart;

            GameManager.OnLevelFailed += SetScreen_OnFail;

            GameManager.OnEndGameStarted += OpenSpeedBar;
            GameManager.OnEndGameStarted += WriteLevelTextOnEndGame;
            GameManager.OnEndGameStarted += ClosePassedMiddleBox;

            GameManager.OnLevelCompleted += SetScreen_OnCompleted;
            GameManager.OnLevelCompleted += ShowReward;
            GameManager.OnLevelCompleted += WriteLevelCompletedRewarDiamondText;

            GameManager.OnContinueLevel += CloseCompletedScreen;
            GameManager.OnContinueLevel += UpdateCollectedDiamond;
            PlayerCollisionController.OnHitBumpEndLine += CloseSpeedBar;

            PlayerMovement.OnCheckPassCondition += OpenPassedMiddleBox;


        }

        private void OnDisable()
        {
            GameManager.OnLevelStarted -= SetScreen_OnStart;
            GameManager.OnLevelStarted -= WriteLevelText;
            GameManager.OnLevelStarted -= WriteTotalDiamondOnStart;

            GameManager.OnLevelFailed -= SetScreen_OnFail;

            GameManager.OnEndGameStarted -= OpenSpeedBar;
            GameManager.OnEndGameStarted -= WriteLevelTextOnEndGame;
            GameManager.OnEndGameStarted -= ClosePassedMiddleBox;

            GameManager.OnLevelCompleted -= SetScreen_OnCompleted;
            GameManager.OnLevelCompleted -= ShowReward;
            GameManager.OnLevelCompleted += WriteLevelCompletedRewarDiamondText;

            GameManager.OnContinueLevel -= CloseCompletedScreen;
            GameManager.OnContinueLevel -= UpdateCollectedDiamond;
            PlayerCollisionController.OnHitBumpEndLine -= CloseSpeedBar;

            PlayerMovement.OnCheckPassCondition -= OpenPassedMiddleBox;
        }


        public void OnClickRestartButton()
        {
            GameManager.OnLevelRestart?.Invoke();
        }
        public void OnClickContinueButton()
        {
            GameManager.OnContinueLevel?.Invoke();
        }





    }


}
