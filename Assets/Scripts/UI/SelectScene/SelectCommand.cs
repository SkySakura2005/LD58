using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.SelectScene
{
    public class SelectCommand:MonoBehaviour
    {
        public Button backButton;

        private void Awake()
        {
            backButton.onClick.AddListener(()=>SceneManager.LoadSceneAsync(0));
        }
    }
}