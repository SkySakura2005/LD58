using DefaultNamespace.Statics.Interface;

namespace Statics.Implement
{
    public class Level9Data:ILevelData
    {
        public int CraftType { get; } = 5;
        public int UnlockedQueue { get; } = 2;
        public float MaxTime { get; } = 80;
        public int GoalScore { get; } = 500;
    }
}