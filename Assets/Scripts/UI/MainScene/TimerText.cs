using System;
using DefaultNamespace.Statics;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TimerText:MonoBehaviour
    {
        private Text _text;
        private float _timer;

        public GameObject winPage;
        public GameObject losePage;

        private void Start()
        {
            _text = GetComponent<Text>();
            _timer = LevelStatics.MaxTime;
        }

        private void Update()
        {
            if (_timer <= 0)
            {
                if (LevelStatics.CurrentScore >= LevelStatics.GoalScore)
                {
                    winPage.SetActive(true);
                }
                else
                {
                    losePage.SetActive(true);
                }
            }
            _text.text = ((int)_timer).ToString();
            _timer -= Time.deltaTime;
        }
    }
}