using DefaultNamespace.Statics;
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
            GetComponent<Button>().onClick.AddListener(DecreasePrice);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            tipWindow.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tipWindow.SetActive(false);
        }

        private void DecreasePrice()
        {
            if (LevelStatics.CurrentScore - _price < 0)
            {
                return;
            }
            LevelStatics.CurrentScore-=_price;
        }
    }
}