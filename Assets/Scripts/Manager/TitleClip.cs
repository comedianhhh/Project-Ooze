using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TitleClip : PlayableAsset
{
    public TitleBehavior template = new TitleBehavior();
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TitleBehavior>.Create(graph, template);
        return playable;
    }
}
