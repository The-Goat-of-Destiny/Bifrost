using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    public CharacterData Sender;
    public TMP_Text Title;
    public TMP_Text Header;
    public TMP_Text Body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RefreshVisuals(CharacterData sender, string header="", string body="")
    {
        Sender = sender;
        if (Sender != null) Title.text = sender.name;
        Header.text = header;
        Header.gameObject.SetActive(header != "");
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
