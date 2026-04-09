using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InspectorLabelAttribute))]
public class InspectorLabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        InspectorLabelAttribute labelAttr = (InspectorLabelAttribute)attribute;

        // Extract the index from the property path (e.g., "myList.Array.data[0]")
        int index = int.Parse(property.propertyPath.Split('[', ']')[1]);

        // Create the new label: "Level 1", "Level 2", etc.
        string newLabel = $"{labelAttr.prefix} {index + 1}";

        EditorGUI.PropertyField(position, property, new GUIContent(newLabel), true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}