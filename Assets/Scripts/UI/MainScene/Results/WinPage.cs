using DefaultNamespace.Statics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainScene.Results
{
    public class WinPage:MonoBehaviour
    {
        public Text scoreText;
        public Button nextButton;
        public Button backButton;

        private void Start()
        {
            nextButton.onClick.AddListener(() =>
            {
                if (LevelStatics.CurrentLevel < 10)
                {
                    LevelStatics.CurrentLevel++;
                    LevelStatics.LoadLevels(LevelStatics.LevelsDatas[LevelStatics.CurrentLevel]);
                }
                SceneManager.LoadSceneAsync(2);
            });
            backButton.onClick.AddListener(() =>
            {
                if (LevelStatics.CurrentLevel < 10)
                {
                    LevelStatics.CurrentLevel++;
                }
                SceneManager.LoadSceneAsync(1);
            });
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