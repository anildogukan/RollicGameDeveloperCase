using System;

namespace RollicDeveloperCase
{
    public enum GameState {StartScreen,GamePlay,Failed,EndGame,Completed }
    public class GameManager : SingletonPersistant<GameManager>
    {       

        public static Action OnLevelFailed;
        public static Action OnLevelCompleted;
        public static Action OnEndGameStarted;

        public static Action OnLevelStarted;
        public static Action OnLevelRestart;
        public static Action OnContinueLevel;

        public GameState gameState;

        public static bool isGameOpenNew;

        private void OnEnable()
        {
            OnLevelStarted += StartLevel;         
            OnLevelFailed += FailedLevel;
            OnLevelRestart += RestartLevel;
            OnEndGameStarted += StartEndGame;
            OnLevelCompleted += CompleteLevel;
            OnContinueLevel += ContinueLevel;

        }
        private void OnDisable()
        {
            OnLevelStarted -= StartLevel;
            OnLevelFailed -= FailedLevel;
            OnLevelRestart -= RestartLevel;
            OnEndGameStarted -= StartEndGame;
            OnLevelCompleted -= CompleteLevel;
            OnContinueLevel -= ContinueLevel;
        }           

        private void StartLevel()
        {
            gameState = GameState.GamePlay;
        }

        private void FailedLevel()
        {
            gameState = GameState.Failed;
        }
        private void StartGamePlay()
        {
            gameState = GameState.GamePlay;
        }
        private void StartEndGame()
        {
            gameState = GameState.EndGame;
        }
        private void CompleteLevel()
        {
            gameState = GameState.Completed;
        }

        private void RestartLevel()
        {           
            gameState = GameState.StartScreen;
        }
        private void ContinueLevel()
        {             
            gameState = GameState.StartScreen;
        }



    }



}