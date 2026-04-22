using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOption : ScriptableObject
{
    public Rarity Rarity = Rarity.Common;
    public Sprite Icon;
    public int Level = 0;
    public List<Data.Trait> Traits;

    [SerializeReference]
    public List<Condition> Prerequisites = new();

    [TextArea]
    [SerializeField] protected string Description;

    public List<CharacterOption> GrantedFeatures = new();
    [SerializeReference]
    public List<RuleElement> RuleElements = new();

    public virtual void ApplyTo(CharacterData character)
    {
        foreach (RuleElement rule in RuleElements)
        {
            rule.ApplyTo(character);
        }
        foreach (CharacterOption option in GrantedFeatures)
        {
            option.ApplyTo(character);
        }
    }

    public virtual void RemoveFrom(CharacterData character)
    {
        foreach (RuleElement rule in RuleElements)
        {
            rule.RemoveFrom(character);
        }
        foreach (CharacterOption option in GrantedFeatures)
        {
            option.RemoveFrom(character);
        }
    }

    public bool CheckValidity(CharacterData character)
    {
        foreach (Condition condition in Prerequisites)
        {
            if (!condition.CanFit(character))
            {
                return false;
            }
        }
        return true;
    }
}
