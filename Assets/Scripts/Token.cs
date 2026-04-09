using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Token : MonoBehaviour
{
    public CharacterData Character;

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

    [ContextMenu("Edit")]
    public void Edit()
    {
        CharacterCreator.Instance.SelectCharacter(Character);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
