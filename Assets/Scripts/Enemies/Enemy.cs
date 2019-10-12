using System;
using System.Collections;
using System.Collections.Generic;
using Game_Management;
using ObjectPools;
using UnityEngine;


public class Enemy : MonoBehaviour, IDamager, IVulnerable, IPoolObject
{
    public delegate void OnActiveDlg(int[] coordinates);
    public event OnActiveDlg onActive;
    public event Action onActiveSingle;
    public event Action onInActiveSingle;
    public event Action onDead;
    
    [SerializeField] private float damage = 1, health = 1,score = 100;

    private bool active;

    private static Vector3 startPos;

    private Vector3 spawnposition;
    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(100, 0, 100);
    }
    
    // Update is called once per frame
    void Update()
    {

        GetComponent<MeshRenderer>().enabled = active;
        GetComponent<Collider>().enabled = active;
    }


    public float Damage => damage;

    public void BeDamaged(IDamager dmgInfo)
    {
        health -= dmgInfo.Damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public float Health => health;
    
    public void Die()
    {
        Deactivate();
        onDead?.Invoke();
        ScoreManager.Instance.Score(score);
    }

    public void Deactivate()
    { 
        print("damaged");
        transform.position = StartPos;
        active = false;
        onInActiveSingle?.Invoke();
    }

    public void Activate()
    {
        
    }

    public void Activate(object parameter)
    {
        onActive?.Invoke((int[]) parameter);
        transform.position = GameManager.Instance.Grid.VectorFromPosition(((int[])parameter)[0],((int[])parameter)[1]);
        onActiveSingle?.Invoke();
        active = true;
    }
    
    
    public virtual void OnTriggerEnter(Collider other)
    {
        print(other.name);
        IDamager dmg = other.GetComponent<IDamager>();
        if (dmg != null)
        {
            BeDamaged(dmg);
        }

        IVulnerable v = other.GetComponent<IVulnerable>();

        if (v != null)
        {
            v.BeDamaged(this);
        }
    }

    public bool Active => active;
    public Vector3 StartPos { get => startPos; set => startPos = value; }
}
