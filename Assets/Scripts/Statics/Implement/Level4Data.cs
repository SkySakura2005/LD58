using DefaultNamespace.Statics.Interface;

namespace Statics.Implement
{
    public class Level4Data:ILevelData
    {
        public int CraftType { get; }
        public int UnlockedQueue { get; }
        public float MaxTime { get; }
        public int GoalScore { get; }
    }
}