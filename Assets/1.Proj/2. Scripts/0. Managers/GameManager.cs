using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    [HideInInspector] public DataManager dataManager;
    [HideInInspector] public ScriptableObjectManager scriptableObjectManager;
    [HideInInspector] public PurchaseManager purchaseManager;
    [HideInInspector] public DelegateManager delegateManager;
    [HideInInspector] public ScreenResolutionManager screenResolutionManager;

    [HideInInspector] public ViewSceneManager viewSceneManager;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        
        DontDestroyOnLoad(this);

        dataManager = GetComponentInChildren<DataManager>();
        scriptableObjectManager = GetComponentInChildren<ScriptableObjectManager>();
        purchaseManager = GetComponentInChildren<PurchaseManager>();
        delegateManager = GetComponentInChildren<DelegateManager>();
        screenResolutionManager = GetComponentInChildren<ScreenResolutionManager>();
    }

    void Start()
    {
        DelegateManager.OnUserDataLoad += OnUserDataLoad;
        dataManager.LoadUserData();
    }

    public void OnUserDataLoad()
    {
        screenResolutionManager.Setup();
    }

    public async void AsyncReload()
    {
        await dataManager.AsyncLoadUserData();
        SceneManager.LoadScene("View");
    }

    public void SetViewSceneManager(ViewSceneManager viewSceneManager)
    {
        this.viewSceneManager = viewSceneManager;
    }
}


public enum LayerType
{
    Default = 0,
    UI = 5,
    Obstacle = 6,
    Entity = 7,
    ObjectLighter = 8,
    CoralPlant = 9,
}
