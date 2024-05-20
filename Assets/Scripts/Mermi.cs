using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody rb;
    public int damage = 10;
    private float destroyMermiAfter = 5f;
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

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Zemin"))
        {
            StartCoroutine(DestroyBulletAfterDelay());
        }
    }

    IEnumerator DestroyBulletAfterDelay()
    {
        yield return new WaitForSeconds(destroyMermiAfter);
        Destroy(gameObject);
    }
}
