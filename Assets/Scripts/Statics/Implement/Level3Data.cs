using DefaultNamespace.Statics.Interface;

namespace Statics.Implement
{
    public class Level3Data:ILevelData
    {
        public int CraftType { get; } = 4;
        public int UnlockedQueue { get; } = 2;
        public float MaxTime { get; }=1;
        public int GoalScore { get; } = 0;
    }
}