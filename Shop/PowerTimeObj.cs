using TMPro;
using UnityEngine;

namespace Shop
{
    public class PowerTimeObj :ShopObj
    {
        [SerializeField] TextMeshProUGUI time;
        public override void Buy()
        {
            base.Buy();

            if (!item.IsByuing)
            {
                price.text = item.Price.ToString();
                PowerTime powerTime = item as PowerTime;
                if (powerTime != null)
                {
                    time.text = powerTime.Time.ToString();
                }
            }

            PlayerProgress.Instance.Save();
        }

        public void SetTime(float time) => this.time.text = time.ToString();

        public virtual void SetImage(Sprite sprite) => image.sprite = sprite;
    }
}
