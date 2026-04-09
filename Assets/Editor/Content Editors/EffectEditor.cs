using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Effect), true)]
public class EffectEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Effect effect = (Effect)target;

        if (effect == null || effect.Icon == null)
        {
            return null;
        }

        Texture2D texture = new(width, height);
        EditorUtility.CopySerialized(effect.Icon.texture, texture);
        return texture;
    }
}