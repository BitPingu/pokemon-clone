
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Move", menuName = "Scriptable Objects/Items/Move")]
public class SO_Item_Move : SO_Item_Base
{
    [Header("Move Item Properties")]
    [SerializeField] private List<SO_CreatureData> creaturesAbleToLearnMove;

    protected override void Awake()
    {
        base.Awake();
        itemType = EItemType.Move;
    }

    public override void OnPickup()
    {
        base.OnPickup();
    }

    public override void OnUse(SO_CreatureData _creatureData)
    {
        base.OnUse(_creatureData);

        if (!creaturesAbleToLearnMove.Contains(_creatureData.parentCreature)) return;

        GameManager.Instance.EventSender.OnRemoveItemFromInventoryEventSend(this);
    }

    public bool CanCreatureLearnMove(SO_CreatureData _creatureData)
    {
        if(_creatureData.parentCreature == null) creaturesAbleToLearnMove.Contains(_creatureData);
        return creaturesAbleToLearnMove.Contains(_creatureData.parentCreature);
    }
}
