using System.IO;
using Audio;
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
                if (LevelStatics.CurrentLevel == LevelStatics.MaxLevel&&LevelStatics.MaxLevel<10)
                {
                    LevelStatics.CurrentLevel++;
                    LevelStatics.MaxLevel++;
                    LevelStatics.LoadLevels(LevelStatics.LevelsDatas[LevelStatics.CurrentLevel-1]);
                    SceneManager.LoadSceneAsync(2);
                }
                else if(LevelStatics.CurrentLevel != LevelStatics.MaxLevel)
                {
                    LevelStatics.CurrentLevel++;
                    LevelStatics.LoadLevels(LevelStatics.LevelsDatas[LevelStatics.CurrentLevel-1]);
                    SceneManager.LoadSceneAsync(2);
                }
                else if (LevelStatics.MaxLevel == 10)
                {
                    SceneManager.LoadSceneAsync(1);
                }
            });
            backButton.onClick.AddListener(() =>
            {
                if (LevelStatics.CurrentLevel == LevelStatics.MaxLevel&&LevelStatics.MaxLevel<10)
                {
                    LevelStatics.MaxLevel++;
                }
                string config="1"+(LevelStatics.MaxLevel-1).ToString();
                File.WriteAllText(Path.Combine(Application.persistentDataPath , "levelData.txt"), config);
                SceneManager.LoadSceneAsync(1);
            });
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
            scoreText.text = LevelStatics.CurrentScore.ToString();
            AudioManager.Instance.PlaySound("Win");
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}