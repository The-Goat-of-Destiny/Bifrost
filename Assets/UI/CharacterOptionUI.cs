using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterOptionUI : MonoBehaviour
{
    public Image Icon;
    public TMP_Text Label;
    public Button EmptySlot;
    public GameObject FullSlot;
    public CharacterOption SelectedOption;

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
        if (SelectedOption || Field is null)
        {
            EmptySlot.gameObject.SetActive(false);
            FullSlot.SetActive(true);
        }
        else
        {
            EmptySlot.gameObject.SetActive(true);
            Icon.sprite = (Field.GetValue(CreatorContext.SelectedCharacter) as CharacterOption)?.Icon;
            Label.text = (Field.GetValue(CreatorContext.SelectedCharacter) as CharacterOption)?.name;
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
