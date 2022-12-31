using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End_UIManager : MonoBehaviour
{
    public TextMeshProUGUI deathText, timeText, gameOverText;
    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    instance = this;
    //}

    public  void UpdateDeathUI(int deathCount)
    {
        deathText.text = deathCount.ToString();
    }

    public  void UpdateTimeUI(int time)
    {
        int minutes = (int)(time / 60);
        int seconds = time % 60;
        timeText.text = minutes.ToString("00")+":"+seconds.ToString("00");
    }

}
