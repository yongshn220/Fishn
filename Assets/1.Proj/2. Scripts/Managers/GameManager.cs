using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{

    string loaddata;
    bool isDataReady;

    // Start is called before the first frame update
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
