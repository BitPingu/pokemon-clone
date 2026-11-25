using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Singleton instance
    public static PlayerBase Instance { get; private set; }

    // Pokemon party
    private List<CreatureInstance> _party = new List<CreatureInstance>();

    // Pokemon base to instantiate from
    [SerializeField] private GameObject _creatureBase;

    // Current Pokemon (walking!!)
    private GameObject _currentCreature;

    protected virtual void Awake()
    {
        // Make sure only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Adds a creature to the player's party
    public void AddCreature(SO_CreatureData creatureData)
    {
        if (_party.Count >= 6) {
            Debug.Log("Party is full!");
            return; // party full! (send to pc?)
        }

        CreatureInstance creature = new CreatureInstance(creatureData);
        _party.Add(creature);

        Debug.Log("Creatures in party:");
        foreach (CreatureInstance caughtCreature in _party)
        {
            Debug.Log(caughtCreature.Name);
        }
    }

    // Spawns a creature in the current scene
    public void SpawnCreature(CreatureInstance creature, Vector3 position)
    {
        // only 1 active creature at a time
        if (_currentCreature != null) Destroy(_currentCreature);

        // instantiate at position
        _currentCreature = Instantiate(_creatureBase, position, Quaternion.identity);

        // assign current data
        _currentCreature.GetComponent<CreatureBase>().Creature = creature;
    }

    // Gets the first healthy creature in the party
    public CreatureInstance GetFirstHealthyCreature()
    {
        foreach (CreatureInstance creature in _party)
        {
            if (creature.CurrentHealth > 0)
            {
                return creature;
            }
        }

        return null; // all creatures have fainted
    }
}
