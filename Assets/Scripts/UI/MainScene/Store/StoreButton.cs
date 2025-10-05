using UI.MainScene.Store.Interface;
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

        public void Initialize(IStoreType storeType)
        {
            _price = storeType.Price;
            tipText = tipWindow.GetComponent<Text>();
            GetComponent<Button>().onClick.AddListener(storeType.ClickAction);
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