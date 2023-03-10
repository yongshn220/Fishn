using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ListExtension
{
    public static List<T> Shuffle<T>(this List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }

    public static Vector3 GetAreaFromCenter(this List<Transform> list)
    {
        Vector3 center = GetCenter(list);
        float minX = list.Min(e => e.position.x);
        float minY = list.Min(e => e.position.y);
        float minZ = list.Min(e => e.position.z);
        float maxX = list.Max(e => e.position.x);
        float maxY = list.Max(e => e.position.y);
        float maxZ = list.Max(e => e.position.z);
        
        float x = Mathf.Min(Mathf.Abs(center.x - minX), Mathf.Abs(center.x - maxX)); 
        float y = Mathf.Min(Mathf.Abs(center.y - minY), Mathf.Abs(center.y - maxY));
        float z = Mathf.Min(Mathf.Abs(center.z - minZ), Mathf.Abs(center.z - maxZ));

        return new Vector3(x,y,z);
    }

    public static Vector3 GetCenter(this List<Transform> list)
    {
        Vector3 center = Vector3.zero;
        foreach(var entity in list)
        {
            center += entity.position;
        }
        return center / list.Count;
    }

    public static void SortByDistance(this List<Transform> list, Vector3 origin)
    {
        list.Sort((t1, t2) => Vector3.Distance(origin, t1.position).CompareTo(Vector3.Distance(origin, t2.position)));
    }

    public static List<Transform> shallowCopy(this List<Transform> list)
    {
        List<Transform> newList = new List<Transform>();

        foreach (var t in list)
        {
            newList.Add(t);
        }
        return newList;
    }

    public static List<EntityData> DeepCopy(this List<EntityData> dataList)
    {
        List<EntityData> newList = new List<EntityData>();
        foreach (var data in dataList)
        {
            newList.Add(new EntityData(data.id, data.type_id, data.born_datetime, data.feed_datetime, data.feed));
        }
        return newList;
    }

    public static List<SeaObjectData> DeepCopy(this List<SeaObjectData> dataList)
    {
        List<SeaObjectData> newList = new List<SeaObjectData>();
        foreach (var data in dataList)
        {
            newList.Add(new SeaObjectData(data.id, data.type_id, data.position, data.instantiated));
        }
        return newList;
    }

    public static List<CoralPlantData> DeepCopy(this List<CoralPlantData> dataList)
    {
        List<CoralPlantData> newList = new List<CoralPlantData>();
        foreach (var data in dataList)
        {
            newList.Add(new CoralPlantData(data.id, data.type_id, data.position, data.instantiated));
        }
        return newList;
    }

    public static List<SeaObjectData> ConvertToData(this List<SeaObjectMono> monoList)
    {
        List<SeaObjectData> dataList = new List<SeaObjectData>();

        foreach(var mono in monoList)
        {
            SeaObjectData data = new SeaObjectData(mono.id, mono.type_id, mono.position, mono.instantiated);
            dataList.Add(data);
        }
        return dataList;
    }

    public static List<CoralPlantData> ConvertToData(this List<CoralPlantMono> monoList)
    {
        List<CoralPlantData> dataList = new List<CoralPlantData>();

        foreach(var mono in monoList)
        {
            CoralPlantData data = new CoralPlantData(mono.id, mono.type_id, mono.position, mono.instantiated);
            dataList.Add(data);
        }
        return dataList;
    }

    public static List<EntityData> ConvertToData(this List<EntityMono> monoList)
    {
        List<EntityData> dataList = new List<EntityData>();

        foreach(var mono in monoList)
        {
            EntityData data = new EntityData(mono.id, mono.type_id, mono.born_datetime, mono.feed_datetime, mono.feed);
            dataList.Add(data);
        }
        return dataList;
    }
}

public class Vector3XComparer : IComparer<Transform>
{
    public int Compare(Transform a, Transform b)
    {
        return a.position.x.CompareTo(b.position.x);
    }
}

public class Vector3YComparer : IComparer<Transform>
{
    public int Compare(Transform a, Transform b)
    {
        return a.position.y.CompareTo(b.position.y);
    }
}

public class Vector3ZComparer : IComparer<Transform>
{
    public int Compare(Transform a, Transform b)
    {
        return a.position.z.CompareTo(b.position.z);
    }
}


