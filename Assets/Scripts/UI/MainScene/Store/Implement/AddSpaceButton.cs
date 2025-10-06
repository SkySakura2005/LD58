using DefaultNamespace.Statics;
using UI.MainScene.Store.Interface;
using UnityEngine;

namespace UI.MainScene.Store.Implement
{
    public class AddSpaceButton:IStoreType,ISpaceButton
    {
        public int Price { get; } = 10;
        public string Text { get; }
        
        public GameObject unlockPage { get; set; }
        public GridObject gridObject { get; set; }
        public bool ClickAction()
        {
            if (LevelStatics.CurrentScore-Price<0)
            {
                return false;
            }

            GridObject gridObject = GameObject.Find("Canvas/GridObject").GetComponent<GridObject>();
            for (int i = 0; i < gridObject.UnlockedGrid.GetLength(0); i++)
            {
                for (int j = 0; j < gridObject.UnlockedGrid.GetLength(1); j++)
                {
                    if (!gridObject.UnlockedGrid[i, j])
                    {
                        unlockPage.SetActive(true);
                        return true;
                    }
                }
            }
            return false;
        }

        
    }
}