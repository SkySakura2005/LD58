using System.Collections.Generic;
using System.Threading;
using DefaultNamespace.Items;
using UI.MainScene;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Generator : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public int maxGeneratorSize;
        public int unlockedGeneratorSize;
        public List<GameObject> GeneratorsObjectList;
        public float maxGenerateTime;

        public GridObject gridObject;
        
        private float currentTime;
        
    
        void Start()
        {
            if (GeneratorsObjectList.Count!=maxGeneratorSize)
            {
                throw new UnityException("GeneratorsObjectList count is not equal to maxGeneratorSize");
            }
            UpdateView();
        }
        
        

        // Update is called once per frame
        void Update()
        {
            UpdateTimer();
        }
        
        private void UpdateTimer()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= maxGenerateTime)
            {
                BaseItem randomItem = RandomItem.GetRandomItem();
                for (int i = 0; i < unlockedGeneratorSize; i++)
                {
                    if (GeneratorsObjectList[i].transform.childCount == 0)
                    {
                        GameObject newItem=Instantiate(Resources.Load<GameObject>("Prefabs/DragItem"), GeneratorsObjectList[i].transform);
                        newItem.name=randomItem.CraftType+" "+randomItem.IPType;
                        newItem.GetComponent<DragItem>().Initialize(randomItem,gridObject);
                        break;
                    }
                }
                currentTime = 0;
            }
        }

        public void UpdateView()
        {
            for (int i = 0; i < GeneratorsObjectList.Count; i++)
            {
                if (i < unlockedGeneratorSize)
                {
                    GeneratorsObjectList[i].GetComponent<Image>().color=new Color32(255,255,255,255);
                }
                else
                {
                    GeneratorsObjectList[i].GetComponent<Image>().color=new Color32(255,100,100,255);
                }
            }
        }
    }
}
