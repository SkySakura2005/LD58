using DefaultNamespace.Statics;
using UI.MainScene.Store.Interface;

namespace UI.MainScene.Store.Implement
{
    public class AddQueueButton:IStoreType
    {
        public int Price { get; } = 10;
        public string Text { get; }
        public bool ClickAction()
        {
            if (LevelStatics.CurrentScore-Price<0||LevelStatics.MaxUnlockedQueue==4)
            {
                return false;
            }
            LevelStatics.MaxUnlockedQueue++;
            LevelStatics.CurrentScore-=Price;
            return true;
        }
    }
}