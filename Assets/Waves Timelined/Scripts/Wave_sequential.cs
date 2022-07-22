using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WavesTimelined/Lists/Sequence")]
public class Wave_sequential : Wave_object
{
    public int actCount = 1;

    public List<Wave_object> actions;

    public override bool Act(Collider collider, List<Wave_object> previous)
    {
        for (int q = 0; q < actCount; q++)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                bool overflow = false;
                for (int j = 0; j < previous.Count; j++)
                {
                    if (previous[j] == actions[i])
                        overflow = true;
                }
                if (overflow)
                {
                    Debug.LogWarning("OVERFLOW DETECTED, IGNORING WAVE OBJECT " + actions[i].name);
                    continue;
                }
                previous.Add(actions[i]);

                /// trigger each action at list 
                actions[i].Act(collider, previous);
            }
        }

        return true;
    }

}
