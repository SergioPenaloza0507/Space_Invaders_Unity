using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ObjectPools
{
    public interface IPoolObject
    {
        void Deactivate();
        void Activate();
        void Activate(object parameter);
        
        bool Active { get; }

        Vector3 StartPos { get; set; }
    }
}