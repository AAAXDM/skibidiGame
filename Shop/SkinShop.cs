using Shop;
using System.Collections.Generic;
using UnityEngine;

public class SkinShop : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject cantBuy;
    List<SkinShopObj> skinList;

    void Start() => InstantiatePrefabs();

    void InstantiatePrefabs()
    {
        skinList = new();
        int counter = 0;

        foreach(var skin in GameManager.Instance.AllSkins.Sprites())
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.transform.SetParent(transform);
            SkinShopObj component = obj.GetComponent<SkinShopObj>();
            component.SetStats(PlayerProgress.Instance.SummaryStats.GetSkin(counter));
            component.PutOnAction += PutOn;
            component.CantBuy += CantBuy;
            component.SetImages(skin);
            skinList.Add(component);
            counter ++;
        }

    }

    void CantBuy() => cantBuy.SetActive(true);

    void PutOn()
    {
        if (PlayerProgress.Instance.SummaryStats.SkinId > 0)
        {
            skinList[PlayerProgress.Instance.SummaryStats.SkinId - 1].TakeOff();
        }
    }
}
