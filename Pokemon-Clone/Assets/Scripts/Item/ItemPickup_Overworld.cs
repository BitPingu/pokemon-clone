using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup_Overworld : MonoBehaviour
{
    [SerializeField] private SO_Item_Base itemType;

    //Called from the PlayerPickupItem.cs on the PlayerInputManager prefab
    public void OnPickup()
    {
        itemType.OnPickup();

        Destroy(gameObject);
    }
}
