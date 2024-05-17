using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 5f;
    private Rigidbody2D rb;

    public Vector2 shootDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        rb.velocity = shootDirection * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == "Player")
        {
            otherObj.GetComponent<BoatHP>().BoatTakeDamage(1);
            Destroy(gameObject);
        }
    }
}
