using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScene.Settings
{
    public class SettingBtn:MonoBehaviour
    {
        public GameObject settingPage;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => settingPage.SetActive(true));
        }
    }
}