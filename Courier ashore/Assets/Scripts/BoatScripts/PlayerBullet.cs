using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 5f;
    private Rigidbody2D rb;

    public Vector2 shootDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            otherObj.GetComponent<EnemyHP>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
