using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVulnerable
{
    void BeDamaged(IDamager dmgInfo);
    float Health { get; }
    void Die();
}
