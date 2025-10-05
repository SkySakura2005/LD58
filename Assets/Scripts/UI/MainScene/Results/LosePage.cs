using System;
using DefaultNamespace.Statics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainScene.Results
{
    public class LosePage:MonoBehaviour
    {
        public Text scoreText;
        public Button restartButton;
        public Button backButton;

        private void Start()
        {
            restartButton.onClick.AddListener(() => SceneManager.LoadSceneAsync(2));
            backButton.onClick.AddListener(() => SceneManager.LoadSceneAsync(1));
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
            scoreText.text = LevelStatics.CurrentScore.ToString();
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}