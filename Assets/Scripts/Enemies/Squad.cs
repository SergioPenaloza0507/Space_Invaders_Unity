using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Squad : MonoBehaviour
    {
        [SerializeField]private List<EnemyMovement> _enemies;
        public event Action OnWiped;
    
    
        public List<EnemyMovement> Enemies => _enemies;

        [SerializeField]private float pacing = 0.1f;

        private void Start()
        {
            foreach (EnemyMovement en in _enemies)
            {
                en.GetComponent<Enemy>().onDead += () => Remove(en);
            }
        }
    
        public void PropagateWrp(int[] propagation,bool flipped)
        {
            StartCoroutine(Propagate(propagation,flipped));
        }
    
        IEnumerator Propagate(int[] direction,bool flipped)
        {
            if(!flipped)
            {
                for (int i = _enemies.Count - 1; i >= 0; i--)
                {
                    yield return new WaitForSeconds(pacing);
                    try
                    {
                        _enemies[i].MoveWrp(direction[0], direction[1], 5f);
                    }
                    catch (Exception error)
                    {
                    
                    }
                }
            }
            else
            {
                for (int i = 0; i <_enemies.Count; i++)
                {
                    yield return new WaitForSeconds(pacing);
                    try
                    {
                        _enemies[i].MoveWrp(direction[0], direction[1], 5f);
                    }
                    catch (Exception error)
                    {
                    
                    }
                }
            }
        }

        public void Remove(EnemyMovement en)
        {
            _enemies.Remove(en);
            if (_enemies.Count <= 0)
            {
                OnWiped?.Invoke();
                Destroy(this);
            }
        }

        public void SetEnemies(EnemyMovement[] movements)
        {
            _enemies = new List<EnemyMovement>();
            _enemies.AddRange(movements);
        }
    }
}
