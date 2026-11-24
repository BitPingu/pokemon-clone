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

    private void Start()
    {
        if(GameManager.Instance != null)
            GameManager.Instance.EventSender.enableInput.AddListener(SetEnableInput);
    }
    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.EventSender.enableInput.AddListener(SetEnableInput);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
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

    // Fight Variables and Actions
    private bool fight;
    public bool FightCommand { get => fight; }
    public void FightAction(InputAction.CallbackContext context)
    {
        if (context.started) return;
        if (!isInputEnabled) return;

        fight = context.ReadValue<bool>();
    }

    public bool PressedFightThisFrame()
    {
        if (!isInputEnabled) return false;
        return action.FindAction("Fight").WasPressedThisFrame();
    }

    // Catch Variables and Actions
    private bool catchAction;
    public bool CatchCommand { get => catchAction; }
    public void CatchAction(InputAction.CallbackContext context)
    {
        if (context.started) return;
        if (!isInputEnabled) return;

        catchAction = context.ReadValue<bool>();
    }

    public bool PressedCatchThisFrame()
    {
        if (!isInputEnabled) return false;
        return action.FindAction("Catch").WasPressedThisFrame();
    }

    // Run Variables and Actions
    private bool run;
    public bool RunCommand { get => run; }
    public void RunAction(InputAction.CallbackContext context)
    {
        if (context.started) return;
        if (!isInputEnabled) return;

        run = context.ReadValue<bool>();
    }

    public bool PressedRunThisFrame()
    {
        if (!isInputEnabled) return false;
        return action.FindAction("Run").WasPressedThisFrame();
    }


    //Confirm Variables and Actions
    private float confirm;
    public float Confirm { get => confirm; }
   
    public void ConfirmAction(InputAction.CallbackContext context)
    {
        if (!isInputEnabled) return;

        if (context.started)
        {
            GameManager.Instance.EventSender.OnAcceptPressedEventSend();
            return;
        }

        confirm = context.ReadValue<float>();
      
    }

    public bool PressedConfirmThisFrame()
    {
        if (!isInputEnabled) return false;
        return action.FindAction("Confirm").WasPressedThisFrame();
    }


    //Cancel Variables and Actions
    private float cancel;
    private float Cancel { get => cancel; }
    public void CancelAction(InputAction.CallbackContext context)
    {
        if (context.started) return;
        if (!isInputEnabled) return;

        cancel = context.ReadValue<float>();
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
