using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RequireHover : MonoBehaviour, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
