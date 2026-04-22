using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipWidget : MonoBehaviour
{
    public static TooltipWidget Instance;

    private TooltipInfo _currentInfo;
    public TooltipInfo CurrentInfo { get => _currentInfo; }

    [SerializeField]
    private TMP_Text Header;
    [SerializeField]
    private TMP_Text Body;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetInfo(TooltipInfo info = null, string body = "")
    {
        SetInfo(info, "", body);
    }

    public void SetInfo(TooltipInfo info, string header, string body)
    {
        _currentInfo = info;
        Header.text = header;
        Body.text = body;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
