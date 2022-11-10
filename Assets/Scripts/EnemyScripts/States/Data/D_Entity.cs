using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newEntityData",menuName="Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance=0.5f;

    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;

    public LayerMask WhatIsWall;
    public LayerMask WhatIsPlayer;
}
