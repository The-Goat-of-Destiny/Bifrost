using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    public bool Snapping = true;
    public MapGrid Grid;
    Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Snap(MapGrid grid)
    {
        if (grid)
        {
            float x = transform.position.x;
            float z = transform.position.z;
            if (grid.Rows % 2 == 1) 
                x = Mathf.Round(x - 0.5f) + 0.5f;
            else
                x = Mathf.Round(x);
            if (grid.Columns % 2 == 1)
                z = Mathf.Round(z - 0.5f) + 0.5f;
            else
                z = Mathf.Round(z);
            transform.position = new(x, Mathf.Round(transform.position.y), z);
        }
        else transform.position = new(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        lastPosition = transform.position;
    }

    public void Snap()
    {
        Snap(Grid);
    }

    // Update is called once per frame
    void Update()
    {
        if (Snapping)
        {
            if (transform.position != lastPosition) Snap();
        }
    }
}
