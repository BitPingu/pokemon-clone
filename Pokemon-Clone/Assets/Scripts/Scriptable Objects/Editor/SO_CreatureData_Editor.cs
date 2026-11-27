using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_CreatureData))]
[CanEditMultipleObjects]
public class SO_CreatureData_Editor : Editor
{
   
    //Update preview with the front sprite image
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        SO_CreatureData creature = (SO_CreatureData)target;

        if (creature == null || creature.frontSprite == null) return null;

        //The texture we create should be a supported format
        Texture2D tex = new Texture2D(width, height);

        //Copies the sprite data from Front Sprite and applies it to the texture
        if(AssetPreview.GetAssetPreview(creature.frontSprite) == null) return null;
        EditorUtility.CopySerialized(AssetPreview.GetAssetPreview(creature.frontSprite), tex);
       
        return tex;
    }
}
