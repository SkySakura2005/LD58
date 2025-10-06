using System;
using DefaultNamespace.Statics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.SelectScene
{
    public class SelectCommand:MonoBehaviour
    {
        public Button backButton;
        public Button[] selectButtons;
        private void Start()
        {
            backButton.onClick.AddListener(()=>SceneManager.LoadSceneAsync(0));
            for (int i = 0; i < selectButtons.Length; i++)
            {
                int index = i;
                if (i >= LevelStatics.CurrentLevel)
                {
                    selectButtons[i].interactable = false;
                    selectButtons[i].gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                else
                {
                    selectButtons[i].onClick.AddListener(()=>
                    {
                        LevelStatics.LoadLevels(LevelStatics.LevelsDatas[index]);
                        SceneManager.LoadSceneAsync(2);
                    });
                }
                
            }
        }
    }
}