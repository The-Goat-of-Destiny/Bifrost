using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectWidget : MonoBehaviour
{
    public EffectInstance Effect;
    [SerializeField]
    private Image Icon;
    [SerializeField]
    private TMP_Text Name;
    [SerializeField]
    private TMP_Text Quantity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LinkTo(EffectInstance effect)
    {
        Effect = effect;
        Icon.sprite = Effect.Context.Icon;
        Name.text = effect.Context.name;
        Quantity.text = effect.Value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
