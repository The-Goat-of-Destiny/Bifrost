using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public CharacterData SelectedCharacter;

    [SerializeField] private TMP_Text CharacterName;
    public CharacterOptionUI CharacterOptionUIPrefab;
    public Transform OptionsList;
    public Transform FeatsList;
    public Transform FeaturesList;

    public static CharacterCreator Instance;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        SelectCharacter(SelectedCharacter);
    }

    public void SelectCharacter(CharacterData characterData)
    {
        if (CharacterName) CharacterName.text = characterData.name;
        foreach (Transform t in OptionsList)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in FeatsList)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in FeaturesList)
        {
            Destroy(t.gameObject);
        }
        SelectedCharacter = characterData;

        // Generate Character Option slots for Class, Ancestry, Heritage, etc
        foreach (FieldInfo field in typeof(CharacterData).GetFields())
        {
            if (field.FieldType.BaseType == typeof(CharacterOption))
            {
                CharacterOptionUI newOptionUI = Instantiate(CharacterOptionUIPrefab, OptionsList);
                newOptionUI.Field = field;
                newOptionUI.Label.text = field.Name;
                newOptionUI.Filter.MaxLevel = 5; // Just for testing, need to replace with the correct level info and other data
                newOptionUI.CreatorContext = this;
            }
        }

        // Generate Feat slots
        // Iterate through levels
        for (int i = 1; i <= characterData.Level; i++)
        {
            if (i % 2 == 0)
            {
                CharacterOptionUI newOptionUI = Instantiate(CharacterOptionUIPrefab, FeatsList);
                newOptionUI.Field = null; // Can't use FieldInfo here, as these need to be stored in a list or other datastructure
                newOptionUI.Label.text = "Class Feat";
                newOptionUI.Filter.TraitWhitelist.AddRange(characterData.Class.Traits);
                newOptionUI.Filter.MaxLevel = i;
                newOptionUI.CreatorContext = this;
            }
            if (i % 3 == 0)
            {
                CharacterOptionUI newOptionUI = Instantiate(CharacterOptionUIPrefab, FeatsList);
                newOptionUI.Field = null; // Can't use FieldInfo here, as these need to be stored in a list or other datastructure
                newOptionUI.Label.text = "General Feat";
                newOptionUI.Filter.TraitWhitelist.Add(Data.Trait.General);
                newOptionUI.Filter.MaxLevel = i;
                newOptionUI.CreatorContext = this;
            }
            if (i % 2 == 0)
            {
                CharacterOptionUI newOptionUI = Instantiate(CharacterOptionUIPrefab, FeatsList);
                newOptionUI.Field = null; // Can't use FieldInfo here, as these need to be stored in a list or other datastructure
                newOptionUI.Label.text = "Skill Feat";
                newOptionUI.Filter.TraitWhitelist.Add(Data.Trait.Skill);
                newOptionUI.Filter.MaxLevel = i;
                newOptionUI.CreatorContext = this;
            }
            if (i % 5 == 0)
            {
                CharacterOptionUI newOptionUI = Instantiate(CharacterOptionUIPrefab, FeatsList);
                newOptionUI.Field = null; // Can't use FieldInfo here, as these need to be stored in a list or other datastructure
                newOptionUI.Label.text = "Ancestry Feat";
                newOptionUI.Filter.MaxLevel = i;
                //newOptionUI.Filter.TraitWhitelist.Add(Data.Trait.Ancestry);
                newOptionUI.Filter.TraitWhitelist.AddRange(characterData.Ancestry.Traits);
                newOptionUI.CreatorContext = this;
            }
        }

        // Generate Feature Visualizations
        List<CharacterOption> Features = new();
        Features.AddRange(characterData.Class.GrantedFeatures);
        Features.AddRange(characterData.Ancestry.GrantedFeatures);
        Features.AddRange(characterData.Heritage.GrantedFeatures);
        Features.AddRange(characterData.Deity.GrantedFeatures);
        for (int i = 0; i < characterData.Level; i++)
        {
            Features.AddRange(characterData.Class.ClassFeatures[i].Features);
        }
        Features.AddRange(characterData.Class.GrantedFeatures);
        foreach (CharacterOption feature in Features)
        {
            CharacterOptionUI newOptionUI = Instantiate(CharacterOptionUIPrefab, FeaturesList);
            //print(newOptionUI.name);
            newOptionUI.Field = null;
            //newOptionUI.Filter.TraitWhitelist.Add(Data.Trait.Ancestry);
            newOptionUI.Label.text = feature.name;
            newOptionUI.Icon.sprite = feature.Icon;
            newOptionUI.CreatorContext = this;
        }
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
