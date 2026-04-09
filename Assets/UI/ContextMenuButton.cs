using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContextMenuButton : MonoBehaviour
{
    public TMP_Text Text;
    public System.Reflection.MethodInfo methodInfo;
    public object obj;
    public object[] parameters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Click()
    {
        transform.parent.parent.GetComponent<ContextMenuWidget>().CloseContextMenu();
        methodInfo.Invoke(obj, parameters);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
