using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature Data", menuName = "Scriptable Objects/Creature Data")]
public class SO_CreatureData : ScriptableObject
{
    //This is null if it is a parent creature. 
    //Parent Creature is the base version of that creature without any modifications, it's pure form.
    //If this creature derives from a creature, that creature is the parent.
    //Ex. Pikachu is a parentCreature and this Pikachu has modified values
    public SO_CreatureData parentCreature;

    [Header("General Creature Information")]
    public string creatureName = "Creature";
    [TextArea] public string creatureDescrition = "Creature Description";
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

    [Header("Current Creature Properties")]
    //If we want to add a multiplier to the creature stats. Manually buffing them
    public int maxHealthMultiplier = 1;
    public int maxStrengthMultiplier = 1;
    public int maxSpeedMultiplier = 1;
    public int maxDefenseMultiplier = 1;

    [Space]
    //Information Specific to individual versions of the creature
    public int currentLevel = 1;
    public int currentHealth;
    public SO_Item_Equip currentItemEquipped;
    public List<EStatusCondition> currentStatusConditions;
    //public List<Moves> currentMoves;
    
    private int currentEquipIncrease_Health;
    private int currentEquipIncrease_Strength;
    private int currentEquipIncrease_Speed;
    private int currentEquipIncrease_Defense;


    //======================= STATISTIC FUNCTIONS ==========================//

    //Get the statistics based on the current level
    public int GetMaxHealth()
    {
        return baseHealth * Mathf.Max((currentLevel / 3), 1) + currentEquipIncrease_Health;
    }
    public int GetMaxStrength()
    {
        return baseStrength * Mathf.Max((currentLevel / 3), 1) + currentEquipIncrease_Strength;
    }
    public int GetMaxSpeed()
    {
        return baseSpeed * Mathf.Max((currentLevel / 3), 1) + currentEquipIncrease_Speed;
    }
    public int GetMaxDefense()
    {
        return baseDefense * Mathf.Max((currentLevel / 3), 1) + currentEquipIncrease_Defense;
    }

    //======================= HELPER FUNCTIONS ==========================//

    //Call this to add equipment onto Creature
    public void OnEquip(SO_Item_Equip _itemToEquip)
    {
        currentItemEquipped = _itemToEquip;
        currentItemEquipped.OnUse(this);
    }

    //Call this to remove equipment on Creature
    public void OnRemoveEquipment()
    {
        currentItemEquipped.OnRemove(this);
        currentItemEquipped = null;
    }
    //Use these functions to update stats based on the equipment effect. Called in the SO_Item_Equip
    public void UpdateCurrentEquipHealth(int amount)
    {
        currentEquipIncrease_Health += amount;
    }
    public void UpdateCurrentEquipStrength(int amount)
    {
        currentEquipIncrease_Strength += amount;
    }
    public void UpdateCurrentEquipSpeed(int amount)
    {
        currentEquipIncrease_Speed += amount;
    }
    public void UpdateCurrentEquipDefense(int amount)
    {
        currentEquipIncrease_Defense += amount;
    }


    //Call this when using healing items to affect current health
    public void UpdateCurrentHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, GetMaxHealth());
    }


    //returns if successful in removing condition or not
    public bool RemoveStatusCondition(EStatusCondition condition)
    {
        if (!currentStatusConditions.Contains(condition)) return false;

        currentStatusConditions.Remove(condition);
        return true;
    }

    //returns if successful in adding condition or not
    public bool AddStatusCondition(EStatusCondition condition)
    {
        if (currentStatusConditions.Contains(condition)) return false;

        currentStatusConditions.Add(condition);
        return true;
    }

    //Set the level of the Creature
    public void SetLevel(int level)
    {
        currentLevel = level;
    }
    public void SetLevel_Random(int minLevel, int maxLevel)
    {
        currentLevel = Random.Range(minLevel, maxLevel);
    }



    //======================= CREATION FUNCTIONS ==========================//

    public string GetFilePath()
    {
        return AssetDatabase.GetAssetPath(this);
    }

    [ContextMenu("Create Child Creature")]
    public SO_CreatureData CreateChildCreature()
    {
        SO_CreatureData childCreature = CreateInstance<SO_CreatureData>();

        #if UNITY_EDITOR
        string assetPath = AssetDatabase.GetAssetPath(this);

        childCreature.name = this.name + "_Child";
        AssetDatabase.AddObjectToAsset(childCreature, assetPath);

        childCreature.parentCreature = this;
        FillInfoBasedOnParentCreature(this, childCreature);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        #endif


        return childCreature;
    }

    private void FillInfoBasedOnParentCreature(SO_CreatureData _parentCreature, SO_CreatureData _childCreature)
    {
        _childCreature.parentCreature = _parentCreature;

        _childCreature.creatureName = _parentCreature.creatureName;
        _childCreature.creatureDescrition = _parentCreature.creatureDescrition;
        _childCreature.elementalType = _parentCreature.elementalType;
        _childCreature.frontSprite = _parentCreature.frontSprite;
        _childCreature.backSprite = _parentCreature.backSprite;

        _childCreature.baseHealth = _parentCreature.baseHealth;
        _childCreature.baseStrength = _parentCreature.baseStrength;
        _childCreature.baseSpeed = _parentCreature.baseSpeed;
        _childCreature.baseDefense = _parentCreature.baseDefense;

        _childCreature.maxHealthMultiplier = _parentCreature.maxHealthMultiplier;
        _childCreature.maxStrengthMultiplier = _parentCreature.maxStrengthMultiplier;
        _childCreature.maxSpeedMultiplier = _parentCreature.maxSpeedMultiplier;
        _childCreature.maxDefenseMultiplier = _parentCreature.maxDefenseMultiplier;
    }



    public void Awake()
    {
        //Set the current health to the max of the level on initializing the Scriptable Object
        currentHealth = GetMaxHealth();
    }

}
