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