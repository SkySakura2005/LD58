
using System.Collections;
using DefaultNamespace.Items;

using UI.MainScene;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class DragItem:MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        public BaseItem CurrentItem;
        private GridObject _gridObject;
        private Vector3 _startPosition;
        private bool _isInQueue;

        private GameObject parent;

        public void Initialize(BaseItem item,GridObject grid)
        {
            CurrentItem = item;
            _gridObject = grid;
            GetComponent<Image>().sprite=CurrentItem.ItemSprite;
            GetComponent<RectTransform>().sizeDelta=CurrentItem.ItemSprite.rect.size;
            _isInQueue = true;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_isInQueue)
            {
                parent=transform.parent.gameObject;
                GameObject newGameObject=new GameObject
                {
                    transform =
                    {
                        parent = parent.transform
                    }
                };
                transform.SetParent(GameObject.Find("Canvas").transform);
                _startPosition = transform.position;
            }
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_isInQueue)
            {
                
                transform.position = eventData.position;
            }
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isInQueue)
            {
                int row, col;
                _gridObject.GetIndex(eventData.position, out row, out col);
                if (row != -1 && col != -1)
                {
                    _gridObject.AddToGrid(row, col, this, out var standardPos);
                    _gridObject.CheckCondition(row, col);
                    _startPosition = standardPos;
                    _isInQueue = false;
                }
                else
                {
                    transform.SetParent( parent.transform);
                }
                Destroy(parent.transform.GetChild(0).gameObject);
                transform.position = _startPosition;
            }
        }

        public IEnumerator ClearCoroutine()
        {
            Image clearImage = GetComponent<Image>();
            Debug.Log(CurrentItem.ClearAnimation.Length);
            for (int i = 0; i < CurrentItem.ClearAnimation.Length; i++)
            {
                clearImage.sprite = CurrentItem.ClearAnimation[i];
                clearImage.rectTransform.sizeDelta = CurrentItem.ClearAnimation[i].rect.size;
                yield return new WaitForSeconds(0.04f);
            }
            Destroy(gameObject);
        }
    }
}