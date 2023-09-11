using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Shop;

[Serializable]
public class SummaryStats
{
    [SerializeField] List<Pair<Item, PowerTime>> byuingPowers;
    [SerializeField] List<Item> skinsInfo;
    [SerializeField] Sprite toiletSkin;
    [SerializeField] Sprite headSkin;
    [SerializeField] int bestScore;
    [SerializeField] int coins;
    [SerializeField] int skinId;
    [SerializeField] float maxPowerTime;

    public IReadOnlyCollection<Pair<Item, PowerTime>> ByuingPowers => byuingPowers;
    public Sprite Head => headSkin;
    public Sprite Toilet => toiletSkin;
    public int SkinId => skinId;
    public int BestScore => bestScore;
    public int Coins => coins;
    public float MaxPowerTime => maxPowerTime;

    public void SetBestScore(int score) 
    {  
        if(bestScore<score) bestScore = score;    
    }

    public void SetCoins(int coins)
    {
        if(coins > 0) this.coins += coins;
    }

    public bool CanBuy(int coins)
    {
        
        if (coins <= this.coins) return true;
        return false;
    }

    public void Buy(int coins)
    {
        if(coins <= this.coins) this.coins -= coins;
    }

    public bool Contains(Item power)
    {
        var item =  byuingPowers.Where(x => x.Key == power);
        if (item != null) return true;
        else return false;
    }

    public Item GetPower(int i)
    {
        if (i < byuingPowers.Count)
        {
          return byuingPowers.ElementAt(i).Key;
        }

        return null;
    }

    public PowerTime GetPowerTime(int i)
    {
        if (i < byuingPowers.Count)
        {
            return byuingPowers.ElementAt(i).Value;
        }

        return null;
    }

    public Item GetSkin(int i)
    {
        if (i < skinsInfo.Count)
        {
            return skinsInfo[i];
        }

        return null;
    }

    public void BuyPower(int i) => byuingPowers.ElementAt(i).Key.Buy();

    public void SetSkin(KeyValuePair<Sprite, Sprite> skin)
    {
        toiletSkin = skin.Key;
        headSkin = skin.Value;
    }

    public void SetSkinId(int id) => skinId = id;
}
