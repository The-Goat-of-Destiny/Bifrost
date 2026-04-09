using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Statsheet InspectWindow;
    public ContextMenuWidget ContextMenuWidget;

    public List<Token> SelectedTokens = new();
    public List<Token> TargettedTokens = new();

    private Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        Game.Manager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenContextMenu(object data)
    {
        ContextMenuWidget.OpenContextMenu(data);
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            ClickReceiver clickable = hit.collider.gameObject.GetComponent<ClickReceiver>();
            if (clickable)
            {
                clickable.Hover();
            }
        }
    }

    private void LateUpdate()
    {


        foreach (Token token in TargettedTokens)
        {
            token.TargetOverlay.gameObject.SetActive(true);
        }
    }
}
