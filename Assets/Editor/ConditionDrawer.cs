using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

[CustomPropertyDrawer(typeof(Condition), true)]
public class ConditionDrawer : PropertyDrawer
{
    private Type[] _subclasses;
    private string[] _subclassNames;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_subclasses == null) Init();

        EditorGUI.BeginProperty(position, label, property);

        // 1. Get current type
        string fullType = property.managedReferenceFullTypename;
        int currentIndex = -1;
        if (!string.IsNullOrEmpty(fullType))
        {
            string typeName = fullType.Split(' ').Last();
            currentIndex = Array.FindIndex(_subclasses, t => t.FullName == typeName);
        }

        // 2. Draw Type Dropdown
        Rect popupRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        int newIndex = EditorGUI.Popup(popupRect, "Requirement", currentIndex, _subclassNames);

        if (newIndex != currentIndex)
        {
            // Update the object to the new type
            property.managedReferenceValue = Activator.CreateInstance(_subclasses[newIndex]);
            property.serializedObject.ApplyModifiedProperties();
        }

        // 3. Draw subclass fields (indented)
        Rect fieldRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, position.height);
        EditorGUI.PropertyField(fieldRect, property, true);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Total height = Dropdown + Fields height
        return EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.singleLineHeight + 2;
    }

    private void Init()
    {
        _subclasses = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => typeof(Condition).IsAssignableFrom(t) && !t.IsAbstract)
            .ToArray();
        _subclassNames = _subclasses.Select(t => t.Name).ToArray();
    }
}
