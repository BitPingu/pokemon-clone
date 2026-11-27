using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RandomCreatureSpawner : MonoBehaviour
{
    [Header("Creature Settings")]
    [SerializeField] private GameObject creatureGO;
    [SerializeField] private List<SO_CreatureData> creaturePool;
    [SerializeField] private Vector2 levelRange;

    [Header("Spawn Settings")]
    [SerializeField] private int maxCreaturesSpawnedAtATime = 5;
    [SerializeField] private Vector2 initialDelayRangeBeforeSpawning;
    [SerializeField] private Vector2 spawnNextCreatureDelayRange;

    [Header("Spawn Zone Properties")]
    [SerializeField] private int spawnZoneID;
    [SerializeField] private Color spawnZoneColour;

    private void Start()
    {
        StartCoroutine(SpawnCreatureRoutine());
    }

    public SO_CreatureData GetRandomCreature()
    {
        //Get random creature from list
        int random = Random.Range(0, creaturePool.Count);

        //Create new Child Creature from selection
        SO_CreatureData newCreature = creaturePool[random].CreateChildCreature();

        //Now we can edit the properties of the child creature
        newCreature.SetLevel_Random((int)levelRange.x, (int)levelRange.y);

        //Set random moves

        return newCreature;
    }

    public int GetSpawnZoneID()
    {
        return spawnZoneID;
    }

    public void SetSpawnZoneID(int newID)
    {
        spawnZoneID = newID;
    }


    private IEnumerator SpawnCreatureRoutine()
    {
        float groundY = GetGroundY();
        yield return new WaitForSeconds(Random.Range(initialDelayRangeBeforeSpawning.x, initialDelayRangeBeforeSpawning.y));

        for(int i = 0; i < maxCreaturesSpawnedAtATime; i++)
        {
            yield return new WaitForSeconds(Random.Range(spawnNextCreatureDelayRange.x, spawnNextCreatureDelayRange.y));

            float x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
            float z = Random.Range(transform.position.z - transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2);
            GameObject creatureObj = Instantiate(creatureGO, new Vector3(x, groundY, z), Quaternion.identity);
            creatureObj.GetComponent<EnemyBase>().creature = GetRandomCreature();
        }
    }

    private float GetGroundY()
    {
        //Shoot ray downwards til hit ground
        RaycastHit hit;
        bool hitGround = Physics.Raycast(transform.position, Vector3.down, out hit, 1000, LayerMask.GetMask("Ground"));
        
        if(hitGround)
        {
            return hit.point.y;
        }
        return transform.position.y;

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = spawnZoneColour;

        Gizmos.DrawCube(transform.position, transform.localScale);

        Color outlineCol = new Color(spawnZoneColour.r - 0.1f, spawnZoneColour.g - 0.1f, spawnZoneColour.b - 0.1f, 1);
        Gizmos.color = outlineCol;
        Gizmos.DrawWireCube(transform.position, transform.localScale);

       
        GUIStyle spawnZoneStyle = new GUIStyle();
        spawnZoneStyle.normal.textColor = outlineCol;
        spawnZoneStyle.fontSize = 20;
        spawnZoneStyle.alignment = TextAnchor.MiddleCenter;
        Handles.Label(transform.position + Vector3.up * (transform.localScale.y / 2 + 2) , $"Spawn Zone {spawnZoneID}", spawnZoneStyle);

    }

}
