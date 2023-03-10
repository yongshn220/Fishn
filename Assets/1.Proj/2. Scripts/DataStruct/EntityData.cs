using UnityEngine;
using System;

public class EntityData
{
    public int id;
    public int type_id;
    public DateTime born_datetime;
    public DateTime feed_datetime;
    public int feed;

    public EntityData(int id, int type_id, DateTime born_datetime, DateTime feed_datetime, int feed)
    {
        this.id = id;
        this.type_id = type_id;
        this.born_datetime = born_datetime;
        this.feed_datetime = feed_datetime;
        this.feed = feed;
    }
    
    public override string ToString()
    {
        return $"id = {id}, type_id = {type_id}, born_datetime = {born_datetime}, feed_datetime = {feed_datetime}";
    }
}