using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopObj : MonoBehaviour, IBuyHandler
    {
        [SerializeField] GameObject checkMark;
        [SerializeField] protected TextMeshProUGUI price;
        [SerializeField] protected Image image;
        [SerializeField] GameObject buyButton;

        protected IItem item;

        public Action CantBuy;
        public IItem Item => item;

        public event Action<IBuyHandler> WantToBuy;

        void Start() => BuyManager.Instance.Register(this);

        void OnDestroy() => BuyManager.Instance.UnRegister(this);

        public virtual void SetStats(IItem item)
        {
            this.item = item;
            checkMark.SetActive(item.IsByuing);
            buyButton.SetActive(!item.IsByuing);
            price.text = item.Price.ToString();
        }

        public void TryToBuy()
        {
            if (!item.IsByuing)
            {
                if (PlayerProgress.Instance.SummaryStats.CanBuy(item.Price))
                {
                    WantToBuy.Invoke(this);
                }
                else
                {
                    CantBuy();
                }
            }
        }

        public virtual void Buy()
        {
            PlayerProgress.Instance.SummaryStats.Buy(item.Price);
            item.Buy();
            checkMark.SetActive(item.IsByuing);
            buyButton.SetActive(!item.IsByuing);
        }
    }
}
