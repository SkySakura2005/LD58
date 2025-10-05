using UI.MainScene.Store.Interface;

namespace UI.MainScene.Store.Implement
{
    public class AddSpaceButton:IStoreType
    {
        public int Price { get; }
        public string Text { get; }
        public bool ClickAction()
        {
            return true;
        }
    }
}