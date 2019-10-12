using System.Collections.Generic;
using ObjectPools;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(EnemyGrid))]
    [RequireComponent(typeof(Pool))]
    public class Spawner : MonoBehaviour
    {
        private EnemyGrid _grid;
        private Pool pool;
    
        [SerializeField] int enemiesPerRow;

        [SerializeField] private int rows;

    
        private void Start()
        {
            _grid = GetComponent<EnemyGrid>();
            pool = GetComponent<Pool>();
            SpawnRows(true);
        }
        
    
        [SerializeField] private int ActiveRows;
        void SpawnRows(bool offseted)
        {
            int start = offseted ? 1 : 0;
            

            for (int i = 0; i < rows; i++)
            {
                Squad sq = (Squad)Hive.Instance.gameObject.AddComponent(typeof(Squad));
                Queue<EnemyMovement> en = new Queue<EnemyMovement>();
                int type = Mathf.RoundToInt(Random.Range(0, pool.Length));
                for (int j = 0; j < enemiesPerRow; j ++) 
                {
                    int[] aram = {j, i};
                
                    GameObject g = pool.GetActiveGameObject(type, aram);
                    en.Enqueue(g.GetComponent<EnemyMovement>());
                    sq.SetEnemies(en.ToArray());
                }
            }
        }
    }
}
