using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private float sensitivity = 0.018f;
    [SerializeField] private float scrollSensitivity = 0.1f;
    [SerializeField] private float minZoom = 0.5f;
    [SerializeField] private float maxZoom = 10f;
    private Vector3 lastMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(2))
        {
            // May require adjustment based on zoom level
            transform.Translate(Camera.orthographicSize * sensitivity * -(Input.mousePosition - lastMousePosition));
        }
        lastMousePosition = Input.mousePosition;
        Camera.orthographicSize -= Input.mouseScrollDelta.y * scrollSensitivity;
        Camera.orthographicSize = Mathf.Clamp(Camera.orthographicSize, minZoom, maxZoom);
    }
}
