using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Class", menuName = "Content/Character Options/Class", order = 0)]
public class Class : CharacterOption
{
    public int Health = 6;

    [Header("Saving Throws")]
    public Proficiency PerceptionProficiency = Proficiency.Trained;
    public Proficiency FortitudeProficiency = Proficiency.Trained;
    public Proficiency ReflexProficiency = Proficiency.Trained;
    public Proficiency WillProficiency = Proficiency.Trained;

    [Header("Defenses")]
    public Proficiency UnarmoredProficiency = Proficiency.Trained;
    public Proficiency LightArmorProficiency = Proficiency.Untrained;
    public Proficiency MediumArmorProficiency = Proficiency.Untrained;
    public Proficiency HeavyArmorProficiency = Proficiency.Untrained;

    // Might change to dictionary, or list of feature/level elements
    [InspectorLabel("Level")]
    public List<FeatureSet> ClassFeatures = new();
}

[System.Serializable]
public class ClassFeature
{
    public CharacterOption Feature;
    public int Level = 1;
}

[System.Serializable]
public class FeatureSet
{
    public List<CharacterOption> Features = new();
}