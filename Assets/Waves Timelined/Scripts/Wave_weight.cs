using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WavesTimelined/Lists/Weight")]
public class Wave_weight : Wave_object
{
    public int actCount = 1;
    [Range(0, 1)]
    public float weight = 1;

    public List<Wave_object> actions;

    public override bool Act(Collider collider, List<Wave_object> previous)
    {
        for (int i = 0; i < actCount; i++)
        {
            int weigthIndex = (int)(weight * actions.Count);

            List<Wave_object> currentPrevious = new List<Wave_object>();

            bool overflow = false;
            for (int j = 0; j < previous.Count; j++)
            {
                currentPrevious.Add(previous[j]);

                if (previous[j] == actions[weigthIndex])
                    overflow = true;
            }

            if (overflow)
            {
                Debug.LogWarning("OVERFLOW DETECTED, IGNORING WAVE OBJECT " + actions[weigthIndex].name);
                return false;
            }

            currentPrevious.Add(actions[weigthIndex]);

            actions[weigthIndex].Act(collider, currentPrevious);
        }

        return true;
    }

}
