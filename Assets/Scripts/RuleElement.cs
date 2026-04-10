using System.Collections.Generic;
using System;

[Serializable]
public abstract class RuleElement
{

}

[Serializable]
public class DamageRule : RuleElement { public int damage; }

/// <summary>
/// ModifierRule may be used instead
/// </summary>
[Serializable]
public class SpeedRule : RuleElement
{
    public MovementType Type;
    public int Speed;
}

[Serializable]
public class ProficiencyRule : RuleElement
{
    public string Target;
    public Proficiency proficiency;
}

[Serializable]
public class TraitRule : RuleElement
{
    public List<Data.Trait> Traits;
}

[Serializable]
public class ResistanceRule : RuleElement
{
    public string DamageType;
    public int Resistance = 0;
    public string Formula;
}

[Serializable]
public class WeaknessRule : RuleElement
{
    public string DamageType;
    public int Weakness = 0;
    public string Formula;
}
