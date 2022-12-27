using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TitleBehavior : PlayableBehaviour
{
    PlayableDirector director;

    bool isClipPlayed;
    public bool requirePause;
    bool pauseScheduled;

    public override void OnPlayableCreate(Playable playable)
    {
        director = playable.GetGraph().GetResolver() as PlayableDirector;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (isClipPlayed == false && info.weight > 0)
        {
            if (requirePause)
                pauseScheduled = true;

            isClipPlayed = true;
        }
    }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        isClipPlayed = false;
        if (pauseScheduled)
        {
            pauseScheduled = false;
            TimeManager.instance.PauseTimeline(director);
        }

    }
}
