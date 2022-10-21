using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public EnemyType Type = EnemyType.None;
    public int Good = 0;
    public int Variant = 0;
}

public enum EnemyType { None, Grass, Mushroom, Goblin, Cyclops  }
