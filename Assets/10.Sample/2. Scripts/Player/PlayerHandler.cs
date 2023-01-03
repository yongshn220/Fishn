using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField]
    private Flock flock;

    private Wallet wallet;
    
    [SerializeField]
    private List<GameObject> fishList = new List<GameObject>();
    public List<GameObject> FishList 
    {
        get {
            return fishList;
        }
        set {
            fishList = value;
        }
    }

    void Awake() 
    {
        LoadData();
    }

    private void LoadData()
    {

    }
}
