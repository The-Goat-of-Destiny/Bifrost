using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    public int Rows = 10;
    public int Columns = 10;
    public List<List<object>> Contents;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Gizmos.DrawWireCube(new Vector3(i - (float)Rows / 2f, 0, j - (float)Columns / 2f) + transform.position, Vector3.one);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
