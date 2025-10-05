using System.Collections.Generic;
using DefaultNamespace.Statics.Interface;
using UnityEngine;

namespace DefaultNamespace.Statics
{
    public static class LevelStatics
    {
        public static bool isNew = true;
        
        public static int CurrentLevel = 1;
        public static ILevelData[] LevelsDatas=new ILevelData[10];
        
        public static int MaxCraftType = 3;
        public static int MaxUnlockedQueue = 2;
        public static float MaxTime = 60;
        public static int GoalScore = 100;
        public static int CurrentScore = 0;

        public static void LoadLevels(ILevelData data)
        {
            MaxCraftType = data.CraftType;
            MaxUnlockedQueue = data.UnlockedQueue;
            MaxTime = data.MaxTime;
            GoalScore = data.GoalScore;
            CurrentScore = 0;
        }
    }
}