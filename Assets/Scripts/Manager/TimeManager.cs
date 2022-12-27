using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimeManager : MonoBehaviour
{

    public static TimeManager instance;
    public enum GameMode { AnimPlay,AnimNoPlay}
    public GameMode gameMode;

    PlayableDirector currentPlayableDirector;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        gameMode = GameMode.AnimPlay;
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if (gameMode == GameMode.AnimNoPlay)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResumeTimeLine();

            }
        }
    }

    public void PauseTimeline(PlayableDirector _playableDirector)
    {
        currentPlayableDirector = _playableDirector;
        gameMode = GameMode.AnimNoPlay;
        //currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
        currentPlayableDirector.Pause();
    }
    public void ResumeTimeLine()
    {
        Debug.Log("resumTimeline");
        gameMode = GameMode.AnimPlay;
        //currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
        currentPlayableDirector.Play();

    }

}
