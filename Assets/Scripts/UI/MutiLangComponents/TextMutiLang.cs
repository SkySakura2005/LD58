using System;
using DefaultNamespace.Statics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MutiLangComponents
{
    
    public class TextMutiLang:MonoBehaviour
    {
        private Text text;
        public string chineseLangText;
        public string englishLangText;
        private void Awake()
        {
            text = GetComponent<Text>();
            switch (LevelStatics.CurrentLanguage)
            {
                case LanguageEnum.English:
                    text.text = englishLangText;
                    break;
                case LanguageEnum.Chinese:
                    text.text = chineseLangText;
                    break;
            }
        }
    }
}