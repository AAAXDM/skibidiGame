using UnityEngine;

public class BuyManager : MonoBehaviour
{
    [SerializeField] GameObject buyPannel;

    static BuyManager instance;
    IBuyHandler buyHandler;
    public static BuyManager Instance => instance;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
        buyPannel.SetActive(false);
    }

    public void Register(IBuyHandler handler) => handler.WantToBuy += TryToBuy;


    public void UnRegister(IBuyHandler handler) => handler.WantToBuy -= TryToBuy;

    void TryToBuy(IBuyHandler handler)
    {
        buyPannel.SetActive(true);
        buyHandler = handler;
    }

    public void Buy()
    {
        buyPannel.SetActive(false);
        buyHandler.Buy();
    }
}
