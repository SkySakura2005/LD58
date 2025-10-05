using UnityEngine;

namespace DefaultNamespace.Items
{
    public class BaseItem
    {
        public BaseItem(Sprite sprite,IPType ipType,CraftType craftType)
        {
            ItemSprite = sprite;
            IPType = ipType;
            CraftType = craftType;
        }
        public Sprite ItemSprite { get;  }
        public IPType IPType { get; }
        public CraftType CraftType { get; }
    }
}