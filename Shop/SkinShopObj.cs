using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Shop
{
    public class SkinShopObj : ShopObj
    {
        [SerializeField] Image faceImage;
        [SerializeField] GameObject setPannel;
        [SerializeField] GameObject putOnButton;

        public Action PutOnAction;

        public override void SetStats(IItem item)
        {
            base.SetStats(item);
            putOnButton.SetActive(item.IsByuing);
            if (item.Id != PlayerProgress.Instance.SummaryStats.SkinId)
            {
                setPannel.SetActive(false);
            }
        }

        public override void Buy()
        {
            base.Buy();
            var sprites = new KeyValuePair<Sprite, Sprite>(image.sprite, faceImage.sprite);
            putOnButton.SetActive(item.IsByuing);
            PlayerProgress.Instance.SummaryStats.SetSkin(sprites);
            PlayerProgress.Instance.Save();
        }

        public void SetImages(KeyValuePair<Sprite, Sprite> images)
        {
            image.sprite = images.Key;
            faceImage.sprite = images.Value;
        }

        public void PutOn()
        {
            KeyValuePair<Sprite, Sprite> skin = new(image.sprite, faceImage.sprite);
            setPannel.SetActive(true);
            PutOnAction();
            PlayerProgress.Instance.SummaryStats.SetSkinId(item.Id);
            PlayerProgress.Instance.SummaryStats.SetSkin(skin);
            PlayerProgress.Instance.Save();
        }

        public void TakeOff() => setPannel.SetActive(false);
    }
}
