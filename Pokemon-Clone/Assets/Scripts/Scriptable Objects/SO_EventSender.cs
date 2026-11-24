using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class can send events out from any script. You can access this script by accessing the GameManager.
/// Ex. Send Event:         GameManager.Instance.EventSender.EnterBattleEventSend();
/// Ex. Listen to Event:    GameManager.Instance.EventSender.enterBattle.AddListener();
/// </summary>

[CreateAssetMenu(fileName = "Event Sender", menuName = "Scriptable Objects/Event Sender")]
public class SO_EventSender : ScriptableObject
{
    #region Battle Events
    [System.NonSerialized]
    public UnityEvent enterBattle = new UnityEvent();

    public void EnterBattleEventSend()
    {
        Debug.Log($"Event Sender - Enter Battle");
        enterBattle.Invoke();
    }

    [System.NonSerialized]
    public UnityEvent exitBattle = new UnityEvent();
    public void ExitBattleEventSend()
    {
        Debug.Log($"Event Sender - Exit Battle");
        exitBattle.Invoke();
    }
    #endregion


    #region InputEvents
    //Called from any script that wants to enable/disable movement
    //received by the PlayerInputHandler 

    [System.NonSerialized]
    public EnableInputEvent enableInput = new EnableInputEvent();
    public void EnableInputEventSend(bool isEnabled)
    {
        Debug.Log($"Event Sender - Enable Input: {isEnabled}");
        enableInput.Invoke(isEnabled);
    }

    //Called from the PlayerInputHandler.cs
    [System.NonSerialized]
    public UnityEvent onAcceptPressedEvent = new UnityEvent();
    public void OnAcceptPressedEventSend()
    {
        onAcceptPressedEvent.Invoke();
    }

    #endregion

    #region Item Events
    //Called from the PlayerItemInput.cs when the player presses ACCEPT to pick up an item in the overworld
    [System.NonSerialized]
    public OnItemPickedUpEvent onItemPickedUpEvent = new OnItemPickedUpEvent();

    public void OnItemPickedUpEventSend(string _name, string _description, EItemType _type, Sprite _sprite)
    {
        onItemPickedUpEvent.Invoke(_name, _description, _type, _sprite);
    }

    [System.NonSerialized]
    public OnAddItemToInventoryEvent onAddItemToInventoryEvent = new OnAddItemToInventoryEvent();

    public void OnAddItemToInventoryEventSend(SO_Item_Base _item)
    {
        onAddItemToInventoryEvent.Invoke(_item);
    }

    [System.NonSerialized]
    public OnRemoveItemFromInventoryEvent onRemoveItemFromInventoryEvent = new OnRemoveItemFromInventoryEvent();

    public void OnRemoveItemFromInventoryEventSend(SO_Item_Base _item)
    {
        onRemoveItemFromInventoryEvent.Invoke(_item);
    }

    #endregion
}


public class EnableInputEvent : UnityEvent<bool> { }

public class OnItemPickedUpEvent : UnityEvent<string, string, EItemType, Sprite> { }
public class OnAddItemToInventoryEvent : UnityEvent<SO_Item_Base> { }
public class OnRemoveItemFromInventoryEvent : UnityEvent<SO_Item_Base> { }