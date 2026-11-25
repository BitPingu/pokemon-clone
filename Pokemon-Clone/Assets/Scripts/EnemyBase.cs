using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public SO_CreatureData creature;

    private void Start()
    {
        // set creature data for wild pokemon
        GetComponent<CreatureBase>().Creature = new CreatureInstance(creature);
    }
}
