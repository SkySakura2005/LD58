using UnityEngine;

namespace DefaultNamespace.Items
{
    public class BaseItem
    {
        public BaseItem(Sprite sprite,IPType ipType,CraftType craftType,Sprite[] clearAnimation)
        {
            ItemSprite = sprite;
            IPType = ipType;
            CraftType = craftType;
            ClearAnimation = clearAnimation;
        }
        public Sprite ItemSprite { get;  }
        public IPType IPType { get; }
        public CraftType CraftType { get; }
        public Sprite[] ClearAnimation{get;}
    }
}