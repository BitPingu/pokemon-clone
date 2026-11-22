using UnityEngine;


public class SO_Item_Base : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public EItemType itemType;

    protected virtual void Awake()
    {
       
    }
}
