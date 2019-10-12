using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHitBullet : Bullet,IVulnerable
{
    private float health = 1;

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
    }
}
