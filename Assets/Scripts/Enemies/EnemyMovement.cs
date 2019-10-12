using System;
using System.Collections;
using Game_Management;
using UnityEngine;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        public delegate void Propagation(int[] propagationVector);
        public event Propagation OnPropagate;

        [SerializeField]private int[] position;

        [SerializeField]bool flip;

        public bool OnEdge => position[0] >= GameManager.Instance.Grid.Centers.GetLength(1) - 1;
        public bool OnStarting => position[0] <= 0;
        private void Start()
        {
        
        }

        private void Awake()
        {
            GetComponent<Enemy>().onActive += SetStartPos;
            GetComponent<Enemy>().onActiveSingle += () => this.enabled = true;
            GetComponent<Enemy>().onInActiveSingle += () => this.enabled = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PrintList(OnPropagate);
            }
        }

        void SetStartPos(int[] pos)
        {
            position = new int[2];
            position = pos;
        }

        public void MoveWrp(int x, int y, float speed)
        {
            StartCoroutine(Move(x, y, speed));
        }
    

        public void SetPosition(int x, int y)
        {
            position = new[] {position[0] + x,position[1] + y};
            transform.position = GameManager.Instance.Grid.VectorFromPosition(position[0] + x,position[1] + y);
        }
    
        IEnumerator Move(int xOff, int yOff,float speed)
        {
            position[0] += xOff;
            position[1] += yOff;

            Vector3 destination = GameManager.Instance.Grid.VectorFromPosition(position[0], position[1]);
            float mag = Vector3.Distance(transform.position, destination);
            int iterations = Mathf.RoundToInt( Vector3.Distance(transform.position, destination) / (Time.deltaTime * speed));

            float t = 0;
            float increment = mag / (float) iterations;
            Vector3 startPos = transform.position;
            for (int i = 0; i < iterations; i++)
            {
                t += increment;
                transform.position = Vector3.Lerp(startPos, destination, t);
                yield return null;
            }

            yield return null;
        }

        public void PrintList(Delegate d)
        {
            print(name);
            foreach (Delegate dl in d.GetInvocationList())
            {
                print(dl.Method.ToString());
            }
        }
    }
}
