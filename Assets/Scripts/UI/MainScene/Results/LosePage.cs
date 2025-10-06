using System;
using Audio;
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
            restartButton.onClick.AddListener(() =>
            {
                LevelStatics.LoadLevels(LevelStatics.LevelsDatas[LevelStatics.CurrentLevel-1]);
                SceneManager.LoadSceneAsync(2);
            });
            backButton.onClick.AddListener(() => SceneManager.LoadSceneAsync(1));
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
            scoreText.text = LevelStatics.CurrentScore.ToString();
            AudioManager.Instance.PlaySound("Lose");
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}