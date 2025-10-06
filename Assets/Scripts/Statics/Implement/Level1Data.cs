using DefaultNamespace.Statics.Interface;

namespace Statics.Implement
{
    public class Level1Data:ILevelData
    {
        public int CraftType { get; } = 3;
        public int UnlockedQueue { get; } = 2;
        public float MaxTime { get; } = 30;
        public int GoalScore { get; } = 10;
    }
}