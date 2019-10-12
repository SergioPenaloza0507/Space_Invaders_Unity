using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[ExecuteInEditMode()]
public class EnemyGrid : MonoBehaviour
{

    [SerializeField] private int rows = 2, columns = 2;
    [SerializeField] private float separation = 1f;
    
    private Vector3[,] corners;
    private Vector3[,] centers;

    public Vector3[,] Centers => centers;
    public int Rows => rows;
    public int Columns => columns;

    private void OnValidate()
    {
        Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        corners = new Vector3[rows,columns];
        for (int i = 0; i < corners.GetLength(0); i++)
        {
            for (int j = 0; j < corners.GetLength(1); j++)
            {
                corners[i,j] = new Vector3(transform.position.x + separation * j,
                    transform.position.y,
                    transform.position.z - separation * i);
            }
        }
        
        centers = new Vector3[rows - 1, columns - 1];
        for (int i = 0; i < centers.GetLength(0); i++)
        {
            for (int j = 0; j < centers.GetLength(1); j++)
            {
                Vector3 centerpoint = Vector3.zero;
                
                centers[i, j] = centerpoint.Average(corners[i,j],corners[i + 1,j],corners[i,j+1],corners[i+1,j+1]);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 VectorFromPosition(int x, int y)
    {
        try
        {
            return centers[y, x];
        }
        catch (System.Exception error)
        {
            return centers[centers.GetLength(0) - 1, centers.GetLength(1) - 1];
        }
    }
    
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(corners[0,columns - 1],corners[rows-1,columns-1]);
        Gizmos.DrawLine(corners[rows-1,0],corners[rows-1,columns-1]);
        for (int i = 0; i < corners.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < corners.GetLength(1) - 1; j++)
            {
                Gizmos.DrawLine(corners[i,j], corners[i,j+1]);
                Gizmos.DrawLine(corners[i,j], corners[i+1,j]);
            }
        }
        
        Gizmos.color = new Color(1f, 0.6f, 0.65f);
        foreach (var center in centers)
        {
            Gizmos.DrawWireSphere(center,0.05f);
        }
        
    }
#endif
}
