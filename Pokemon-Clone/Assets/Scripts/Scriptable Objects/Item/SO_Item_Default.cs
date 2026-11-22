using UnityEngine;

[CreateAssetMenu(fileName = "Default Item", menuName = "Scriptable Objects/Items/Default")]
public class SO_Item_Default : SO_Item_Base
{
    protected override void Awake()
    {
        base.Awake();
        itemType = EItemType.Default;
    }
}
