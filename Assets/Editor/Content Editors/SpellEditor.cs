using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spell), true)]
public class SpellEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Spell spell = (Spell)target;

        if (spell == null || spell.Icon == null)
        {
            return null;
        }

        Texture2D texture = new(width, height);
        EditorUtility.CopySerialized(spell.Icon.texture, texture);
        return texture;
    }
}