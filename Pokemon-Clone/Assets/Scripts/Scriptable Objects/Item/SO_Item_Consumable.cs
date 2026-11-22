using UnityEngine;

[CreateAssetMenu(fileName = "Consumable Item", menuName = "Scriptable Objects/Items/Consumable")]
public class SO_Item_Consumable : SO_Item_Base
{
    protected override void Awake()
    {
        base.Awake();
        itemType = EItemType.Consumable;
    }
}
