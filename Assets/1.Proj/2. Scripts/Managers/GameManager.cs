using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    JObject loaddata;
    bool isDataReady;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        // xxManager = getComponentInChildren<xxManager>();
    }

    void Start()
    {
        LoadGameData().Forget();
    }

    private async UniTaskVoid LoadGameData()
    {
        loaddata = await DatabaseHelper.LoadGameData();
        isDataReady = true;
    }

    void Update()
    {
        if (isDataReady)
        {
            Debug.Log(loaddata);
            isDataReady = false;
        }
    }
}
