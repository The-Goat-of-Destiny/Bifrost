using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Deity), true)]
public class DeityEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Deity deity = (Deity)target;

        if (deity == null || deity.Icon == null)
        {
            return null;
        }

        Texture2D texture = new(width, height);
        EditorUtility.CopySerialized(deity.Icon.texture, texture);
        return texture;
    }
}