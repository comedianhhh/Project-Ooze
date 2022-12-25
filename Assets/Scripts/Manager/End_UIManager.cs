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

    public static void UpdateDeathUI(int deathCount)
    {
        instance.deathText.text = deathCount.ToString();
    }

    public static void UpdateTimeUI(int time)
    {
        int minutes = (int)(time / 60);
        int seconds = time % 60;
        instance.timeText.text = time.ToString("00")+":"+seconds.ToString("00");
    }

}
