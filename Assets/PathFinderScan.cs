using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class PathFinderScan : MonoBehaviour
{
    // Start is called before the first frame update
    int times=0;
    void Start()
    {
        AstarPath.active.Scan();
    }

    // Update is called once per frame
    void Update()
    {
        if (times < 3)
        {
            AstarPath.active.Scan();
            times++;
        }
        
    }
}
