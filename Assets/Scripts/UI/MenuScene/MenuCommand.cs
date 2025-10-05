using System;
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
    }
}