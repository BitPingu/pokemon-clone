using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreatureInstance
{
    // creature data
    public SO_CreatureData CreatureData { get; set;}
    public string Name => CreatureData.creatureName;

    // base stats (from SO)
    // dynamically updates on every call using (=>)
    // we could apply any multipliers here
    public int MaxHealth => CreatureData.baseHealth;
    public int Strength => CreatureData.baseStrength;
    public int Speed => CreatureData.baseSpeed;
    public int Defense => CreatureData.baseDefense;

    // individual stats (for instance)
    public int CurrentLevel { get; set; }
    public int CurrentHealth { get; set; }

    public CreatureInstance(SO_CreatureData creatureData)
    {
        this.CreatureData = creatureData;
        
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
