using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCreatureSpawner : MonoBehaviour
{
    [SerializeField] private List<SO_CreatureData> creaturePool;
    [SerializeField] private Vector2 levelRange;

    public SO_CreatureData GetRandomCreature()
    {
        //Get random creature from list
        int random = Random.Range(0, creaturePool.Count -1);

        //Create new Child Creature from selection
        SO_CreatureData newCreature = creaturePool[random].CreateChildCreature();

        //Now we can edit the properties of the child creature
        newCreature.SetLevel_Random((int)levelRange.x, (int)levelRange.y);

        //Set random moves

        return newCreature;
    }

}
