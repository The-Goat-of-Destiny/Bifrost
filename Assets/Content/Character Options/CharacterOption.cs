using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOption : ScriptableObject
{
    public Rarity Rarity = Rarity.Common;
    public Sprite Icon;
    public int Level = 0;
    public List<Data.Trait> Traits;
    public List<CharacterOption> GrantedFeatures = new();

    [TextArea]
    [SerializeField] private string Description;
}
