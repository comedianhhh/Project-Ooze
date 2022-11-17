using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetReceiver : MonoBehaviour
{
    [SerializeField] float memoryDuration = 2;
    float targetSetTime = 0;
    public Health Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - targetSetTime > memoryDuration)
            Target = null;
    }

    public void SendTarget(Health target)
    {
        targetSetTime = Time.time;
        Target = target;
    }


}
