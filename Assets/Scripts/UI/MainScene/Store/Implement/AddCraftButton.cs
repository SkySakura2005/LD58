using DefaultNamespace.Statics;
using UI.MainScene.Store.Interface;

namespace UI.MainScene.Store.Implement
{
    public class AddCraftButton:IStoreType
    {
        public int Price { get; } = 100000000;
        public string Text { get; }
        public bool ClickAction()
        {
            if (LevelStatics.CurrentScore-Price<0||LevelStatics.MaxCraftType==5)
            {
                return false;
            }
            LevelStatics.MaxCraftType++;
            LevelStatics.CurrentScore-=Price;
            return true;
        }
    }
}