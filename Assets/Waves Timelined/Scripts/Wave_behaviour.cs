using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class Wave_behaviour : PlayableBehaviour
{
    public Wave_behaviour() { }

    public enum WaveStartType
    {
        TIMED,
        TRIGGED
    }
    public enum WaveEndType
    {
        TIMED,
        CLEAN_UP
    }

    [Tooltip("TIMED waves play with timeline.\nTRIGGED waves wait for collider to be trigged.")]
    public WaveStartType StartType;
    [Tooltip("TIMED waves end with timeline.\nCLEAN_UP waves wait for all enemies to be dead.\n(dead = unparent from collider or destroyed)")]
    public WaveEndType EndType;

    [Tooltip("Wave actions will be played sequentially at wave start.")]
    public List<Wave_object> Actions;
    //public TimeLineList Actions;

    private bool played = false;
    private bool trigged = false;
    private bool subscribed = false;
    public double startTime = -1;

    [HideInInspector]
    public float waveStart;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Collider waveCollider = playerData as Collider;

        if (waveCollider == null) return;
        if (!Application.isPlaying) return;

        if (!played)
        {
            PlayableDirector director = (playable.GetGraph().GetResolver() as PlayableDirector);

            if (StartType == WaveStartType.TIMED)
            {
                played = true;

                for (int i = 0; i < Actions.Count; i++)
                {
                    List<Wave_object> previous = new List<Wave_object>();
                    previous.Add(Actions[i]);

                    Actions[i].Act(waveCollider, previous);
                }
            }
            else
            {
                if (!trigged)
                {
                    TimelineAsset timeline = director.playableAsset as TimelineAsset;
                    SetClipStart(timeline);
                    //Debug.Log("setting time to start " + startTime);
                    director.time = startTime;
                }

                Wave_trigger trigger = waveCollider.GetComponent<Wave_trigger>();
                if (trigger == null)
                    trigger = waveCollider.gameObject.AddComponent<Wave_trigger>();

                if (!subscribed)
                {
                    trigger.trigged += TriggerWave;
                    subscribed = true;
                }
            }
        }
        else
        {
            if (EndType == WaveEndType.CLEAN_UP)
            {
                PlayableDirector director = (playable.GetGraph().GetResolver() as PlayableDirector);

                if (waveCollider.transform.childCount <= 0)
                {
                    //director.time = actionTime + playable.GetDuration() * .95f;
                    //director.Resume();
                }
                else
                {
                    if (director.time > playable.GetDuration() * .95f)
                    {
                        director.time = startTime + playable.GetDuration() * .95f;
                        Debug.Log("ending wave!!");
                    }
                }
            }

        }
    }

    public void SetClipStart(TimelineAsset timeline)
    {
        foreach (var track in timeline.GetOutputTracks()){
            foreach (var clip in track.GetClips())
            {
                Wave_clip behaviour = clip.asset as Wave_clip;
                behaviour.configurations.startTime = clip.start;
                Debug.Log(clip.start);
            }
        }
    }

    public double GetClipStart(TimelineAsset timeline)
    {
        foreach (var track in timeline.GetOutputTracks())
        {
            foreach (var clip in track.GetClips())
            {
                Wave_clip behaviour = clip.asset as Wave_clip;
                if (behaviour.configurations.Equals(this))
                    return clip.start;
            }
        }

        return 0;
    }

    public override void OnPlayableCreate(Playable playable)
    {
        PlayableDirector director = (playable.GetGraph().GetResolver() as PlayableDirector);
        TimelineAsset timeline = director.playableAsset as TimelineAsset;

        SetClipStart(timeline);

        base.OnPlayableCreate(playable);
    }
    private void TriggerWave()
    {
        trigged = true;
        Debug.Log("trigged by event");
    }
}
