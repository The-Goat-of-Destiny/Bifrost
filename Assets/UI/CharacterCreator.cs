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
        SelectedCharacter = characterData;
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
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
