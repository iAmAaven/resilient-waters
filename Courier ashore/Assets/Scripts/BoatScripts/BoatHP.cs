using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHP : MonoBehaviour
{
    public int boatHitPoints;
    public int boatMaxHitPoints;
    public float dangerousBoatSpeed;
    public float damageCooldown;

    private Rigidbody2D rb;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time >= timer && other.gameObject.tag == "Object")
        {
            if (rb.velocity.magnitude >= dangerousBoatSpeed)
            {
                BoatTakeDamage(1);
            }
        }
    }

    public void BoatTakeDamage(int damage)
    {
        boatHitPoints -= damage;
        timer = Time.time + damageCooldown;
        Debug.Log("Boat took damage, HP: " + boatHitPoints);

        if (boatHitPoints <= 0)
        {
            // TODO: Trigger a mini game where the player has to patch holes in the boat
            Debug.Log("Boat broke");
            boatHitPoints = 0;
        }
    }
}
