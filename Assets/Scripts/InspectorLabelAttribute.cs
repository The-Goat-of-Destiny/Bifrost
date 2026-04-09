using UnityEngine;

public class InspectorLabelAttribute : PropertyAttribute
{
    public string prefix;
    public InspectorLabelAttribute(string prefix) { this.prefix = prefix; }
}