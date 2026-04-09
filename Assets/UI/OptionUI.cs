using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public CharacterOption Option;
    public Image Icon;
    public TMP_Text Name;
    public Button SelectButton;
    public Button InspectButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateVisuals()
    {
        if (Option)
        {
            Icon.sprite = Option.Icon;
            Name.text = Option.name;
        }
        else
        {
            Icon.sprite = null;
            Name.text = "???";
        }
    }

    public void SelectOption()
    {
        SelectionWindow parentWindow = transform.parent.GetComponent<SelectionWindow>();
        parentWindow.SelectOption(Option);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateVisuals();
    }
}
