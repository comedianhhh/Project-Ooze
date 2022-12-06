using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyData
{
    public EnemyType Type;
    public int Good;
    public int Variant;
    public int Bulk;

    public EnemyData(EnemyType type, int good, int variant,int bulk)
    {
        Type = type;
        Good = good;
        Variant = variant;
        Bulk = bulk;
    }
}

public enum EnemyType { None, Grass, Mushroom, Goblin, Cyclops, MrMagic, MrPosion}
