using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Content/Spell", order = 0)]
public class Spell : CharacterOption
{
    public enum Tradition { Arcane, Occult, Primal, Divine };
    public List<Tradition> Traditions = new();
}
