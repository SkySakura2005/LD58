using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DefaultNamespace.Items;
using DefaultNamespace.Statics;
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
        
        private float[] _currentTime;
        private Sprite[] _blindBagAnim;
    
        void Start()
        {
            if (GeneratorsObjectList.Count!=maxGeneratorSize)
            {
                throw new UnityException("GeneratorsObjectList count is not equal to maxGeneratorSize");
            }
            unlockedGeneratorSize = LevelStatics.MaxUnlockedQueue;
            _currentTime = new float[maxGeneratorSize];
            _blindBagAnim = Resources.LoadAll<Sprite>("ArtAssets/Animation/Goods/BlindBag");
            UpdateView();
        }
        
        

        // Update is called once per frame
        void Update()
        {
            if (LevelStatics.MaxUnlockedQueue != unlockedGeneratorSize)
            {
                unlockedGeneratorSize = LevelStatics.MaxUnlockedQueue;
                UpdateView();
            }
            UpdateTimer();
        }
        
        private void UpdateTimer()
        {
            float perTime = Time.deltaTime;
            for (int i = 0; i < unlockedGeneratorSize; i++)
            {
                if (_currentTime[i] < 0)
                {
                    continue;
                }
                _currentTime[i] += perTime;
                if (_currentTime[i] >= maxGenerateTime)
                {
                    
                    BaseItem randomItem = RandomItem.GetRandomItem();
                    GameObject newItem=Instantiate(Resources.Load<GameObject>("Prefabs/DragItem"), GeneratorsObjectList[i].transform);
                    newItem.name=randomItem.CraftType+" "+randomItem.IPType;
                    newItem.GetComponent<DragItem>().Initialize(randomItem,gridObject,this,i);
                    
                    _currentTime[i] = -1;
                    StartCoroutine(OpenAnimation(newItem));
                }
                
            }
            
        }

        private IEnumerator OpenAnimation(GameObject newItem)
        {
            Image parentImage = newItem.transform.parent.gameObject.GetComponent<Image>();
            Image newItemImage = newItem.GetComponent<Image>();
            newItemImage.color = new Color(1, 1, 1, 0);
            newItem.GetComponent<DragItem>().enabled = false;
            for (int i = 0; i < _blindBagAnim.Length; i++)
            {
                parentImage.sprite = _blindBagAnim[i];
                yield return new WaitForSeconds(0.02f);
            }

            float alpha = 0;
            while (alpha < 1)
            {
                alpha += Time.deltaTime*4;
                newItemImage.color = new Color(1, 1, 1, alpha );
                yield return null;
            }
            newItem.GetComponent<DragItem>().enabled = true;
        }
        private void UpdateView()
        {
            for (int i = 0; i < GeneratorsObjectList.Count; i++)
            {
                if (i < unlockedGeneratorSize)
                {
                    GeneratorsObjectList[i].GetComponent<Image>().color=new Color32(255,255,255,255);
                }
                else
                {
                    GeneratorsObjectList[i].GetComponent<Image>().color=new Color(0.4f,0.4f,0.4f,1);
                }
            }
        }

        public IEnumerator ResetAnimation(int index)
        {
            for (int i = 0; i < 10; i++)
            {
                GeneratorsObjectList[index].transform.position+=new Vector3(40f,0f,0f);
                yield return new WaitForSeconds(1/(float)60);
            }
            GeneratorsObjectList[index].GetComponent<Image>().sprite = _blindBagAnim[0];
            for (int i = 0; i < 10; i++)
            {
                GeneratorsObjectList[index].transform.position-=new Vector3(40f,0f,0f);
                yield return new WaitForSeconds(1/(float)60);
            }
            _currentTime[index] = 0;
        }
    }
}
