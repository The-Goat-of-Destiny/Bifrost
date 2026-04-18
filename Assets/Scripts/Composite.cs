using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class Composite
{
    public string Name = "Stat";
    public object Source;

    public int Base;
    public List<string> Factors = new();
    public List<string> Context = new();

    [SerializeReference]
    public List<Modifier> RelevantModifiers = new();

    public Composite(string name = "Stat", int _Base = 0, List<string> _Factors = default, List<string> _Context = default)
    {
        Name = name;
        Base = _Base;
        Factors = _Factors;
        if (_Factors is null)
        {
            Factors = new();
        }
        Factors.Add("All");
        Context = _Context;
    }

    /// <summary>
    /// Squashes Relevant Modifiers and sub-modifiers into a CompositePackage
    /// </summary>
    /// <param name="source"></param>
    /// <returns>Data structure containing each type of bonus/penalty</returns>
    public virtual CompositePackage Squash()
    {
        int _base = 0;
        int statusBonus = 0;
        int statusPenalty = 0;
        int circumstanceBonus = 0;
        int circumstancePenalty = 0;
        int itemBonus = 0;
        int itemPenalty = 0;
        int untyped = 0;
        List<Modifier> modifiers = SquashModifiers();
        foreach (string factor in Factors)
        {
            FieldInfo field = Source.GetType().GetField(factor);
            if (field.FieldType == typeof(Composite))
            {
                Composite composite = (Composite)field.GetValue(Source);
            }
            else if (field.FieldType == typeof(int))
            {
                untyped += (int)field.GetValue(Source);
            }
        }
        foreach (Modifier modifier in modifiers)
        {
            if (modifier.Value > 0)
            {
                switch (modifier.Type)
                {
                    case Modifier.ModType.Status:
                        statusBonus = Mathf.Max(statusBonus, modifier.Value);
                        break;
                    case Modifier.ModType.Circumstance:
                        circumstanceBonus = Mathf.Max(circumstanceBonus, modifier.Value);
                        break;
                    case Modifier.ModType.Item:
                        itemBonus = Mathf.Max(itemBonus, modifier.Value);
                        break;
                    default:
                        untyped += modifier.Value;
                        break;
                }
            }
            else
            {
                switch (modifier.Type)
                {
                    case Modifier.ModType.Status:
                        statusPenalty = Mathf.Min(statusPenalty, modifier.Value);
                        break;
                    case Modifier.ModType.Circumstance:
                        circumstancePenalty = Mathf.Min(circumstancePenalty, modifier.Value);
                        break;
                    case Modifier.ModType.Item:
                        itemPenalty = Mathf.Min(itemPenalty, modifier.Value);
                        break;
                    default:
                        untyped += modifier.Value;
                        break;
                }
            }
        }
        return new CompositePackage(_base, statusBonus + statusPenalty, circumstanceBonus + circumstancePenalty, itemBonus + itemPenalty, untyped);
    }

    public List<Modifier> SquashModifiers()
    {
        List<Modifier> modifiers = new();
        modifiers.AddRange(RelevantModifiers);
        //Debug.Log(Name + " " + modifiers.Count.ToString());
        foreach (string factor in Factors)
        {
            FieldInfo field = Source.GetType().GetField(factor);
            if (field.FieldType == typeof(Composite))
            {
                Composite composite = (Composite)field.GetValue(Source);
                //modifiers.AddRange(composite.RelevantModifiers);
                //Debug.Log(composite.Name + " " + modifiers.ToString());
                modifiers.AddRange(composite.SquashModifiers());
            }
        }
        return modifiers;
    }

    public override string ToString()
    {
        return Squash().ToString();
    }
}

public class ProfComposite : Composite
{
    public Proficiency Proficiency;

    public ProfComposite(string name="Stat", int _Base = 0, Proficiency _Proficiency = Proficiency.Untrained, List<string> _Factors = null, List<string> _Context = null) : base(name, _Base, _Factors, _Context)
    {
        Proficiency = _Proficiency;
    }
}

public struct CompositePackage
{
    public int Base;
    public int Status;
    public int Circumstance;
    public int Item;
    public int Untyped;

    public CompositePackage(int _base = 0, int status=0, int circumstance=0, int item=0, int untyped=0)
    {
        Base = _base;
        Status = status;
        Circumstance = circumstance;
        Item = item;
        Untyped = untyped;
    }

    public readonly int Total()
    {
        return Base + Status + Circumstance + Item + Untyped;
    }

    public override readonly string ToString()
    {
        return Base.ToString() + " + " + Status.ToString() + " + " + Circumstance.ToString() + " + " + Item.ToString() + " + " + Untyped.ToString();
    }
}