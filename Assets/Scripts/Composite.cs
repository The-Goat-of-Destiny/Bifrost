using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class Composite
{
    public object Source;

    public int Base;
    public List<string> Factors = new();
    public List<string> Context = new();

    [SerializeReference]
    public List<Modifier> RelevantModifiers = new();

    public Composite(int _Base = 0, List<string> _Factors = default, List<string> _Context = default)
    {
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
        int status = 0;
        int circumstance = 0;
        int item = 0;
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
            switch (modifier.Type)
            {
                case Modifier.ModType.Status:
                    status = Mathf.Max(status, modifier.Value);
                    break;
                case Modifier.ModType.Circumstance:
                    circumstance = Mathf.Max(circumstance, modifier.Value);
                    break;
                case Modifier.ModType.Item:
                    item = Mathf.Max(item, modifier.Value);
                    break;
                default:
                    untyped += modifier.Value;
                    break;
            }
        }
        return new CompositePackage(_base, status, circumstance, item, untyped);
    }

    public List<Modifier> SquashModifiers()
    {
        List<Modifier> modifiers = RelevantModifiers;
        foreach (string factor in Factors)
        {
            FieldInfo field = Source.GetType().GetField(factor);
            if (field.FieldType == typeof(Composite))
            {
                Composite composite = (Composite)field.GetValue(Source);
                modifiers.AddRange(composite.RelevantModifiers);
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

    public ProfComposite(int _Base = 0, Proficiency _Proficiency = Proficiency.Untrained, List<string> _Factors = null, List<string> _Context = null) : base(_Base, _Factors, _Context)
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

    public int Total()
    {
        return Base + Status + Circumstance + Item + Untyped;
    }

    public override readonly string ToString()
    {
        return Base.ToString() + " + " + Status.ToString() + " + " + Circumstance.ToString() + " + " + Item.ToString() + " + " + Untyped.ToString();
    }
}