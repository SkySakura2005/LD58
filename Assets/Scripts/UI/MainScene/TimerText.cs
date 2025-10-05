using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TimerText:MonoBehaviour
    {
        private Text text;

        private void Start()
        {
            text = GetComponent<Text>();
        }

        private void Update()
        {
            
        }
    }
}