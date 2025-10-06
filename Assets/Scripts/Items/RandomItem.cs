using System;
using System.Linq;
using System.Reflection;
using DefaultNamespace.Statics;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace.Items
{
    public static class RandomItem
    {
        public static BaseItem GetRandomItem()
        {
            Random random = new Random();
            var ipTypeValue = (int[])Enum.GetValues(typeof(IPType));
            var randomIPTypeValue = ipTypeValue[random.Next(0, ipTypeValue.Length)];
            int randomNum;
            CraftType randomCraftTypeValue;
            do
            {
                randomNum = random.Next(0, 100);
                randomCraftTypeValue = CraftType.Doll;
                if (randomNum < 30)
                {
                    randomCraftTypeValue = CraftType.Badge;
                }
                else if (randomNum < 60)
                {
                    randomCraftTypeValue = CraftType.Ticket;
                }
                else if (randomNum < 75)
                {
                    randomCraftTypeValue = CraftType.KeySet;
                }
                else if (randomNum < 90)
                {
                    randomCraftTypeValue = CraftType.Decoration;
                }
            }while ((int)randomCraftTypeValue >=LevelStatics.MaxCraftType);
            BaseItem randomItem = new BaseItem(Resources.Load<Sprite>("ArtAssets/Items/Item_"+(int)randomIPTypeValue+"_"+(int)randomCraftTypeValue)
                ,(IPType)randomIPTypeValue,randomCraftTypeValue,
                Resources.LoadAll<Sprite>("ArtAssets/Animation/Goods/ItemAnim_"+(int)randomIPTypeValue+"_"+(int)randomCraftTypeValue));
            return randomItem;
        }
    }
}