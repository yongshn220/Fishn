using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishController : MonoBehaviour
{
    public int id;
    public int type_id;
    public DateTime born_datetime;
    public DateTime feed_datetime;

    public void Setup(int id, int type_id, DateTime born_datetime, DateTime feed_datetime)
    {
        this.id = id;
        this.type_id = type_id;
        this.born_datetime = born_datetime;
        this.feed_datetime = feed_datetime;
    }
}
