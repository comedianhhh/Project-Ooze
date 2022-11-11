using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSender : MonoBehaviour
{
    [SerializeField] float range = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var collider in colliders)
        {
            var receiver = collider.GetComponent<TargetReceiver>();
            if (receiver != null)
                receiver.SendTarget(GetComponent<Health>());
        }
    }

}
