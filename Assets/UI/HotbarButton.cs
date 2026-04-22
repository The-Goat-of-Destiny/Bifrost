using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HotbarButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Label;
    private UnityEvent<Token> Function;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetFunction(UnityEvent<Token> function, string label)
    {
        Function = function;
        Label.text = label;
    }

    public void Run()
    {
        Function.Invoke(Game.Manager.SelectedTokens[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
