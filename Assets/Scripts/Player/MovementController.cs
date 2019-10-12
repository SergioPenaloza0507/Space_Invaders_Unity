using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputIndexer))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private Transform from, to;
    
    public void SetPosition(float t)
    {
        transform.position = Vector3.Lerp(@from.position, to.position, t);
    }

    private void Awake()
    {
        GetComponent<InputIndexer>().onAccelerometerMovement += SetPosition;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(@from.position, 1f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(@to.position, 1f);
        }
        catch (Exception error)
        {
            
        }
        
        Gizmos.color = Color.blue;
        float z = Camera.main.transform.position.y;
        Rect r = Camera.main.pixelRect;
        Vector3 bl = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, z));
        Vector3 br = Camera.main.ScreenToWorldPoint(new Vector3(r.width, 0, z));
        Vector3 tl = Camera.main.ScreenToWorldPoint(new Vector3(0, r.height, z));
        Vector3 tr = Camera.main.ScreenToWorldPoint(new Vector3(r.width, r.height, z));
        Gizmos.DrawLine(bl,br);
        Gizmos.DrawLine(bl,tl);
        Gizmos.DrawLine(tl,tr);
        Gizmos.DrawLine(tr,br);
    }
#endif
}
