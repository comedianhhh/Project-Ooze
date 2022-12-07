using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HealthEffect
{
    public float DamagePerSecond;
    public float Duration;
    public float TimeStart;
    public HeathEffectType Type;
    

    public HealthEffect(float damagePerSecond, float duration, HeathEffectType type = HeathEffectType.None)
    {
        DamagePerSecond = damagePerSecond;
        Duration = duration;
        TimeStart = Time.time;
        Type = type;
    }

    public enum HeathEffectType { None, Fire, Posion }
}
