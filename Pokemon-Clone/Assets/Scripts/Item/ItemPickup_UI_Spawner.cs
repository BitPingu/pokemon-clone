
using UnityEngine;

/// <summary>
/// This class listens to whenever the player picks up an item. Sent from the SO_Item_Base OnUse() function
/// It spawns an item pickup UI prefab under the vertical layout group object with the correct information
/// 
/// Attached to the "ItemPickupPanel" game object in the overworld canvas.
/// </summary>

public class ItemPickup_UI_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPickupUIPrefab;
    [SerializeField] private Transform verticalLayoutGroupTransform;

    void Start()
    {
        GameManager.Instance.EventSender.onItemPickedUpEvent.AddListener(OnPickupItem);
    }

    private void OnPickupItem(string _name, string _description, EItemType _type, Sprite _sprite)
    {
        GameObject obj = Instantiate(itemPickupUIPrefab, verticalLayoutGroupTransform);

        obj.GetComponent<ItemPickup_UI>().FillInformation(_name, _description, _type, _sprite);
    }
}
