
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private List<SO_Item_Base> itemsInInventory = new List<SO_Item_Base>();

    [Header("Specific Categories")]
    [SerializeField] private List<SO_Item_Default> itemsDefault = new List<SO_Item_Default>();
    [SerializeField] private List<SO_Item_Consumable> itemsConsumable = new List<SO_Item_Consumable>();
    [SerializeField] private List<SO_Item_Equip> itemsEquip = new List<SO_Item_Equip>();
    [SerializeField] private List<SO_Item_Move> itemsMove = new List<SO_Item_Move>();

    private void Awake()
    {
        //Singleton Pattern. Only 1 inventory manager at a time
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Bind events to functions on add or remove an item
        GameManager.Instance.EventSender.onAddItemToInventoryEvent.AddListener(OnAddItemToInventory);
        GameManager.Instance.EventSender.onRemoveItemFromInventoryEvent.AddListener(OnRemoveItemFromInventory);

        //Categorize items into different lists if starting with a list of items
        foreach(SO_Item_Base item in itemsInInventory)
        {
            AddItemToSpecificList(item);
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.EventSender.onAddItemToInventoryEvent.RemoveListener(OnAddItemToInventory);
        GameManager.Instance.EventSender.onRemoveItemFromInventoryEvent.RemoveListener(OnRemoveItemFromInventory);
    }

    private void OnAddItemToInventory(SO_Item_Base _item)
    {
        Debug.Log($"Inventory Manager - Adding Item: {_item.itemName} to Inventory");
        itemsInInventory.Add(_item);
        AddItemToSpecificList(_item);
    }

    private void OnRemoveItemFromInventory(SO_Item_Base _item)
    {
        Debug.Log($"Inventory Manager - Removing Item: {_item.itemName} from Inventory");
        itemsInInventory.Remove(_item);
        RemoveItemFromSpecificList(_item);
    }

    private void AddItemToSpecificList(SO_Item_Base _item)
    {
        switch (_item.itemType)
        {
            case EItemType.Default:
                itemsDefault.Add(_item as SO_Item_Default);
                break;

            case EItemType.Equip:
                itemsEquip.Add(_item as SO_Item_Equip);
                break;

            case EItemType.Consumable:
                itemsConsumable.Add(_item as SO_Item_Consumable);
                break;

            case EItemType.Move:
                itemsMove.Add(_item as SO_Item_Move);
                break;

        }

    }

    private void RemoveItemFromSpecificList(SO_Item_Base _item)
    {
        switch (_item.itemType)
        {
            case EItemType.Default:
                itemsDefault.Remove(_item as SO_Item_Default);
                break;

            case EItemType.Equip:
                itemsEquip.Remove(_item as SO_Item_Equip);
                break;

            case EItemType.Consumable:
                itemsConsumable.Remove(_item as SO_Item_Consumable);
                break;

            case EItemType.Move:
                itemsMove.Remove(_item as SO_Item_Move);
                break;

        }
    }
}
