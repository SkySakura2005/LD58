using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScene.Store
{
    public class UnlockPage:MonoBehaviour
    {
        public GridObject gridObject;
        public GameObject localGridObject;
        private void OnEnable()
        {
            Time.timeScale = 0;
            for (int i = 0; i < localGridObject.transform.childCount; i++)
            {
                int index = i;
                if (gridObject.UnlockedGrid[i / 5, i %5])
                {
                    localGridObject.transform.GetChild(i).GetComponent<Button>().interactable = false;
                    localGridObject.transform.GetChild(i).GetComponent<Image>().color = new Color(0,0,0,0);
                }
                else
                {
                    localGridObject.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() =>
                    {
                        gridObject.UnlockedGrid[index / 5, index %5]=true;
                        gridObject.UpdateUnlockedGrid();
                        gameObject.SetActive(false);
                    });
                }
            }
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}