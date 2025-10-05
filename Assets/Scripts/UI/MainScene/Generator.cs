using System.Collections.Generic;
using System.Threading;
using DefaultNamespace.Items;
using UI.MainScene;
using UnityEngine;

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
        
        private int currentGeneratorSize;
        private float currentTime;
        
    
        void Start()
        {
            currentGeneratorSize = unlockedGeneratorSize;
            if (GeneratorsObjectList.Count!=maxGeneratorSize)
            {
                throw new UnityException("GeneratorsObjectList count is not equal to maxGeneratorSize");
            }
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
                foreach (GameObject parent in GeneratorsObjectList)
                {
                    if (parent.transform.childCount == 0)
                    {
                        GameObject newItem=Instantiate(Resources.Load<GameObject>("Prefabs/DragItem"), parent.transform);
                        newItem.name=randomItem.CraftType+" "+randomItem.IPType;
                        newItem.GetComponent<DragItem>().Initialize(randomItem,gridObject);
                        break;
                    }
                }
                currentTime = 0;
            }
        }
    }
}
