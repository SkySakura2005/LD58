using System;
using DefaultNamespace.Statics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScene
{
    public class ScoreText:MonoBehaviour
    {
        private Text text;
        private void Start()
        {
            text = GetComponent<Text>();
        }

        private void Update()
        {
            if (LevelStatics.CurrentScore >= LevelStatics.GoalScore)
            {
                
            }
            text.text=LevelStatics.CurrentScore.ToString();
            
        }
    }
}