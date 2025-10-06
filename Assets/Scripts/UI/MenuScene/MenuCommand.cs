using System;
using System.IO;
using DefaultNamespace.Statics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MenuScene
{
    public class MenuCommand:MonoBehaviour
    {
        public Button startButton;
        public Button exitButton;

        private void Awake()
        {
            startButton.onClick.AddListener(()=>SceneManager.LoadSceneAsync(1));
            exitButton.onClick.AddListener(Application.Quit);
        }

        private void Start()
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath , "levelData.txt")))
            {
                File.WriteAllText(Path.Combine(Application.persistentDataPath , "levelData.txt"), "00");
            }
            else
            {
                string config=File.ReadAllText(Application.persistentDataPath + "/levelData.txt");
                if (config[0] == '0')
                {
                    LevelStatics.isNew = true;
                }
                else
                {
                    LevelStatics.isNew = false;
                }
                LevelStatics.MaxLevel=config[1]-'0'+1;
            }
        }
    }
}