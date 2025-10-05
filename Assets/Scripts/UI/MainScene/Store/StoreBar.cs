using System;
using System.Collections;
using UI.MainScene.Store.Implement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.MainScene.Store
{
    public class StoreBar:MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        
        public Vector2 openedPosition;
        public Vector2 closedPosition;
        
        public StoreButton addSpaceButton;
        public StoreButton addCraftButton;
        public StoreButton addQueueButton;
        
        private Coroutine _moveCoroutine;

        private void Start()
        {
            addSpaceButton.Initialize(new AddSpaceButton());
            addCraftButton.Initialize(new AddCraftButton());
            addQueueButton.Initialize(new AddQueueButton());
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(_moveCoroutine!=null) StopCoroutine(_moveCoroutine);
            _moveCoroutine=StartCoroutine(MoveCoroutine(openedPosition));
            Debug.Log("OnPointerEnter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(_moveCoroutine!=null) StopCoroutine(_moveCoroutine);
            _moveCoroutine=StartCoroutine(MoveCoroutine(closedPosition));
            Debug.Log("OnPointerExit");
        }

        private IEnumerator MoveCoroutine(Vector2 endPosition)
        {
            while (((Vector2)transform.position-endPosition).magnitude>0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position,endPosition,10);
                yield return null;
            }
        }
    }
}