using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainScene.Settings
{
    public class SettingPage:MonoBehaviour
    {
        public Button continueButton;
        public Button backButton;

        private void Awake()
        {
            continueButton.onClick.AddListener(() => gameObject.SetActive(false));
            backButton.onClick.AddListener(() => SceneManager.LoadSceneAsync(1));
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}