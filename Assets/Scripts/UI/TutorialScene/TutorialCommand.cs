using System;
using DefaultNamespace.Statics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.TutorialScene
{
    public class TutorialCommand:MonoBehaviour
    {
        public Sprite[] backgroundSprite;
        public Image backgroundImage;

        public Button nextBtn;
        public Button prevBtn;
        
        private int index = 0;
        private void Start()
        {
            backgroundImage.sprite = backgroundSprite[0];
            backgroundImage.color = Color.white;
            nextBtn.onClick.AddListener(() =>
            {
                if (index < backgroundSprite.Length - 1)
                {
                    backgroundImage.sprite = backgroundSprite[index + 1];
                    index++;
                }
                else
                {
                    LevelStatics.CurrentLevel = 1;
                    LevelStatics.LoadLevels(LevelStatics.LevelsDatas[0]);
                    SceneManager.LoadSceneAsync(2);
                }
            });
            prevBtn.onClick.AddListener(() =>
            {
                if (index > 0)
                {
                    backgroundImage.sprite = backgroundSprite[index - 1];
                    index--;
                }
            });
        }
    }
}