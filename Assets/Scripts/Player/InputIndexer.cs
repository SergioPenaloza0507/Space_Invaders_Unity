using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class InputIndexer : MonoBehaviour
{
    public event Action onSingleTap; //Event to handle single tap action
    public event Action onTripleTap; //Event to handle triple tap action
    
    public event Action onSwipe; //Event to handle swipe action

    public delegate void OnAccelerometerMovement(float value);

    public event OnAccelerometerMovement onAccelerometerMovement; //Event to handle accelerometer motion actions
    
    [Header("Accelerometer Input")]
    [SerializeField][Range(1f,100f)] private float attenuation; //Accelerometer smoothing attenuation
    private float inter; //Accelerometer positioning representing value

    [Header("Swipe input")] [SerializeField]
    private float tapThreshold; //How long is a swipe in order to not be considered a tap anymore

    //Swipe positions
    Vector2 pos1 = Vector2.zero;
    Vector2 pos2 = Vector2.zero;
    
    
    void Update()
    {
        HandleInput();   
    }

    void HandleInput()
    {
        HandleAccel();
        HandleTouch();
    }

    /// <summary>
    /// Touch handling function containing Single Tap, Triple Tap and Swipe
    /// </summary>
    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            Touch[] ts = Input.touches;

            
            if (ts.Length == 1)
            {
                switch (t.phase)
                {
                    case TouchPhase.Began:
                        pos1 = t.position;
                        pos2 = t.position;
                        break;
                    case TouchPhase.Moved:
                        pos2 = t.position;
                        break;
                    case TouchPhase.Ended:
                        print((pos2-pos1).magnitude);
                        if ((pos2-pos1).magnitude >= tapThreshold )
                        {
                            if ((pos2 - pos1).y > (pos2 - pos1).x)
                            {
                                onSwipe?.Invoke();
                            }
                        }
                        else
                        {
                            onSingleTap?.Invoke();
                        }
                        break;
                }
            }
            else if (ts.Length >= 3)
            {
                if (t.phase == TouchPhase.Ended)
                {
                    onTripleTap?.Invoke();
                }
            }
        }
    }
    
    /// <summary>
    /// Accelerometer motion Function
    /// </summary>
    void HandleAccel()
    {
        inter += Input.acceleration.x / attenuation;

        inter = Mathf.Clamp(inter, 0f, 1f);
        
        onAccelerometerMovement?.Invoke(inter);
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        float z = Camera.main.transform.position.y;
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(pos1.x,pos1.y,z)),0.5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(pos2.x,pos2.y,z)),0.5f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(pos1.x,pos1.y,z)),Camera.main.ScreenToWorldPoint(new Vector3(pos2.x,pos2.y,z)));
    }
    #endif
}
