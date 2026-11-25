using UnityEngine;

[CreateAssetMenu(fileName = "Item_Consumable", menuName = "Scriptable Objects/Items/Consumable")]
public class SO_Item_Consumable : SO_Item_Base
{
    [Header("Consumable Properties")]
    [SerializeField] private EItemConsumableEffect consumeEffect;

    [Tooltip("The amount of HP to heal if it's chosen as the consume Effect. \n -1 for MAX HP")]
    [SerializeField] private int healHPAmount; 

    protected override void Awake()
    {
        base.Awake();
        itemType = EItemType.Consumable;
    }

    public override void OnPickup()
    {
        base.OnPickup();
    }

    public override void OnUse(SO_CreatureData _creatureData)
    {
        base.OnUse(_creatureData);

        //Here we can change the effect of the Item based on the consumeEffect.
        switch (consumeEffect)
        {
            case EItemConsumableEffect.None:
                break;

            case EItemConsumableEffect.Heal_HP:
                _creatureData.UpdateCurrentHealth(healHPAmount);
                break;

            case EItemConsumableEffect.Heal_Burn:
                _creatureData.RemoveStatusCondition(EStatusCondition.Burn);
                break;

            case EItemConsumableEffect.Heal_Poison:
                _creatureData.RemoveStatusCondition(EStatusCondition.Poison);
                break;

            default:
                break;
        }

        GameManager.Instance.EventSender.OnRemoveItemFromInventoryEventSend(this);
    }
}   

