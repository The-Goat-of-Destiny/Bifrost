using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using CustomAttributes;

public class Statsheet : MonoBehaviour
{
    [SerializeField] private StatUI StatPrefab;

    public CharacterData Data;

    private List<StatUI> Stats = new();

    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    public void Inspect(CharacterData data)
    {
        Data = data;
        Refresh();
    }

    public void Refresh()
    {
        foreach (StatUI stat in Stats)
        {
            Destroy(stat.gameObject);
        }
        Stats = new();
        if (Data)
        {
            NewStat("Name : " + Data.name);
            foreach (System.Reflection.PropertyInfo property in Data.GetType().GetProperties())
            {
                // Check if property has custom attribute
                if (property.GetCustomAttributes(typeof(ExposedPropertyAttribute), true).Count() != 0)
                    NewStat(property.Name + " : " + property.GetValue(Data).ToString());
            }
            foreach (System.Reflection.FieldInfo field in Data.GetType().GetFields())
            {
                // Check if field has custom attribute
                if (field.GetCustomAttributes(typeof(ExposedFieldAttribute), true).Count() != 0)
                    if (field.GetValue(Data) == null)
                    {
                        NewStat(field.Name + " : ---");
                    }
                    else
                    {
                        if (field.FieldType == typeof(Composite))
                        {
                            NewStat(field.Name + " : " + ((Composite)field.GetValue(Data)).Squash().ToString());
                        }
                        else
                        {
                            NewStat(field.Name + " : " + field.GetValue(Data).ToString());
                        }
                    }
            }
        }
    }

    private void NewStat(string text = "")
    {
        StatUI stat = Instantiate(StatPrefab, transform.GetChild(0));
        stat.Text.text = text;
        Stats.Add(stat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
