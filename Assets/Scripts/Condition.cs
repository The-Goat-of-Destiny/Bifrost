using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class Condition
{
    public abstract bool CanFit(CharacterData character);
}

[Serializable]
public class TraitRequirement : Condition
{
    public List<Data.Trait> Traits = new();

    public override bool CanFit(CharacterData character)
    {
        foreach (Data.Trait trait in Traits)
        {
            if (!character.Traits.Contains(trait)) return false;
        }
        return true;
    }

    public override string ToString()
    {
        string result = "Requires ";
        foreach (Data.Trait trait in Traits)
        {
            result += trait.ToString() + " ";
        }
        if (Traits.Count > 1) result += "traits";
        else result += "trait";
        return result;
    }
}

[Serializable]
public class ProficiencyRequirement : Condition
{
    public string Target;
    public Proficiency Minimum;

    public override bool CanFit(CharacterData character)
    {
        // TODO
        return true;
    }
}


[Serializable]
public class LevelRequirement : Condition
{
    public int MinimumLevel = 0;

    public override bool CanFit(CharacterData character)
    {
        return character.Level >= MinimumLevel;
    }
}

[Serializable]
public class SpeedRequirement : Condition
{
    public int MinimumSpeed = 0;

    public override bool CanFit(CharacterData character)
    {
        return character.Speed >= MinimumSpeed;
    }
}

[Serializable]
public class WieldingRequirement : Condition
{
    [SerializeReference]
    public List<Condition> Requirements = new(); // Replace with ItemCondition List once ItemCondition is written

    public override bool CanFit(CharacterData character)
    {
        foreach (Condition condition in Requirements)
        {
            if (!condition.CanFit(character))
            {
                return false;
            }
        }
        return true;
    }
}

// This requirement is very specific, but comes up several times and would be too difficult to implement generically
[Serializable]
public class ShieldRaisedRequirement : Condition
{
    public override bool CanFit(CharacterData character)
    {
        // No inventory system yet
        return false;
    }
}

// Broad based generic, use more specific requirements if available
[Serializable]
public class StatRequirement : Condition
{
    public string Stat;
    public int Minimum = 0;

    public override bool CanFit(CharacterData character)
    {
        int stat = (int)typeof(CharacterData).GetField(Stat).GetValue(character);
        return stat >= Minimum;
    }
}