using UnityEngine;

[CreateAssetMenu(fileName = "Equip Item", menuName = "Scriptable Objects/Items/Equip")]
public class SO_Item_Equip : SO_Item_Base
{
    [Header("Equip Properties")]
    [SerializeField] private EItemEquipEffect itemEquipEffect;

    protected override void Awake()
    {
        base.Awake();
        itemType = EItemType.Equip;
    }

    public override void OnPickup()
    {
        base.OnPickup();
    }

    //When the item is equipped onto creature
    public override void OnUse(SO_CreatureData _creatureData)
    {
        base.OnUse(_creatureData);

        //Here we can write code to attach to the specific Pokemon passed through
        switch (itemEquipEffect)
        {
            case EItemEquipEffect.None:
                break;

            case EItemEquipEffect.SpeedUp:
                break;

            case EItemEquipEffect.DefenseUp:
                break;

            case EItemEquipEffect.StrengthUp:
                break;

            default:
                break;
        }
    }

    //Called when the item is dequipped from a creature
    public virtual void OnRemove(SO_CreatureData _creatureData)
    {

    }
}
