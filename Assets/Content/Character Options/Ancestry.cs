using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ancestry", menuName = "Content/Character Options/Ancestry", order = 0)]
public class Ancestry : CharacterOption
{
    public int Hitpoints = 6;
    public Data.Size Size = Data.Size.Medium;
    public int Speed = 25;
    public List<Data.Attribute> AttributeBoosts;
    public int FreeBoosts = 0;
    public List<Data.Attribute> AttributeFlaws;
    public List<Data.Languages> Languages;
    public int AdditionalLanguages = 0;

    //Granted Ancestry Feats
    //public List<Feat>

    private void OnValidate()
    {
        if (!Traits.Contains(Data.Trait.Ancestry))
        {
            Traits.Insert(0, Data.Trait.Ancestry);
        }
        // If not found in Global Data for Ancestries, automatically add
        if (Game.Data) if (!Game.Data.Ancestries.Contains(this)) Game.Data.Ancestries.Add(this);
    }
}
