using System;
using UnityEngine;

namespace Shop
{
    public class PowerShopObj : ShopObj
    {
        public Action<ShopObj> BuyAction;

        public override void Buy()
        {
            base.Buy();
            BuyAction(this);
            PlayerProgress.Instance.SummaryStats.BuyPower(item.Id);
            PlayerProgress.Instance.Save();
        }

        public virtual void SetImage(Sprite sprite)  => image.sprite = sprite;
    }
}
