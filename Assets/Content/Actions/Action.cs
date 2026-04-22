using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action", menuName = "Content/Action", order = 0)]
public class Action : CharacterOption
{
    public enum ActionCostType { Action, FreeAction, Reaction, Round, Minute, Hour, Day }
    public ActionCostType CostType = ActionCostType.Action;
    public int ActionCost = 1;

    public bool HasCheck = false;
    public CheckData Check;

    public void Perform(Token performer)
    {
        ChatLog.Instance.NewMessage(performer.Character, name, Description);
    }
}

[System.Serializable]
public class CheckData
{
    public bool TargettedDC = true;
    public string DCProficiency = "AC";
    public string Name = "Flat Check";
    public string AbilityScore = "None";
    public string ProficiencyName = "None";

    /*public CheckData(int dc, string checkName, string abilityScore, string proficiencyName)
    {
        DC = dc;
        Name = checkName;
        AbilityScore = abilityScore;
        ProficiencyName = proficiencyName;
    }*/

    public RollData Check(CharacterData roller, CharacterData target = null)
    {
        int roll = Dice.Roll(20);
        int ability = (int)typeof(CharacterData).GetField(AbilityScore).GetValue(roller);
        Proficiency proficiency = roller.Proficiencies[ProficiencyName];

        //int total = roll + ability + roller.ProficiencyBonus(proficiency);

        int DC = 10;
        if (TargettedDC && target)
        {
            DC = target.ProficiencyBonus(target.Proficiencies[DCProficiency]) + 10;
        }

        return new RollData(roll, new(ability, 0, 0, 0, roller.ProficiencyBonus(proficiency)), DC);
    }
}