using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Content/Effect", order = 0)]
public class Effect : ScriptableObject
{
    public bool HasValue = false;
    // Duration is handled by the instantiated effect and whatever applies the effect
    public Sprite Icon;

    [Multiline]
    public string Description;

    [Tooltip("A list of additional effects that last for as long as this effect is applied")]
    public List<EffectInstance> Riders = new();
    //public List<Modifier> Modifiers = new();
    [SerializeReference]
    public List<RuleElement> RuleElements = new();

    public void ApplyTo(CharacterData character)
    {
        foreach (EffectInstance effect in Riders)
        {
            effect.ApplyTo(character);
        }
    }

    public void RemoveFrom(CharacterData character)
    {
        foreach (EffectInstance effect in Riders)
        {
            effect.RemoveFrom(character);
        }
    }

    private void OnValidate()
    {
        foreach (RuleElement rule in RuleElements)
        {
            rule.Label = name;
        }
    }
}