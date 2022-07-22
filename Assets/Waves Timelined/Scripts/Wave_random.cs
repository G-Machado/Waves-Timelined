using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WavesTimelined/Lists/Random")]
public class Wave_random : Wave_object
{
    public int actCount = 1;
    public List<Wave_object> actions;

    public override bool Act(Collider collider, List<Wave_object> previous)
    {
        for (int i = 0; i < actCount; i++)
        {
            int randomIndex = Random.Range(0, actions.Count);

            List<Wave_object> currentPrevious = new List<Wave_object>();

            bool overflow = false;
            for (int j = 0; j < previous.Count; j++)
            {
                currentPrevious.Add(previous[j]);

                if (previous[j] == actions[randomIndex])
                    overflow = true;
            }

            if (overflow)
            {
                Debug.LogWarning("OVERFLOW DETECTED, IGNORING WAVE OBJECT " + actions[randomIndex].name);
                return false;
            }

            currentPrevious.Add(actions[randomIndex]);

            actions[randomIndex].Act(collider, currentPrevious);
        }

        return true;
    }

}


