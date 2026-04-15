using CustomAttributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Content/Character", order = 0)]
public class CharacterData : ScriptableObject
{
    [ExposedField] public int Level = 1;
    [ExposedField] public Class Class;
    [ExposedField] public Ancestry Ancestry;
    [ExposedField] public Heritage Heritage;
    [ExposedField] public Background Background;
    [ExposedField] public Deity Deity;

    public List<Data.Trait> Traits => Class.Traits.Union(Ancestry.Traits).Union(Heritage.Traits).Union(Background.Traits).ToList();

    public List<EffectInstance> Effects = new();

    public int GetModifier(string target)
    {
        int result = 0;
        foreach (EffectInstance effect in Effects)
        {
            foreach (RuleElement ruleElement in effect.Context.RuleElements)
            {
                // Apply modifications from Rule Element Modifiers
                if (ruleElement.GetType() == typeof(Modifier))
                {
                    Modifier mod = ((Modifier)ruleElement);
                    if (mod.Target == target)
                    {
                        if (mod.ScaleWithEffectValue) result += mod.Value * 1;
                        else result += mod.Value;
                    }
                }
            }
        }
        return result;
    }

    /// <summary>
    /// Expensive function, ONLY call when applying a new feature
    /// </summary>
    public void Recalculate()
    {
        foreach (FieldInfo field in GetType().GetFields())
        {
            if (field.FieldType == typeof(Composite))
            {
                ((Composite)field.GetValue(this)).Source = this;
            }
        }

        for (int i = 0; i < Level; i++)
        {
            foreach (CharacterOption option in Class.ClassFeatures[i].Features)
            {
                option.ApplyTo(this);
            }
        }
    }

    /// <summary>
    /// Ability scores can be looked up by index using the Data.Attribute enum
    /// </summary>
    [NamedArray(new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" })]
    public List<int> AbilityScores = new(6);

    [Tooltip("Modifies all checks except Flat Checks")]
    [ExposedField]
    public Composite All = new(0);

    [ExposedField] public Composite Strength = new(0);// { get => AbilityScores[(int)Data.Attribute.Strength] + GetModifier("Str"); }
    [ExposedField] public Composite Dexterity = new(0);// { get => AbilityScores[(int)Data.Attribute.Dexterity] + GetModifier("Dex"); }
    [ExposedField] public Composite Constitution = new(0);// { get => AbilityScores[(int)Data.Attribute.Constitution]; }
    [ExposedField] public Composite Intelligence = new(0);// { get => AbilityScores[(int)Data.Attribute.Intelligence]; }
    [ExposedField] public Composite Wisdom = new(0);// { get => AbilityScores[(int)Data.Attribute.Wisdom]; }
    [ExposedField] public Composite Charisma = new(0);// { get => AbilityScores[(int)Data.Attribute.Charisma]; }

    [ExposedProperty] public int MaxHealth { get => Ancestry.Hitpoints + (Class.Health + Constitution.Squash().Total()) * Level; }
    [ExposedField] public int Health;
    [ExposedField] public Composite AC = new(10, new List<string> { "Dexterity", "Level"});//, new List<Composite> { Dexterity });//{ get => 10 + Dexterity + Level; }
    [ExposedField] public Composite Reflex = new(0, new List<string> { "Dexterity", "Level"});// { get => Dexterity + Level; }
    [ExposedField] public Composite Will = new(0, new List<string> { "Wisdom", "Level" });
    [ExposedField] public Composite Fortitude = new(0, new List<string> { "Constitution", "Level" });
    [ExposedProperty] public int Speed { get => Ancestry.Speed; }

    public Dictionary<GrantVision.VisionType, int> GetVisionTypes {
        get => new();
        /*
        Dictionary<GrantVision.VisionType, int> visionTypes = new();
        foreach (FieldInfo field in typeof(self).GetFields())
        {
            if (field.FieldType.BaseType == typeof(CharacterOption))
            {
                visionTypes[field.GetValue().] = field.GetValue().;
            }
        }*/
    }

    private void OnValidate()
    {
        Recalculate();
    }
}
