using DefaultNamespace.Statics.Interface;

namespace Statics.Implement
{
    public class Level4Data:ILevelData
    {
        public int CraftType { get; } = 4;
        public int UnlockedQueue { get; } = 2;
        public float MaxTime { get; }=60;
        public int GoalScore { get; } = 35;
    }
}