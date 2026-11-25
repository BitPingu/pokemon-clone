using UnityEngine;

[CreateAssetMenu(fileName = "Item_Default", menuName = "Scriptable Objects/Items/Default")]
public class SO_Item_Default : SO_Item_Base
{
    protected override void Awake()
    {
        base.Awake();
        itemType = EItemType.Default;
    }

    public override void OnPickup()
    {
        base.OnPickup();
    }

    //We can't use default objects so nothing goes in this code
    public override void OnUse(SO_CreatureData _creatureData)
    {
        base.OnUse(_creatureData);
    }
}
