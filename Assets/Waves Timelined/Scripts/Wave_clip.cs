using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class Wave_clip : PlayableAsset, ITimelineClipAsset
{
    [HideInInspector]
    public ClipCaps clipCaps
    { get { return ClipCaps.None; } }

    [SerializeField]
    public Wave_behaviour configurations = new Wave_behaviour();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<Wave_behaviour>.Create(graph, configurations);
    }

    
}
