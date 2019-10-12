using System.Collections;
using System.Collections.Generic;
using ObjectPools;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour,IPoolObject,IDamager
{
    private Rigidbody rb;

    [SerializeField] Transform StartReference;
    
    [SerializeField]private Vector3 initialPos;

    [SerializeField] private float forceMul,time;

    [SerializeField] private float damage = 1;
    bool active;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Deactivate()
    {
        transform.position = initialPos;
        rb.isKinematic = true;
        active = false;
        GetComponent<TrailRenderer>().enabled = false;
    }

    public void Activate()
    {
        transform.position = StartReference.position;
        active = true;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * forceMul,ForceMode.Impulse);
        StartCoroutine(DestroyOnTime());
        GetComponent<TrailRenderer>().enabled = true;
    }

    public void Activate(object parameter)
    {
        
    }

    public bool Active => active;
    public Vector3 StartPos { get => initialPos;
        set => initialPos = value;
    }

    IEnumerator DestroyOnTime()
    {
        print("starting process...");
        yield return new WaitForSeconds(time);
        Deactivate();
        print("not active");
    }

    public float Damage => damage;
}
