using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature Data", menuName = "Scriptable Objects/Creature Data")]
public class SO_CreatureData : ScriptableObject
{
    [Header("General Creature Information")]
    public string creatureName = "Creature";
    public string creatureDescrition = "Creature Description";
    public List<EElementalType> elementalType;
    //public Ability ability;
    public Sprite frontSprite;
    public Sprite backSprite;
    //public List<Moves> availableMoves;

    [Header("Creature Stats")]
    [Range(0f,100f)]
    public int baseHealth = 50;

    [Range(0f, 100f)]
    public int baseStrength = 50;

    [Range(0f, 100f)]
    public int baseSpeed = 50;

    [Range(0f, 100f)]
    public int baseDefense = 50;

    //If we want to add a multiplier to the creature stats. Manually buffing them
    public int maxHealthMultiplier = 1;
    public int maxStrengthMultiplier = 1;
    public int maxSpeedMultiplier = 1;
    public int maxDefenseMultiplier = 1;

    [Header("Current Creature Properties")]
    //Information Specific to individual versions of the creature
    public int currentLevel;
    public int currentHealth;
    public SO_Item_Equip currentItemEquipped;
    public int currentStatusConditions;
    //public List<Moves> currentMoves;


    //Get the statistics based on the current level
    public int GetMaxHealth()
    {
        return baseHealth * (currentLevel / 3);
    }
    public int GetMaxStrength()
    {
        return baseStrength * (currentLevel / 3);
    }
    public int GetMaxSpeed()
    {
        return baseSpeed * (currentLevel / 3);
    }
    public int GetMaxDefense()
    {
        return baseDefense * (currentLevel / 3);
    }

}
