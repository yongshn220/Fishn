using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class EntityMono : MonoBehaviour
{   
    public int _id; 
    public int id { get { return _id; }}

    public int _type_id;
    public int type_id { get { return _type_id; }}

    public int _coral;
    public int coral { get { return _coral; }}

    public DateTime _born_datetime;
    public DateTime born_datetime { get { return _born_datetime; }}

    public DateTime _feed_datetime;
    public DateTime feed_datetime { get { return _feed_datetime; }}

    public int _feed;
    public int feed { get { return _feed; }}

    public int _maxFeed;
    public int maxFeed { get { return _maxFeed; }}


    public void Setup(EntityData data)
    {
        var entitySO = GameManager.instance.scriptableObjectManager.TryGetEntitySOById(data.type_id); if (entitySO == null) return;
        var entityGrowthSO = GameManager.instance.scriptableObjectManager.TryGetEntityGrowthSOByData(data); if (entityGrowthSO == null) return;

        _id = data.id;
        _type_id = data.type_id;
        _coral = entitySO.coral;
        _born_datetime = data.born_datetime;
        _feed_datetime = data.feed_datetime;
        _feed = data.feed;
        
        _maxFeed = entityGrowthSO.maxFeed;
        transform.localScale = Vector3.one * entityGrowthSO.scale;
    }

    public EntityData ToData()
    {
        return new EntityData(this.id, this.type_id, this.born_datetime, this.feed_datetime, this.feed);
    }

    public void GetFeed(int amount)
    {
        _feed += amount;
        if (_feed > maxFeed) _feed = maxFeed;
        SaveData();
    }

    public void UseFeed(int amount)
    {
        _feed -= amount;
        if (_feed < 0) _feed = 0;
        SaveData();
    }

    public void SaveData()
    {
        DelegateManager.InvokeOnEntityMonoUpdate(this);
        GameManager.instance.dataManager.SaveEntityData(ToData());
    }
}
