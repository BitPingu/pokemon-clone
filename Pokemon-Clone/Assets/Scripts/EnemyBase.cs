using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public SO_CreatureData creature;
    //[SerializeField] private Material creatureMAT;

    private void Start()
    {
        if(gameObject.CompareTag("CaughtPokemon"))
        {
            // set creature data for wild pokemon
            creature = GameManager.Instance.GetStarterCreature();
            UpdateMaterialSprite();
        }
        else
        {
            // set creature data for wild pokemon
            GetComponent<CreatureBase>().Creature = new CreatureInstance(creature);
            UpdateMaterialSprite();
        }
          
    }

    public void OnValidate()
    {
        //Get the sprite and create a Material Instance
        UpdateMaterialSprite();
    }

    [ContextMenu("UpdateSprite")]
    public void UpdateMaterialSprite()
    {
        //if (creatureMAT == null) return;
        if (creature == null) return;
        if (creature.frontSprite == null) return;
        //Material mat = new Material(creatureMAT);

        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = creature.frontSprite;
        //creatureMAT.SetTexture("_MainTex", creature.frontSprite.texture);
    }
}
