using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class SuperPowersShop : MonoBehaviour
    {
        [SerializeField] GameObject powerPrefab;
        [SerializeField] GameObject timePrefab;
        [SerializeField] GameObject cantBuy;

        List<GameObject> powerTimes;

        void Awake()
        {
            InstantiatePrefabs();
        }

        void InstantiatePrefabs()
        {
            powerTimes = new List<GameObject>();
            int counter = 0;

            foreach (var power in GameManager.Instance.AllPowers.Obstacles)
            {
                GameObject obj = Instantiate(powerPrefab, transform);
                obj.transform.SetParent(transform);
                PowerShopObj component = obj.GetComponent<PowerShopObj>();
                component.SetStats(PlayerProgress.Instance.SummaryStats.GetPower(counter));
                component.CantBuy += CantBuy;
                bool canGetPower = power.TryGetComponent(out SpriteRenderer sprite);
                if(!canGetPower) sprite = power.GetComponentInChildren<SpriteRenderer>();
                component.SetImage(sprite.sprite);
                component.BuyAction += BuyPower;
                counter++;
            }

            counter = 0;
            foreach(var pair in PlayerProgress.Instance.SummaryStats.ByuingPowers)
            {
                GameObject power = GameManager.Instance.AllPowers.GetObstacle(counter);
                GameObject obj = Instantiate(timePrefab,transform);
                obj.transform.SetParent(transform);
                obj.SetActive(pair.Key.IsByuing);
                PowerTimeObj component = obj.GetComponent<PowerTimeObj>();
                component.SetTime(pair.Value.Time);
                component.SetStats(PlayerProgress.Instance.SummaryStats.GetPowerTime(counter));
                component.CantBuy += CantBuy;
                SpriteRenderer sprite = power.GetComponentInChildren<SpriteRenderer>();
                component.SetImage(sprite.sprite);
                powerTimes.Add(obj);
                counter++;
            }
        }

        void CantBuy() => cantBuy.SetActive(true);

        void BuyPower(ShopObj shopObj)
        {
            powerTimes[shopObj.Item.Id].SetActive(true);
        }
    }
}