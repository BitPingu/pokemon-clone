using UnityEngine;

[CreateAssetMenu(fileName = "Equip Item", menuName = "Scriptable Objects/Items/Equip")]
public class SO_Item_Equip : SO_Item_Base
{
    protected override void Awake()
    {
        base.Awake();
        itemType = EItemType.Equip;
    }
}
