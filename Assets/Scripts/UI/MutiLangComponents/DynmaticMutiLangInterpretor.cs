using System.Collections.Generic;
using DefaultNamespace.Statics;
using UnityEngine;

namespace UI.MutiLangComponents
{
    public class LanguageTextElement
    {
        public LanguageTextElement(string chn, string eng)
        {
            chineseText = chn;
            englishText = eng;
        }
        public string chineseText;
        public string englishText;
    }
    public class DynmaticMutiLangInterpretor
    {
        private static Dictionary<string, LanguageTextElement> languageTextElements =
            new Dictionary<string, LanguageTextElement>
            {
                { "STORETIPBOX_SUCCESS", new LanguageTextElement("购买成功！", "Succeed!") },
                { "STORETIPBOX_FAIL", new LanguageTextElement("购买失败！", "Fail!") }
            };

        public string GetDynmaticMutiLang(string langKey)
        {
            if (!languageTextElements.ContainsKey(langKey))
            {
                throw new UnityException("The MutiLangKey '"+langKey+"' is not exist!");
            }
            LanguageTextElement element = languageTextElements[langKey];
            switch (LevelStatics.CurrentLanguage)
            {
                case LanguageEnum.English:
                    return element.englishText;
                case LanguageEnum.Chinese:
                    return element.chineseText;
            }
            return element.englishText;
        }
    }
}