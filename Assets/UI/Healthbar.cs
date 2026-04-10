using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    private Token Representing;
    public Image Bar;
    public TMP_Text Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Set fill amount according to percentage of health remaining
        if (Bar) Bar.fillAmount = (float)Representing.Character.Health / (float)Representing.Character.MaxHealth;
        if (Text) Text.text = Representing.Character.Health.ToString() + "/" + Representing.Character.MaxHealth.ToString();
    }
}
