using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public bool TryPurchase(int id, ItemType type)
    {
        Debug.Log("try buy : " + id + type);
        return false;
    }
}
