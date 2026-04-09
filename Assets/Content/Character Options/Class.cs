using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Class", menuName = "Content/Character Options/Class", order = 0)]
public class Class : CharacterOption
{
    public int Health = 6;

    public Proficiency PerceptionProficiency = Proficiency.Trained;
    public Proficiency FortitudeProficiency = Proficiency.Trained;
    public Proficiency ReflexProficiency = Proficiency.Trained;
    public Proficiency WillProficiency = Proficiency.Trained;

    // Might change to dictionary, or list of feature/level elements
    public List<ClassFeature> ClassFeatures = new();
}

[System.Serializable]
public class ClassFeature
{
    public CharacterOption Feature;
    public int Level = 1;
}