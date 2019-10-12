using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace ObjectPools
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] GameObject[] prefabs;

        [SerializeField] int maxCount;

        List<List<GameObject>> objects;

        bool ready = false;

        public int Length => prefabs.Length;
        private void Awake()
        {
            StartCoroutine(InitializeObjects());
        }


        IEnumerator InitializeObjects()
        {
            objects = new List<List<GameObject>>();
            GameObject[] clones = new GameObject[prefabs.Length];

            for (int i = 0; i < clones.Length; i++)
            {
                objects.Add(new List<GameObject>());
                GameObject clone = Instantiate(prefabs[i], new Vector3(100,0,100),prefabs[i].transform.rotation);
                clones[i] = clone;
                clone.GetComponent<IPoolObject>().Deactivate();
                objects[i].Add(clone);
                for (int j = 0; j < maxCount - 1; j++)
                {
                    objects[i].Add(Instantiate(clones[i], new Vector3(100,0,100), clones[i].transform.rotation));
                    yield return null;
                    objects[i][j].GetComponent<IPoolObject>().Deactivate();
                }
            }

            ready = true;
            yield return null;
        }

        public GameObject GetActiveGameObject(int index,object parameter)
        {
            if (ready)
            {
                try
                {
                    GameObject c = objects[index].First(x => !x.GetComponent<IPoolObject>().Active);
                    IPoolObject p = c.GetComponent<IPoolObject>();
                    if (parameter != null)
                    {
                        p.Activate(parameter);
                    }
                    else
                    {
                        p.Activate();
                    }
                    return c;
                }
                catch (Exception error)
                {
                    GameObject c = Instantiate(prefabs[index], new Vector3(100, 0, 100), prefabs[index].transform.rotation);
                    
                    c.GetComponent<IPoolObject>().Deactivate();
                    if (parameter != null)
                    {
                        c.GetComponent<IPoolObject>().Activate(parameter);
                    }
                    else
                    {
                        c.GetComponent<IPoolObject>().Activate();
                    }

                    return c;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
