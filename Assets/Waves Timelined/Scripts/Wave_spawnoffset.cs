using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WavesTimelined/Spawn/Offset")]
public class Wave_spawnoffset : Wave_object
{
    [Header("Prefab Spawn")]
    [Tooltip("The prefab to be spawned")]
    public GameObject prefab;

    [Tooltip("The offset to wave collider position")]
    public Vector3 offset;

    public override bool Act(Collider collider, List<Wave_object> previous)
    {
        Vector3 position = collider.transform.position + offset;

        GameObject clone = Instantiate(prefab, position, collider.transform.rotation, collider.transform);

        return clone != null;
    }

}
