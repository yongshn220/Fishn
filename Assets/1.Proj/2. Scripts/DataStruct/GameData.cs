using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int id;
    public int tank_id;
    public int coral;

    public GameData(int id, int tank_id, int coral)
    {
        this.id = id;
        this.tank_id = tank_id;
        this.coral = coral;
    }

    public override string ToString()
    {
        return $"id = {id}, tank_id = {tank_id}, coral = {coral}";
    }
}