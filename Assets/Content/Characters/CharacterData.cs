using CustomAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Character Data", menuName="Content/Character", order = 0)]
public class CharacterData : ScriptableObject
{
    [ExposedField] public int Level = 1;
    [ExposedField] public Class Class;
    [ExposedField] public Ancestry Ancestry;
    [ExposedField] public Heritage Heritage;
    [ExposedField] public Background Background;
    [ExposedField] public Deity Deity;

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
    /// Ability scores can be looked up by index using the Data.Attribute enum
    /// </summary>
    [NamedArray(new string[] {"Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma"})]
    public List<int> AbilityScores = new(6);

    [ExposedProperty] public int Strength { get => AbilityScores[(int)Data.Attribute.Strength] + GetModifier("Str"); }
    [ExposedProperty] public int Dexterity { get => AbilityScores[(int)Data.Attribute.Dexterity] + GetModifier("Dex"); }
    [ExposedProperty] public int Constitution { get => AbilityScores[(int)Data.Attribute.Constitution]; }
    [ExposedProperty] public int Intelligence { get => AbilityScores[(int)Data.Attribute.Intelligence]; }
    [ExposedProperty] public int Wisdom { get => AbilityScores[(int)Data.Attribute.Wisdom]; }
    [ExposedProperty] public int Charisma { get => AbilityScores[(int)Data.Attribute.Charisma]; }

    [ExposedProperty] public int MaxHealth { get => Ancestry.Hitpoints + (Class.Health + Constitution) * Level; }
    [ExposedField] public int Health;
    [ExposedProperty] public int AC { get => 10 + Dexterity + Level; }
    [ExposedProperty] public int Reflex { get => Dexterity + Level; }
    [ExposedProperty] public int Will { get => Wisdom + Level; }
    [ExposedProperty] public int Fortitude { get => Constitution + Level; }
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
}
