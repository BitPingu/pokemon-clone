
using UnityEngine;

public class PlayerPickupInput : MonoBehaviour
{
    private PlayerCollision playerCollision;

    private void Start()
    {
        playerCollision = GetComponentInParent<PlayerCollision>();

        GameManager.Instance.EventSender.onAcceptPressedEvent.AddListener(PressedAccept);    
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.EventSender.onAcceptPressedEvent.AddListener(PressedAccept);
    }

    private void OnDisable()
    {
        if(GameManager.Instance != null)
            GameManager.Instance.EventSender.onAcceptPressedEvent.RemoveListener(PressedAccept);
    }

    private void PressedAccept()
    {
        if (playerCollision == null) return;

        if (playerCollision.GetTouchedItem() == null) return;

        playerCollision.GetTouchedItem().OnPickup();
    }
}
