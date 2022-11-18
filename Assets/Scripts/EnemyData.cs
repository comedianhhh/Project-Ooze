using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyData
{
    public EnemyType Type;
    public int Good;
    public int Variant;

    public EnemyData(EnemyType type, int good, int variant)
    {
        Type = type;
        Good = good;
        Variant = variant;
    }
}

public enum EnemyType { None, Grass, Mushroom, Goblin, Cyclops  }
