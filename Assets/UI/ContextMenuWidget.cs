using CustomAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContextMenuWidget : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private ContextMenuButton ContextButtonPrefab;
    [SerializeField] private Transform ButtonContainer;

    public object Data;

    private List<ContextMenuButton> ContextButtons = new();

    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    public void OpenContextMenu(object data)
    {
        Data = data;
        Refresh();
    }

    public void CloseContextMenu()
    {
        Data = null;
        Refresh();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CloseContextMenu();
    }

    public void Refresh()
    {
        // Destroying the child-elements causes an error in the Content Size Fitter.
        // This is not currently avoidable, so the content size fitter has been disabled until a solution can be found.
        // Turns out the rect hitbox expands to fit child rects automatically, so unless a background is required the code works as intended.
        foreach (ContextMenuButton contextButton in ContextButtons)
        {
            Destroy(contextButton.gameObject);
        }
        ContextButtons = new();
        gameObject.SetActive(Data != null);
        if (Data != null)
        {
            foreach (MethodInfo method in Data.GetType().GetMethods())
            {
                if (method.GetCustomAttributes(typeof(ContextMenu), true).Count() != 0)
                    NewOption(Data, method, method.Name);
            }
        }
        transform.position = Input.mousePosition - Vector3.right + Vector3.up;
    }

    private void NewOption(object obj, MethodInfo method, string text = "")
    {
        ContextMenuButton button = Instantiate(ContextButtonPrefab, ButtonContainer);
        button.transform.GetChild(0).GetComponent<TMP_Text>().text = text;
        button.methodInfo = method;
        button.obj = obj;
        button.parameters = method.GetParameters();
        ContextButtons.Add(button);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
