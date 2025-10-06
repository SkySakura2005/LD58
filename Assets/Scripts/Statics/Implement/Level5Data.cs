using DefaultNamespace.Statics.Interface;

namespace Statics.Implement
{
    public class Level5Data:ILevelData
    {
        public int CraftType { get; } = 5;
        public int UnlockedQueue { get; }=3;
        public float MaxTime { get; } = 60;
        public int GoalScore { get; } = 50;
    }
}