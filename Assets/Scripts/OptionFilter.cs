using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionFilter
{
    public int MinLevel = -1;
    public int MaxLevel = 99;

    public List<Data.Trait> TraitWhitelist = new();
    public List<Data.Trait> TraitBlacklist = new();

    public bool Check(CharacterOption option)
    {
        if (TraitWhitelist.Count > 0)
        {
            foreach (Data.Trait trait in option.Traits)
            {
                if (!TraitWhitelist.Contains(trait)) return false;
            }
        }
        foreach (Data.Trait trait in option.Traits)
        {
            if (TraitBlacklist.Contains(trait)) return false;
        }
        if (option.Level < MinLevel) return false;
        if (option.Level > MaxLevel) return false;

        return true;
    }
}
