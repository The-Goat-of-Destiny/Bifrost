using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Header;
    public string Body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetInfo(string header, string body="")
    {
        Header = header;
        Body = body;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Display tooltop at mouse position using info data
        TooltipWidget.Instance.SetInfo(this, Header, Body);
        TooltipWidget.Instance.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide tooltip
        if (TooltipWidget.Instance.CurrentInfo == this) TooltipWidget.Instance.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
