using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPools;
using UnityEngine;

[RequireComponent(typeof(InputIndexer))]
public class ShootController : MonoBehaviour
{
    [SerializeField] private float coolDownBurst, coolDownPiercing;
    
    float burstTimer,piercingTimer;

    public float BurstTimer
    {
        get => burstTimer;
    }

    public float PiercingTimer
    {
        get => piercingTimer;
    }

    private void Awake()
    {
        InputIndexer ii = GetComponent<InputIndexer>();
        ii.onSwipe += Piercing;
        ii.onSingleTap += SingleShot;
        ii.onTripleTap += Burst;
    }

    private void Update()
    {
        burstTimer -= Time.deltaTime;
        burstTimer = Mathf.Clamp(burstTimer, 0, coolDownBurst);
        piercingTimer -= Time.deltaTime;
        piercingTimer = Mathf.Clamp(piercingTimer, 0, coolDownPiercing);
    }

    void SingleShot()
    {

        print("1 Bullet");
        Shoot(0);

    }

    void Burst()
    {
        if (burstTimer <= 0)
        {
            print("Burst");
            StartCoroutine(BurstThroughTime(0.1f,3f));
            burstTimer = coolDownBurst;
        }
    }

    void Piercing()
    {
        if (piercingTimer <= 0)
        {
            print("piercing");
            Shoot(1);
            piercingTimer = coolDownPiercing;
        }
    }

    void Shoot(int index)
    {
        ((Pool)GetComponentInChildren(typeof(Pool))).GetActiveGameObject(index,null);
    }

    

    IEnumerator BurstThroughTime(float delay,float time)
    {
        int iterations = Mathf.RoundToInt( time / delay);
        for (int i = 0; i < iterations; i++)
        {
            SingleShot();
            yield return new WaitForSeconds(delay);
        }
    }

    public float PierceTime => 1f - (piercingTimer / coolDownPiercing);
    public float BurstTime =>1f - ( burstTimer / coolDownBurst);
}
