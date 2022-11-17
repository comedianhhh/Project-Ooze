using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSender : MonoBehaviour
{
    [SerializeField] float range = 3;

    void Update()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var collider in colliders)
        {
            var receiver = collider.GetComponentInParent<TargetReceiver>();
            if (receiver != null)
                receiver.SendTarget(GetComponent<Health>());
        }
        //Debug .Log(colliders);
    }

    void OnGizomdraw()
    {
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
