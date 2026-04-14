using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SelectionWindow : MonoBehaviour
{
    public CharacterCreator CreatorContext;
    public FieldInfo TargetField;
    [SerializeField] private OptionUI OptionUIPrefab;

    public OptionFilter Filter;

    // Start is called before the first frame update
    void Start()
    {
        GenerateOptions();
    }

    public void GenerateOptions()
    {
        if (TargetField == null)
        {
            Debug.LogWarning("No target field");
            return;
        }
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        foreach (CharacterOption option in Game.Data.GetOptions())
        {
            // Check if option matches field type
            if (option.GetType() == TargetField.FieldType)
            // Check if option passes filter
            //if (Filter != null && Filter.Check(option))
            {
                if (option.CheckValidity(CreatorContext.SelectedCharacter))
                {
                    OptionUI newOptionUI = Instantiate(OptionUIPrefab, transform);
                    newOptionUI.Option = option;
                    newOptionUI.UpdateVisuals();
                    // If option is already selected, modify visuals
                    //if (option == TargetField.GetValue(CreatorContext.SelectedCharacter))
                }
            }
        }
    }

    public void SelectOption(CharacterOption option)
    {
        if (!CreatorContext)
        {
            Debug.LogWarning("No character context exists for this Selection Window");
            return;
        }
        TargetField.SetValue(CreatorContext.SelectedCharacter, option);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
