using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabManager : MonoBehaviour
{
    public Transform TabHolder;
    public Transform DefaultTab = null;

    // Start is called before the first frame update
    void Start()
    {
        SwitchTab(DefaultTab);
    }

    public void SwitchTab(Transform tab = null)
    {
        if (tab == null) Debug.LogWarning("Tab does not exist");
        foreach (Transform t in TabHolder)
        {
            t.gameObject.SetActive(t == tab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
