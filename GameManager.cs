using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    [SerializeField] ObstacleSO allPowers;
    [SerializeField] SkinSo allSkins;
    DeviceType deviceType;
    static GameManager instance;
    int gameSceneIndex = 1;
    int shopSceneIndex = 2;

    public static GameManager Instance => instance;
    public ObstacleSO AllPowers => allPowers;
    public SkinSo AllSkins => allSkins;
    public int ShopSceneIndex => shopSceneIndex;
    public DeviceType DeviceType => deviceType;

    [DllImport("__Internal")]
    static extern void GetPlatformType();

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
#if UNITY_WEBGL
        GetPlatformType();
#endif
    }

    public void SetDeviceType(string type)
    {
        switch (type) 
        {
            case "mobile":
                deviceType = DeviceType.mobile;
                break;
            case "desktop":
                deviceType = DeviceType.desktop;
                break;
            default:
                deviceType = DeviceType.mobile; 
                break;
        }
    }

    public void StartGame() => SceneManager.LoadScene(gameSceneIndex);

    public void LoadShop() => SceneManager.LoadSceneAsync(shopSceneIndex, LoadSceneMode.Additive);

    public void ExitGame() => Application.Quit();
}
