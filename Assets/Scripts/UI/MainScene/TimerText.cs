using System;
using DefaultNamespace.Statics;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TimerText:MonoBehaviour
    {
        private Text text;
        private float timer;

        private void Start()
        {
            text = GetComponent<Text>();
            timer = LevelStatics.MaxTime;
        }

        private void Update()
        {
            if (timer <= 0)
            {
                
            }
            text.text = ((int)timer).ToString();
            timer -= Time.deltaTime;
        }
    }
}