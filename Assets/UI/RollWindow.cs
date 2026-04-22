using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RollWindow : MonoBehaviour
{
    public static RollWindow Instance;

    public CompositePackage Stat = new();
    public int DC = 10;

    [SerializeField]
    private TMP_Text Text;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateVisuals(Composite composite)
    {
        Stat = composite.Squash();
        Text.text = composite.Name + "\n";
        List<Modifier> modifiers = composite.SquashModifiers();
        foreach (Modifier modifier in modifiers)
        {
            Text.text += modifier.Label + ": " + modifier.Value.ToString() + " " + modifier.Type.ToString() + "\n";
        }
        gameObject.SetActive(true);
    }

    public void Roll(CharacterData character)
    {
        //ChatLog.Instance.NewMessage(RollBonus.ToString());
        gameObject.SetActive(false);
        RollData data = Dice.Check(DC, Stat);
        ChatLog.Instance.NewMessage(character, "", data.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
