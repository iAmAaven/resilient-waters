using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 15f;
    private Rigidbody2D rb;

    public Vector2 shootDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        rb.velocity = shootDirection * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == "Enemy")
        {
            otherObj.GetComponentInParent<EnemyHP>().TakeDamage();
            Destroy(gameObject);
        }
        else if (otherObj.tag == "Object")
        {
            Destroy(gameObject);
        }
    }
}
