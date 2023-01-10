using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    public List<FishData> entityDataList = new List<FishData>();
    public void Setup()
    {
        entityDataList.Add(new FishData(-1, 1, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 2, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 3, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 4, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 5, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 6, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 7, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 8, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 9, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 1, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 2, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 3, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 4, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 5, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 6, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 7, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 8, DateTime.MinValue, DateTime.MinValue));
        entityDataList.Add(new FishData(-1, 9, DateTime.MinValue, DateTime.MinValue));
    }
}
