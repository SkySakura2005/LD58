using DefaultNamespace.Statics.Interface;

namespace Statics.Implement
{
    public class Level8Data:ILevelData
    {
        public int CraftType { get; } = 5;
        public int UnlockedQueue { get; } = 3;
        public float MaxTime { get; } = 80;
        public int GoalScore { get; } = 150;
    }
}