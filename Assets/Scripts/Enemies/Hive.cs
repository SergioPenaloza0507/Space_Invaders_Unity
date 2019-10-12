using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game_Management;
using UnityEngine;

namespace Enemies
{
    public class Hive : MonoBehaviour
    {
        private static Hive instance;
        [SerializeField]private List<Squad> _squads;

        int squadLength;
        [SerializeField]private int moveCount;
    
        bool flipped;
    
        bool toflip;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }
        }
    
        // Start is called before the first frame update
        void Start()
        {
            _squads = new List<Squad>();
            StartCoroutine(SpawnPeriodically());
            squadLength = GetComponent<Squad>().Enemies.Count;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                StartCoroutine(MoveCompany(0.1f));   
            }
        }

        public void Push(Squad squad)
        {
            _squads.Add(squad);
            print("Added");
        }

        IEnumerator SpawnPeriodically()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);
                StartCoroutine(MoveCompany(0.1f));
            }
        }
        IEnumerator MoveCompany(float delay)
        {
            foreach (Squad s in GetComponents<Squad>().Reverse())
            {
                yield return new WaitForSeconds(delay);
                int[] dir = new int[0];
                if (moveCount < GameManager.Instance.Grid.Centers.GetLength(1) - squadLength)
                {
                    if (!flipped)
                    {
                        dir = new[] {1, 0};
                    }
                    else
                    {
                        dir = new[] {-1, 0};
                    }
                }
                else
                {
                    dir = new[] {0, 1};

                
                    toflip = true;
                }
                s.PropagateWrp(dir,flipped);
            }

            if (toflip)
            {
                flipped = !flipped;
                toflip = false;
                moveCount = 0;
            }
            else
            {
                moveCount++;
            }

        }

        public static Hive Instance => instance;
    }
}
