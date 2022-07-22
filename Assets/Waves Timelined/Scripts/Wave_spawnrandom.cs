using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WavesTimelined/Spawn/Random")]
public class Wave_spawnrandom : Wave_object
{
    [Header("Prefab Spawn")]
    [Tooltip("The prefab to be spawned at a random position inside the collider")]
    public GameObject prefab;

    [Header("Collider spawn area %")]
    [Range(0, 1)]              
    public float xPercentage = 1;
    [Range(0, 1)]
    public float yPercentage = 1;
    [Range(0, 1)]
    public float zPercentage = 1;

    public override bool Act(Collider collider, List<Wave_object> previous)
    {
        Vector3 offset = new Vector3(
            Random.Range((-collider.bounds.size.x * xPercentage)/2, (collider.bounds.size.x * xPercentage)/2),
            Random.Range((-collider.bounds.size.y * yPercentage)/2, (collider.bounds.size.y * yPercentage)/2),
            Random.Range((-collider.bounds.size.z * zPercentage)/2, (collider.bounds.size.z * zPercentage)/2)
        );

        Vector3 position = collider.transform.position + offset;

        GameObject clone = Instantiate(prefab, position, collider.transform.rotation, collider.transform);

        return clone != null;
    }

}
