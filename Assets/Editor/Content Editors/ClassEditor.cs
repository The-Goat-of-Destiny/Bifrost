using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Class), true)]
public class ClassEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Class _class = (Class)target;

        if (_class == null || _class.Icon == null)
        {
            return null;
        }

        Texture2D texture = new(width, height);
        EditorUtility.CopySerialized(_class.Icon.texture, texture);
        return texture;
    }
}