using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOption : ScriptableObject
{
    public Rarity Rarity = Rarity.Common;
    public Sprite Icon;
    public int Level = 0;
    public List<Data.Trait> Traits;

    [TextArea]
    [SerializeField] private string Description;

    public List<CharacterOption> GrantedFeatures = new();
    [SerializeReference]
    public List<RuleElement> RuleElements = new();
}
