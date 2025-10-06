using DefaultNamespace.Statics.Interface;

namespace Statics.Implement
{
    public class Level6Data:ILevelData
    {
        public int CraftType { get; } = 5;
        public int UnlockedQueue { get; } = 2;
        public float MaxTime { get; } = 70;
        public int GoalScore { get; } = 100;
    }
}