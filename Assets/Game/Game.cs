using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static GameManager Manager;
    public static GlobalData Data;

    public enum Degree { CriticalFailure, Failure, Success, CriticalSuccess }

    public static Degree Check(int bonus=0, int DC=10)
    {
        int roll = Random.Range(1, 21);
        Degree result;
        if (roll + bonus >= DC + 10)
        {
            result = Degree.CriticalSuccess;
        }
        else if (roll + bonus >= DC)
        {
            result = Degree.Success;
        }
        else if (roll + bonus <= DC - 10)
        {
            result = Degree.CriticalFailure;
        }
        else
        {
            result = Degree.Failure;
        }
        if (roll == 20)
        {
            result += 1;
        }
        else if (roll == 1)
        {
            result -= 1;
        }
        if (result < 0) result = Degree.CriticalFailure;
        else if (result > Degree.CriticalSuccess) result = Degree.CriticalSuccess;
        return result;
    }
}

public enum Proficiency { Untrained, Trained, Expert, Master, Legendary }
public enum Rarity { Common, Uncommon, Rare, Unique }