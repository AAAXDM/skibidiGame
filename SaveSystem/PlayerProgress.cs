using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField] SummaryStats summaryStats;
    public SummaryStats SummaryStats => summaryStats;

    private static PlayerProgress instance;

    public static PlayerProgress Instance => instance;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        else Destroy(gameObject);
    }

    void Start()
    {
#if UNITY_WEBGL
        LoadExtern();
#endif
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(SummaryStats);
#if UNITY_WEBGL
       SaveExtern(jsonString);
#endif
    }

    public void Load(string value) => summaryStats = JsonUtility.FromJson<SummaryStats>(value);
}
