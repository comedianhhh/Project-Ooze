using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoom : Enemy
{



    protected override void introduction()
    {
        base.introduction();
        Debug.Log("this is Mr.MushRoom");
    }

    
    protected override void Move()
    {
        base.Move();
    }


}
