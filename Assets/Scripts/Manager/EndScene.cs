using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public End_UIManager UI;
    private void Awake()
    {
        Instantiate(UI);
    }
}
