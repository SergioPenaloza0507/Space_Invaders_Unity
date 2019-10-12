using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions
{
    public enum direction
    {
        Forward =0,
        Back = 1,
        Up = 2,
        Down = 3,
        Right = 4,
        Left = 5,
    }

    public static Vector3 DirectionalVector(this Transform binder, Vector3 dir)
    {
        Vector3 norm = dir.normalized;
        return binder.position + binder.InverseTransformDirection(dir);
    }
    
    public static Vector3 Average(this Vector3 binder, params Vector3[] vector3s)
    {
        float x = 0;
        float y = 0;
        float z = 0;

        foreach (Vector3 vec in vector3s)
        {
            x += vec.x;
            y += vec.y;
            z += vec.z;
        }

        x /= vector3s.Length;
        y /= vector3s.Length;
        z /= vector3s.Length;
        
        return new Vector3(x,y,z);
    }
}
