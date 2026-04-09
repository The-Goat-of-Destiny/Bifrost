using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    public TMP_Text Sender;
    public TMP_Text Body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RefreshVisuals(string sender="", string body="")
    {
        Sender.text = sender;
        Body.text = body;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
