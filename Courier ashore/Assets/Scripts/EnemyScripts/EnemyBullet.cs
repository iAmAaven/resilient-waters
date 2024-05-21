using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 5f;
    public Vector2 shootDirection;
    public AudioClip[] bulletSounds;
    private AudioSource oneShotAudio;
    private Rigidbody2D rb;

    void Start()
    {
        oneShotAudio = GameObject.FindWithTag("OneShotAudio").GetComponent<AudioSource>();
        oneShotAudio.PlayOneShot(bulletSounds[Random.Range(0, bulletSounds.Length)]);
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        rb.velocity = shootDirection * bulletSpeed;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == "Player")
        {
            if (otherObj.GetComponent<BoatHP>() == true)
            {
                otherObj.GetComponent<BoatHP>().BoatTakeDamage(1);
            }
            else
            {
                otherObj.GetComponentInParent<BoatHP>().BoatTakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
