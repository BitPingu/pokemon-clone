using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    //This script is attached to the Player Input Handler Prefab.
    //These Functions are set in the "PlayerInput" component under the specific action

    private InputActionAsset action;
    private PlayerInput playerInput;

    //Input Variables
    private bool isInputEnabled = true;

    private void OnEnable()
    {
        GameManager.Instance.EventSender.enableInput.AddListener(SetEnableInput);
    }
    private void OnDisable()
    {
        GameManager.Instance.EventSender.enableInput.RemoveListener(SetEnableInput);
    }


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        action = playerInput.actions;
    }

    //Movement Variables and Action
    private Vector2 movement;
    public Vector2 Movement { get => movement; }

   
    public void MovementAction(InputAction.CallbackContext context)
    {
        if (context.started) return;
        if (!isInputEnabled) return;

        movement = context.ReadValue<Vector2>();
    }


    //Confirm Variables and Actions
    private bool confirm;
    public bool Confirm { get => confirm; }
   
    public void ConfirmAction(InputAction.CallbackContext context)
    {
        if (context.started) return;
        if (!isInputEnabled) return;

        confirm = context.ReadValue<bool>();

    }

    public bool PressedConfirmThisFrame()
    {
        if (!isInputEnabled) return false;
        return action.FindAction("Confirm").WasPressedThisFrame();
    }


    //Cancel Variables and Actions
    private bool cancel;
    private bool Cancel { get => cancel; }
    public void CancelAction(InputAction.CallbackContext context)
    {
        if (context.started) return;
        if (!isInputEnabled) return;

        cancel = context.ReadValue<bool>();
    }

    public bool PressedCancelThisFrame()
    {
        if (!isInputEnabled) return false;
        return action.FindAction("Cancel").WasPressedThisFrame();
    }


    //Getters and Setters
    private void SetEnableInput(bool isEnabled)
    {
        isInputEnabled = isEnabled;
    }

    public bool GetInputEnabled()
    {
        return isInputEnabled;
    }

}
