using DefaultNamespace.Statics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MutiLangComponents
{
    public class ImageMutiLang:MonoBehaviour
    {
        private Image image;
        public Sprite chineseLangImage;
        public Sprite englishLangImage;
        private void Awake()
        {
            image = GetComponent<Image>();
            switch (LevelStatics.CurrentLanguage)
            {
                case LanguageEnum.English:
                    image.sprite = englishLangImage;
                    break;
                case LanguageEnum.Chinese:
                    image.sprite = chineseLangImage;
                    break;
            }
        }
    }
}