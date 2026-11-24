using UnityEngine;


public class SO_Item_Base : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public EItemType itemType;
    public Sprite itemSprite;

    protected virtual void Awake()
    {
       //Called when the Scriptable Object is created in the editor
    }

    public virtual void OnPickup()
    {
        //Send an event for the UI to show that we picked up an item
        GameManager.Instance.EventSender.OnItemPickedUpEventSend(itemName, itemDescription, itemType, itemSprite);
    }

    //Pass a creature through this method to use on specific creature. 
    public virtual void OnUse(SO_CreatureData _creatureData)
    {
        
    }
}
