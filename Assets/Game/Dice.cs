using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public static class Dice
{
    public static int Roll(int max = 20)
    {
        return Random.Range(0, max) + 1;
    }

    public static RollData Check(int DC = 10, int modifier = 0)
    {
        int roll = Roll(20);
        int total = roll + modifier;


        return new RollData(roll, new(), DC);
    }

    public static RollData Check(int DC = 10, CompositePackage stat = default)
    {
        int roll = Roll(20);

        return new RollData(roll, stat, DC);
    }
}

public class RollData
{
    // If roll is 0, then there must be an error
    public int Roll = 0;
    public CompositePackage Data;
    public int DC = 10;

    public RollResult Result()
    {
        int total = Roll + Data.Total();
        RollResult result;
        if (total <= DC - 10)
        {
            result = RollResult.CritFail;
        }
        else if (total >= DC + 10)
        {
            result = RollResult.CritSuccess;
        }
        else if (total >= DC)
        {
            result = RollResult.Success;
        }
        else
        {
            result = RollResult.Fail;
        }
        if (Roll == 20)
        {
            result = (RollResult)Mathf.Min((int)RollResult.CritSuccess, (int)result + 1);
        }
        if (Roll == 1)
        {
            result = (RollResult)Mathf.Max((int)RollResult.CritFail, (int)result - 1);
        }
        return result;
    }

    public RollData(int roll, CompositePackage stat, int dc)
    {
        Roll = roll;
        Data = stat;
        DC = dc;
    }

    public override string ToString()
    {
        string result = (Roll + Data.Total()).ToString();
        if (Result() == RollResult.CritSuccess) result += "<color=#00FF00>";
        else if (Result() == RollResult.CritFail) result += "<color=#FF0000>";
        else result += "<color=#000000>";
        result += ": " + Roll.ToString() + " + (" + Data.ToString() + ")";
        result += " vs " + DC.ToString() + ":\n";
        if (Result() == RollResult.CritSuccess) result += "Critical Success";
        else if (Result() == RollResult.Success) result += "Success";
        else if (Result() == RollResult.CritFail) result += "Critical Failure";
        else result += "Failure";
        result += "</color>";
        return result;
    }
}

public enum RollResult { CritFail, Fail, Success, CritSuccess }

[System.Serializable]
public class SkillCheck
{
    public string name = "Check";
    public int DC = 10;
    public string Skill;
    public List<Data.Trait> Traits;
    public List<string> Context;

    public SkillCheck(string _name="Check", int dc=10, string skill="Flat", List<Data.Trait> traits = default, List<string> context=default)
    {
        name = _name;
        DC = dc;
        Skill = skill;
        Traits = traits;
        Context = context;
    }
}