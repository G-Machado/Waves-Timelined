using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave_object : ScriptableObject
{
    public abstract bool Act(Collider collider, List<Wave_object> previous = null);
}
