namespace UI.MainScene.Store.Interface
{
    public interface IStoreType
    {
        int Price { get; }
        string Text { get; }
        bool ClickAction();
    }
}