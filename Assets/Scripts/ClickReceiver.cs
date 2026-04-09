using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickReceiver : MonoBehaviour
{
    public UnityEvent OnHover;
    public UnityEvent OnStopHover;
    public UnityEvent OnClick;
    public UnityEvent OnRightClick;
    public UnityEvent OnMiddleClick;

    private bool hovered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void StopHover()
    {
        OnStopHover.Invoke();
    }

    public virtual void Hover()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RightClick();
        }
        else if (Input.GetMouseButtonDown(2))
        {
            MiddleClick();
        }
        else
        {
            OnHover.Invoke();
        }
        hovered = true;
    }

    public virtual void Click()
    {
        //print("Clicked on " + ToString());
        OnClick.Invoke();
    }

    public virtual void RightClick()
    {
        //print("Right Clicked on " + ToString());
        OnRightClick.Invoke();
    }

    public virtual void MiddleClick()
    {
        //print("Middle Clicked on " + ToString());
        OnMiddleClick.Invoke();
    }

    public void OpenContextMenu()
    {
        Game.Manager.OpenContextMenu(gameObject.GetComponent<Token>());
    }

    // Update is called once per frame
    void Update()
    {
        if (!hovered)
        {
            StopHover();
        }
        hovered = false;
    }
}
