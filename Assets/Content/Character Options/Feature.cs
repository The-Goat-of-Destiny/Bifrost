using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Feature", menuName = "Content/Character Options/Feature", order = 0)]
public class Feature : CharacterOption
{

}

[System.Serializable]
public class GrantVision : RuleElement
{
    public enum VisionType { Lowlight, Darkvision, GreaterDarkvision, Tremorsense, Scent, Wavesense, Lifesense, Thoughtsense }
    public VisionType visionType;
    public int Range;
    public enum PrecisionDegree { Precise, Imprecise, Vague }
    public PrecisionDegree Precision = PrecisionDegree.Precise;
}
