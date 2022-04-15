using System.Collections.Generic;
using UnityEngine;
using RollicDeveloperCase.Player;
using UnityEngine.SceneManagement;

namespace RollicDeveloperCase
{
    public class LevelRegulator : MonoBehaviour
    {
        [SerializeField] GameObject[] level;
        public List<GameObject> createdLevels = new List<GameObject>();

        const float levelDistance = 582.8f;
        float levedistanceMultiplier;
        [SerializeField] private GameObject _playerPrefab;

        GameObject _player;

        int randomLevel;
        int randomLevelNext;

        private void Awake()
        {
            _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
            levedistanceMultiplier = 0;
            CreatingFirstTwoLevels();
        }

        private void CreatingFirstTwoLevels()
        {
            if (PlayerPrefs.GetInt(("CurrentLevel")) < level.Length - 1)
            {
                GameObject firstLevel = Instantiate(level[PlayerPrefs.GetInt("CurrentLevel")], new Vector3(0, 0, levelDistance * levedistanceMultiplier), Quaternion.identity);
                createdLevels.Add(firstLevel);
                levedistanceMultiplier++;
                GameObject SecondLevel = Instantiate(level[PlayerPrefs.GetInt("CurrentLevel") + 1], new Vector3(0, 0, levelDistance * levedistanceMultiplier), Quaternion.identity);
                createdLevels.Add(SecondLevel);
            }
            else
            {
                GameObject firstLevel = Instantiate(level[PlayerPrefs.GetInt(("CurrentCreatedLevel"))], new Vector3(0, 0, levelDistance * levedistanceMultiplier), Quaternion.identity);
                createdLevels.Add(firstLevel);
                levedistanceMultiplier++;
                randomLevel = Random.Range(0, level.Length);
                while (randomLevel == PlayerPrefs.GetInt(("CurrentCreatedLevel")))
                {
                    randomLevel = Random.Range(0, level.Length);
                }
                GameManager.isGameOpenNew = true;

                GameObject SecondLevel = Instantiate(level[randomLevel], new Vector3(0, 0, levelDistance * levedistanceMultiplier), Quaternion.identity);
                createdLevels.Add(SecondLevel);


            }
        }       
        private void GoNextlevel()
        {
            levedistanceMultiplier++;
            if (PlayerPrefs.GetInt(("CurrentLevel")) < level.Length - 2)
            {
                PlayerPrefs.SetInt(("CurrentLevel"), PlayerPrefs.GetInt("CurrentLevel") + 1);
                GameObject newLevel = Instantiate(level[PlayerPrefs.GetInt("CurrentLevel") + 1],
                 new Vector3(0, 0, levelDistance * levedistanceMultiplier), Quaternion.identity);
                Destroy(createdLevels[0]);
                createdLevels.Remove(createdLevels[0]);
                createdLevels.Add(newLevel);

            }
            else
            {
                randomLevelNext = Random.Range(0, level.Length);
                while (randomLevelNext == randomLevel)
                {

                    randomLevelNext = Random.Range(0, level.Length);
                    Debug.Log("Random" + randomLevelNext);
                }

                PlayerPrefs.SetInt(("CurrentLevel"), PlayerPrefs.GetInt("CurrentLevel") + 1);
                GameObject newLevel = Instantiate(level[randomLevelNext], new Vector3(0, 0, levelDistance * levedistanceMultiplier), Quaternion.identity);
                Destroy(createdLevels[0]);
                createdLevels.Remove(createdLevels[0]);
                createdLevels.Add(newLevel);

            }


        }
        private void FindRestartScene()
        {
            if (GameManager.isGameOpenNew == true)
            {
                PlayerPrefs.SetInt("CurrentCreatedLevel", randomLevel);
            }
            if (!GameManager.isGameOpenNew)
            {
                PlayerPrefs.SetInt("CurrentCreatedLevel", randomLevelNext);
            }
            GameManager.isGameOpenNew = false;
        }
        private void RunPlayerMovingOnEndGame()
        {
            StartCoroutine(_player.GetComponent<PlayerMovement>().MoveToStartPosition(new Vector3(0, 0, levelDistance * levedistanceMultiplier)));
        }
        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void OnEnable()
        {
            GameManager.OnLevelCompleted += RunPlayerMovingOnEndGame;
            GameManager.OnContinueLevel += GoNextlevel;
            GameManager.OnLevelRestart += RestartLevel;
            GameManager.OnEndGameStarted += FindRestartScene;
        }
        private void OnDisable()
        {
            GameManager.OnLevelCompleted -= RunPlayerMovingOnEndGame;
            GameManager.OnContinueLevel -= GoNextlevel;
            GameManager.OnLevelRestart -= RestartLevel;
            GameManager.OnEndGameStarted -= FindRestartScene;
        }
    }

}
