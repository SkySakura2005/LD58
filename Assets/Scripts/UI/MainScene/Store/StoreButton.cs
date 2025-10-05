using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.MainScene.Store
{
    public class StoreButton:MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        public GameObject tipWindow;
        public Text tipText;
        
        private int _price;

        public void Initialize()
        {
            
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            tipWindow.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tipWindow.SetActive(false);
        }
    }
}