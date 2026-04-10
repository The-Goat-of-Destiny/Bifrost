using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectWidget : MonoBehaviour
{
    public EffectInstance Effect;
    [SerializeField]
    private Image Icon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LinkTo(EffectInstance effect)
    {
        Effect = effect;
        Icon.sprite = Effect.Context.Icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
