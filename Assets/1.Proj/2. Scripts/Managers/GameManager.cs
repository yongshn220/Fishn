using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    public DataManager dataManager;
    public PrefabManager prefabManager;

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
        prefabManager = GetComponentInChildren<PrefabManager>();
    }

    void Start()
    {
        dataManager.LoadUserData();
    }
}
