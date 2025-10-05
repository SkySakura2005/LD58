
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

                transform.position = _startPosition;
            }
        }
        
    }
}