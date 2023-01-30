using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class EntityMono : MonoBehaviour
{
    public int id;
    public int type_id;
    public DateTime born_datetime;
    public DateTime feed_datetime;
    public int feed;

    public void Setup(EntityData data)
    {
        this.id = data.id;
        this.type_id = data.type_id;
        this.born_datetime = data.born_datetime;
        this.feed_datetime = data.feed_datetime;
        this.feed = data.feed;
    }
}
