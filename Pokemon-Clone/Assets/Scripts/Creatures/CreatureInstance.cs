using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreatureInstance
{
    // creature data
    public SO_CreatureData Creature { get; set;}
    public string Name => Creature.creatureName;

    // base stats (from SO)
    // dynamically updates on every call using (=>)
    // we could apply any multipliers here
    public int MaxHealth => Creature.baseHealth;
    public int Strength => Creature.baseStrength;
    public int Speed => Creature.baseSpeed;
    public int Defense => Creature.baseDefense;

    // individual stats (for instance)
    public int CurrentLevel { get; set; }
    public int CurrentHealth { get; set; }

    public CreatureInstance(SO_CreatureData creatureData)
    {
        this.Creature = creatureData;
        
        // set starting health
        this.CurrentHealth = MaxHealth;
    }

    public virtual void Damage(int damageAmount)
    {
        // prevent negative
        int damageTaken = Mathf.Min(damageAmount, CurrentHealth);

        Debug.Log(Name + " took " + damageTaken + " damage");

        // take damage
        CurrentHealth -= damageTaken;

        if (CurrentHealth <= 0)
        {
            Debug.Log(Name + " fainted!");
            // state automatically ends
        }
    }
}
