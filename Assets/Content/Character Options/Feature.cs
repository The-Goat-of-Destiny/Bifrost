using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Feature", menuName = "Content/Character Options/Feature", order = 0)]
public class Feature : CharacterOption
{
    public List<RuleElement> ruleElements = new();
}

[System.Serializable]
public class GrantVision : RuleElement
{
    public enum VisionType { Lowlight, Darkvision, GreaterDarkvision, Tremorsense, Scent, Wavesense, Lifesense, Thoughtsense }
    public VisionType Type;
    public int Range;
    public enum PrecisionDegree { Precise, Imprecise, Vague }
    public PrecisionDegree Precision = PrecisionDegree.Precise;
}

[System.Serializable]
public class RuleElement
{

}