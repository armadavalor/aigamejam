using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody rb;
    public int damage = 10;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        HayvanatDeneme1 enemy = hitInfo.GetComponent<HayvanatDeneme1>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            new WaitForSeconds(5f)
        }
    }
}
