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
    public UnityEvent enterBattle = new UnityEvent();

    public void EnterBattleEventSend()
    {
        Debug.Log($"Event Sender - Enter Battle");
        enterBattle.Invoke();
    }

    public UnityEvent exitBattle = new UnityEvent();
    public void ExitBattleEventSend()
    {
        Debug.Log($"Event Sender - Exit Battle");
        exitBattle.Invoke();
    }
    #endregion


    #region Movement Events
    //Called from any script that wants to enable/disable movement
    //received by the PlayerInputHandler 
    public EnableInputEvent enableInput = new EnableInputEvent();
    public void EnableInputEventSend(bool isEnabled)
    {
        Debug.Log($"Event Sender - Enable Input: {isEnabled}");
        enableInput.Invoke(isEnabled);
    }

    
    #endregion

}

public class EnableInputEvent : UnityEvent<bool> { }
