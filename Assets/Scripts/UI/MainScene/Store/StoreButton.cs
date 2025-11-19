using DefaultNamespace.Statics;
using UI.MainScene.Store.Interface;
using UI.MutiLangComponents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.MainScene.Store
{
    public class StoreButton:MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        public GameObject unlockPage;
        public GridObject gridObject;
        
        public GameObject tipWindow;
        public Text tipText;
        
        public TipBox tipBox;
        
        private int _price;

        public void Initialize(IStoreType storeType)
        {
            _price = storeType.Price;
            tipText = tipWindow.GetComponent<Text>();
            GetComponent<Button>().onClick.AddListener(() =>
            {
                DynmaticMutiLangInterpretor interpretor = new DynmaticMutiLangInterpretor();
                if (storeType is ISpaceButton)
                {
                    ((ISpaceButton)storeType).gridObject=gridObject;
                    ((ISpaceButton)storeType).unlockPage = unlockPage;
                }
                if (storeType.ClickAction())
                {
                    tipBox.MakeMove(interpretor.GetDynmaticMutiLang("STORETIPBOX_SUCCESS"));
                }
                else
                {
                    tipBox.MakeMove(interpretor.GetDynmaticMutiLang("STORETIPBOX_FAIL"));
                }
            }); 
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