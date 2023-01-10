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
}