using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Token : MonoBehaviour
{
    public CharacterData Character;

    [Tooltip("For Testing")]
    public SkillCheck SkillCheck = new();

    public Image TargetOverlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [ContextMenu("Inspect")]
    public void Inspect()
    {
        Game.Manager.InspectWindow.Inspect(Character);
    }

    public void Select(bool multiSelect=false)
    {
        if (!multiSelect)
        {
            Game.Manager.SelectedTokens = new();
        }
        Game.Manager.SelectedTokens.Add(this);
    }

    [ContextMenu("Select")]
    public void Select()
    {
        Select(false);
    }

    public void Target(bool multiTarget = false)
    {
        if (!multiTarget)
        {
            Game.Manager.TargettedTokens = new();
        }
        Game.Manager.TargettedTokens.Add(this);
    }

    [ContextMenu("Target")]
    public void Target()
    {
        Target(false);
    }

    [ContextMenu("Full Heal")]
    public void FullHeal()
    {
        Character.Health = Character.MaxHealth;
    }

    [ContextMenu("Edit")]
    public void Edit()
    {
        CharacterCreator.Instance.SelectCharacter(Character);
    }

    [ContextMenu("Recalculate")]
    public void Recalculate()
    {
        Character.Recalculate();
    }

    public void ApplyEffect(Effect effect)
    {
        EffectInstance effectInstance = new EffectInstance(effect);
        Character.Effects.Add(effectInstance);
        effectInstance.ApplyTo(Character);
    }

    public void Check()
    {
        Composite skill = ((Composite)typeof(CharacterData).GetField(SkillCheck.Skill).GetValue(Character));
        print("Skill bonus: " + skill.Name + " " + skill.SquashModifiers().Count.ToString() + " " + skill.Squash().ToString() + " " + skill.Squash().Total().ToString());
        RollWindow.Instance.UpdateVisuals(skill);
        //ChatLog.Instance.NewMessage(Character.name, data.Data.Total().ToString() + data.Result().ToString());
        //ChatLog.Instance.NewMessage(Character.name, data.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        TargetOverlay.gameObject.SetActive(false);
    }
}
