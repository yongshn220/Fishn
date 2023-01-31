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
    public int maxFeed;

    public void Setup(EntityData data)
    {
        var entitySO = GameManager.instance.scriptableObjectManager.TryGetEntitySOById(data.type_id);
        if (entitySO == null) return;

        this.id = data.id;
        this.type_id = data.type_id;
        this.born_datetime = data.born_datetime;
        this.feed_datetime = data.feed_datetime;
        this.feed = data.feed;
        this.maxFeed = entitySO.maxFeed;
    }

    public EntityData ToData()
    {
        return new EntityData(this.id, this.type_id, this.born_datetime, this.feed_datetime, this.feed);
    }

    public void Feed(int amount)
    {
        feed += amount;
        if (feed > maxFeed) feed = maxFeed;
    }
    
    public void SaveData()
    {
        DelegateManager.InvokeOnEntityMonoUpdate(this);
        GameManager.instance.dataManager.SaveEntityData(ToData());
    }
}
