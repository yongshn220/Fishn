using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    [HideInInspector] public DataManager dataManager;
    [HideInInspector] public ScriptableObjectManager scriptableObjectManager;
    [HideInInspector] public ViewSceneManager viewSceneManager;
    [HideInInspector] public PurchaseManager purchaseManager;

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
    }

    void Start()
    {
        dataManager.LoadUserData();
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
}
