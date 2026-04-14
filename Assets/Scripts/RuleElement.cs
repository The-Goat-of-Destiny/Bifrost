using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;

[Serializable]
public abstract class RuleElement
{
    [SerializeReference]
    public List<Condition> Conditions = new();
    
    public virtual void ApplyTo(CharacterData character)
    {

    }

    public virtual void RemoveFrom(CharacterData character)
    {

    }
}

[Serializable]
public class Modifier : RuleElement
{
    public int Value = 1;
    [Tooltip("Whether to multiply the value by the value of the effect")]
    public bool ScaleWithEffectValue = false;
    public enum ModType { Untyped, Status, Item, Circumstance, Ability }
    public ModType Type = ModType.Untyped;
    // Field doesn't appear in inspector, requires custom property drawer, using String for now
    //public System.Reflection.FieldInfo Field;
    public string Target;

    public override void ApplyTo(CharacterData character)
    {
        FieldInfo field = typeof(CharacterData).GetField(Target);
        if (field.FieldType == typeof(Composite))
        {
            ((Composite)field.GetValue(character)).RelevantModifiers.Add(this);
        }
    }

    public override void RemoveFrom(CharacterData character)
    {
        FieldInfo field = typeof(CharacterData).GetField(Target);
        if (field.FieldType == typeof(Composite))
        {
            ((Composite)field.GetValue(character)).RelevantModifiers.Remove(this);
        }
    }
}

[Serializable]
public class DamageRule : RuleElement { public int damage; }

/// <summary>
/// ModifierRule may be used instead
/// </summary>
[Serializable]
public class SpeedRule : RuleElement
{
    public MovementType Type;
    public int Speed;
}

[Serializable]
public class ProficiencyRule : RuleElement
{
    public string Target;
    public Proficiency proficiency;

    private Proficiency formerProficiency;

    public override void ApplyTo(CharacterData character)
    {
        base.ApplyTo(character);
        formerProficiency = (Proficiency)typeof(CharacterData).GetField(Target).GetValue(character);
        typeof(CharacterData).GetField(Target).SetValue(character, proficiency);

    }

    public override void RemoveFrom(CharacterData character)
    {
        base.ApplyTo(character);
        typeof(CharacterData).GetField(Target).SetValue(character, formerProficiency);
    }
}

[Serializable]
public class TraitRule : RuleElement
{
    public List<Data.Trait> Traits;
}

[Serializable]
public class ResistanceRule : RuleElement
{
    public string DamageType;
    public int Resistance = 0;
    public string Formula;
}

[Serializable]
public class WeaknessRule : RuleElement
{
    public string DamageType;
    public int Weakness = 0;
    public string Formula;
}
