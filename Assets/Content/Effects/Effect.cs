using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Content/Effect", order = 0)]
public class Effect : ScriptableObject
{
    public bool HasValue = false;
    // Duration is handled by the instantiated effect and whatever applies the effect
    public Sprite Icon;
    [Tooltip("A list of additional effects that last for as long as this effect is applied")]
    public List<Effect> Riders = new();
    public List<Modifier> Modifiers = new();
}

[System.Serializable]
public class Modifier
{
    public int Value = 1;
    [Tooltip("Whether to multiply the value by the value of the effect")]
    public bool ScaleWithEffectValue = false;
    public enum ModType { Untyped, Status, Item, Circumstance, Ability }
    public ModType Type = ModType.Untyped;
    // Field doesn't appear in inspector, requires custom property drawer, using String for now
    //public System.Reflection.FieldInfo Field;
    public string Target;
}