using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterOptionUI : MonoBehaviour
{
    public Image Icon;
    public TooltipInfo RequirementWidget;
    public TMP_Text Label;
    public Button EmptySlot;
    public GameObject FullSlot;

    [SerializeField] private SelectionWindow SelectionWindowPrefab;

    public CharacterCreator CreatorContext;
    public FieldInfo Field;
    public OptionFilter Filter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateVisuals()
    {
        if (Field is null)
        {
            EmptySlot.gameObject.SetActive(false);
            FullSlot.SetActive(true);
        }
        else
        {
            CharacterOption selectedOption = (Field.GetValue(CreatorContext.SelectedCharacter) as CharacterOption);
            EmptySlot.gameObject.SetActive(true);
            Icon.sprite = selectedOption.Icon;
            Label.text = selectedOption.name;
            if (selectedOption)
            {
                RequirementWidget.gameObject.SetActive(selectedOption.Prerequisites.Count != 0);
                string requirementsText = "";
                foreach (Condition condition in selectedOption.Prerequisites)
                {
                    requirementsText += condition.ToString() + "\n";
                }
                RequirementWidget.SetInfo("Requirements", requirementsText);
            }
            FullSlot.SetActive(false);
        }
    }

    public void SelectionPrompt()
    {
        SelectionWindow newWindow = Instantiate(SelectionWindowPrefab, CreatorContext.transform);
        newWindow.CreatorContext = CreatorContext;
        newWindow.TargetField = Field;
        newWindow.Filter = Filter;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisuals();
    }
}
