using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End_UIManager : MonoBehaviour
{
    static End_UIManager instance;

    public TextMeshProUGUI deathText, timeText, gameOverText;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

}
