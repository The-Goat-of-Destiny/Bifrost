using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsList : MonoBehaviour
{
    public static EffectsList Instance;
    public EffectWidget EffectPrefab;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        if (Game.Manager.SelectedTokens.Count == 1)
        {
            foreach (EffectInstance effect in Game.Manager.SelectedTokens[0].Character.Effects)
            {
                InstantiateEffectWidget(effect);
            }
        }
    }

    private void InstantiateEffectWidget(EffectInstance effect)
    {
        EffectWidget effectWidget = Instantiate(EffectPrefab, transform);
        foreach (EffectInstance rider in effect.Context.Riders)
        {
            InstantiateEffectWidget(rider);
        }
        effectWidget.LinkTo(effect);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisuals();
    }
}
