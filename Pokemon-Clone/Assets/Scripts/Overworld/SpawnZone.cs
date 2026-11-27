using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[System.Serializable]
public class CreatureDropChanceClass
{
    public SO_CreatureData creatureData;
    [Range(0f, 1f)] public float spawnRate = 1f;

}

public class SpawnZone : MonoBehaviour
{
    [Header("Creature Settings")]
    [SerializeField] private GameObject creatureGO;
    [SerializeField] private List<CreatureDropChanceClass> creatureSpawnPool;
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


    //Gets a random creature from the pool and randomizes level, moves, etc. from attributes
    public SO_CreatureData GetRandomCreature()
    {
        //Get random creature from list
        int random = RNGVariant();

        //Create new Child Creature from selection
        SO_CreatureData newCreature = creatureSpawnPool[random].creatureData.CreateChildCreature();

        //Now we can edit the properties of the child creature
        newCreature.SetLevel_Random((int)levelRange.x, (int)levelRange.y);

        //Set random moves

        return newCreature;
    }

    private int RNGVariant()
    {
        float sumOfProbabilities = 0f;
        float prevProbs = 0f;

        float[] probs = new float[creatureSpawnPool.Count];
        float[] newChance = new float[creatureSpawnPool.Count];
        float[] cumulativeProb = new float[creatureSpawnPool.Count + 1];

        //Get the sum of all the probabilities
        for(int i = 0; i < probs.Length; i++)
        {
            sumOfProbabilities += creatureSpawnPool[i].spawnRate;
        }

        //Calculate the chance of each snowball based on the value
        for(int i = 0; i < probs.Length; i++)
        {
            newChance[i] = creatureSpawnPool[i].spawnRate / sumOfProbabilities;
        }

        //now for cumulative probability add them all up so result is 1
        for(int i = 0; i < probs.Length; i++)
        {
            cumulativeProb[0] = 0.00f;

            prevProbs += newChance[i];
            cumulativeProb[i + 1] = prevProbs;
        }

        //Now generate a random number between 0 and 1 and the result is the creature we get
        float random = Random.Range(0.0f, 1.0f);

        for(int i = 0; i < cumulativeProb.Length; i++)
        {
            if (random > cumulativeProb[i] && random < cumulativeProb[i + 1])
                return i;
        }
        return 0;
    }

    public int GetSpawnZoneID()
    {
        return spawnZoneID;
    }

    public void SetSpawnZoneID(int newID)
    {
        spawnZoneID = newID;
        gameObject.name = $"Spawn Zone - {newID}";
    }

    //Spawns creatures up to the max allowed based on random draws from the pool
    private IEnumerator SpawnCreatureRoutine()
    {
        float groundY = HelperFunctions.GetGroundY(transform.position);

        yield return new WaitForSeconds(Random.Range(initialDelayRangeBeforeSpawning.x, initialDelayRangeBeforeSpawning.y));

        for(int i = 0; i < maxCreaturesSpawnedAtATime; i++)
        {
            yield return new WaitForSeconds(Random.Range(spawnNextCreatureDelayRange.x, spawnNextCreatureDelayRange.y));

            //Spawn in random area inside the zone
            float x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
            float z = Random.Range(transform.position.z - transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2);
            GameObject creatureObj = Instantiate(creatureGO, new Vector3(x, groundY, z), Quaternion.identity);

            //Set the specific information of the creature
            creatureObj.transform.SetParent(transform);
            EnemyBase enemyBase = creatureObj.GetComponent<EnemyBase>();
            enemyBase.creature = GetRandomCreature();
            enemyBase.UpdateMaterialSprite();
        }
    }



#if UNITY_EDITOR
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
        Vector3 labelPosition = transform.position + Vector3.up * (transform.localScale.y / 2 + 2);
        Handles.Label(labelPosition, $"Spawn Zone {spawnZoneID}", spawnZoneStyle);

        //Draw Creature Textures
        int i = 0;
     
        foreach (CreatureDropChanceClass creatureData in creatureSpawnPool)
        {
            if (creatureData == null) continue;
            SO_CreatureData creature = creatureData.creatureData;

            if (creature == null) { continue; }
            if (creature.frontSprite == null) { i++; continue; }

            Gizmos.DrawIcon(labelPosition - new Vector3((creatureSpawnPool.Count - 1) / 2.00f - i, 1, 0), creature.creatureName, true, Color.white);
            i++;
        }
    }
#endif

}
