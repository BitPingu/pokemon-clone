using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemSprite;
    [SerializeField] private float timeToDestroy = 3f;

    private void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    public void FillInformation(string _name, string _description, EItemType _type, Sprite _sprite)
    {
        itemName.text = _name;
        itemSprite.sprite = _sprite;
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(timeToDestroy);

        Destroy(gameObject);
    }

}
