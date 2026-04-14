using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class Composite
{
    public int Base;
    public int Status;
    public int Circumstance;
    public int Item;
    public int Untyped;

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

    public virtual int Total(object source = null)
    {
        Composite Result = this;
        foreach (Modifier modifier in RelevantModifiers)
        {
            switch (modifier.Type)
            {
                case Modifier.ModType.Status:
                    Result.Status = Mathf.Max(Status, modifier.Value);
                    break;
                case Modifier.ModType.Circumstance:
                    Result.Circumstance = Mathf.Max(Status, modifier.Value);
                    break;
                case Modifier.ModType.Item:
                    Result.Item = Mathf.Max(Status, modifier.Value);
                    break;
                default:
                    Result.Untyped += modifier.Value;
                    break;
            }
        }
        if (source != null)
            Result = Result.Squash(source);
        return Result.Base + Result.Status + Result.Circumstance + Result.Item + Result.Untyped;
    }

    public virtual Composite Squash(object source)
    {
        Composite Result = (Composite)MemberwiseClone();
        foreach (string factorName in Factors)
        {
            FieldInfo factor = source.GetType().GetField(factorName);
            //Debug.Log(factorName);
            if (factor.FieldType == typeof(Composite))
            {
                Composite composite = ((Composite)factor.GetValue(source)).Squash(source);
                Result.Base += composite.Base;
                Result.Status = Mathf.Max(Result.Status, composite.Status);
                Result.Circumstance = Mathf.Max(Result.Status, composite.Status);
                Result.Item = Mathf.Max(Result.Status, composite.Status);
                Result.Untyped += composite.Untyped;
            }
            else if (factor.ReflectedType == typeof(int))
            {
                Result.Untyped += (int)factor.GetValue(source);
            }
        }
        return Result;
    }

    public string ToString(object source)
    {
        return Total(source).ToString();
    }
}

public class ProfComposite : Composite
{
    public Proficiency Proficiency;

    public ProfComposite(int _Base = 0, Proficiency _Proficiency = Proficiency.Untrained, List<string> _Factors = null, List<string> _Context = null) : base(_Base, _Factors, _Context)
    {
        Proficiency = _Proficiency;
    }

    public override int Total(object source = null)
    {
        ProfComposite Result = this;
        if (source != null)
            Result = ProfSquash(source);
        return Result.Base + Result.Status + Result.Circumstance + Result.Item + Result.Untyped;
    }

    public ProfComposite ProfSquash(object source)
    {
        ProfComposite Result = (ProfComposite)MemberwiseClone();
        foreach (string factorName in Factors)
        {
            FieldInfo factor = source.GetType().GetField(factorName);
            if (factor.ReflectedType.BaseType == typeof(Composite))
            {
                ProfComposite composite = (ProfComposite)(factor.GetValue(source) as ProfComposite).Squash(source);
                Result.Base += composite.Base;
                Result.Status = Mathf.Max(Result.Status, composite.Status);
                Result.Circumstance = Mathf.Max(Result.Status, composite.Status);
                Result.Item = Mathf.Max(Result.Status, composite.Status);
                Result.Untyped += composite.Untyped;
                if (factor.ReflectedType == typeof(ProfComposite))
                {
                    if (composite.Proficiency > Result.Proficiency)
                        Result.Proficiency = composite.Proficiency;
                }
            }
            else if (factor.ReflectedType == typeof(int))
            {
                Result.Untyped += (int)factor.GetValue(source);
            }
        }
        return Result;
    }
}