using UnityEngine;

[CreateAssetMenu(fileName = "Item_Equip", menuName = "Scriptable Objects/Items/Equip")]
public class SO_Item_Equip : SO_Item_Base
{
    [Header("Equip Properties")]
    [SerializeField] private EItemEquipEffect itemEquipEffect;
    [SerializeField] private int equipIncrease;

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
        UpdateEquipEffect(_creatureData, 1);
    }

    //Called when the item is dequipped from a creature
    public virtual void OnRemove(SO_CreatureData _creatureData)
    {
        //Removing increase effect on creature
        UpdateEquipEffect(_creatureData, -1);
    }

    private void UpdateEquipEffect(SO_CreatureData _creatureData, int sign)
    {
        switch (itemEquipEffect)
        {
            case EItemEquipEffect.None:
                break;

            case EItemEquipEffect.HealthUp:
                _creatureData.UpdateCurrentEquipHealth(equipIncrease * sign);
                break;

            case EItemEquipEffect.SpeedUp:
                _creatureData.UpdateCurrentEquipSpeed(equipIncrease * sign);
                break;

            case EItemEquipEffect.DefenseUp:
                _creatureData.UpdateCurrentEquipDefense(equipIncrease * sign);
                break;

            case EItemEquipEffect.StrengthUp:
                _creatureData.UpdateCurrentEquipStrength(equipIncrease * sign);
                break;

            default:
                break;
        }
    }
}
