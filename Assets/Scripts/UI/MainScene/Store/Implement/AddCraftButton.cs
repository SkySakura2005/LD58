using DefaultNamespace.Statics;
using UI.MainScene.Store.Interface;

namespace UI.MainScene.Store.Implement
{
    public class AddCraftButton:IStoreType
    {
        public int Price { get; }
        public string Text { get; }
        public void ClickAction()
        {
            LevelStatics.maxCraftType++;
        }
    }
}