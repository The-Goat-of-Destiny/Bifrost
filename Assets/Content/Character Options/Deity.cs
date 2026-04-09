using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deity", menuName = "Content/Character Options/Deity", order = 0)]
public class Deity : CharacterOption
{
    //public string DivineSkill;
    //public string FavoredWeapon;
    public bool HealFont = true;
    public bool HarmFont = true;
    public List<Domain> Domains = new();
    public List<Domain> AltDomains = new();
    public List<Spell> ClericSpells = new(3);
}
