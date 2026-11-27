using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneManager : MonoBehaviour
{
    [ContextMenu("Update Zone IDs")]
    public void UpdateZoneID()
    {
        //Get each child and set ID based on sibling index
        for(int i = 0; i < transform.childCount; i++)
        {
            SpawnZone spawn = transform.GetChild(i).GetComponent<SpawnZone>();
            if (spawn == null) continue;
            spawn.SetSpawnZoneID(i);
        }
    }
}
