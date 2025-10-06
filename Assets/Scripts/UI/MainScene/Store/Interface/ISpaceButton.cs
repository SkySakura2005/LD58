using UnityEngine;

namespace UI.MainScene.Store.Interface
{
    public interface ISpaceButton
    {
        GameObject unlockPage { get; set; }
        GridObject gridObject { get; set; }
    }
}