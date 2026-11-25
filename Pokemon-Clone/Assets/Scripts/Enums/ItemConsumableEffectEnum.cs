public enum EItemConsumableEffect
{
    None, //no consumable effect
    Heal_HP, //recover HP. Amount is in the SO_Item_Consumable
    Heal_Burn, //remove burn status effect
    Heal_Poison, //remove poison status effect

    MAX //last entry for getting length of enum
}