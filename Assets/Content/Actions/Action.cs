using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action", menuName = "Content/Action", order = 0)]
public class Action : CharacterOption
{
    public enum ActionCostType { Action, FreeAction, Reaction, Round, Minute, Hour, Day }
    public ActionCostType CostType = ActionCostType.Action;
    public int ActionCost = 1;
}
