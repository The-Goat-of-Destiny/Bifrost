using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterOption), true)]
public class CharacterOptionEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        CharacterOption characterOption = (CharacterOption)target;

        if (characterOption == null || characterOption.Icon == null)
        {
            return null;
        }

        Texture2D texture = new(width, height);
        EditorUtility.CopySerialized(characterOption.Icon.texture, texture);
        return texture;
    }
}