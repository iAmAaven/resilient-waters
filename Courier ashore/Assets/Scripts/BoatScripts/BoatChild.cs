using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatChild : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer gfx;
    public BoatHP parentBoat;
    public BoatMovement boatMovement;
    void OnEnable()
    {
        boatMovement.boatAnim = animator;
        parentBoat.graphics = gfx;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time >= parentBoat.hitTimer && collision.gameObject.tag == "Object")
        {
            if (parentBoat.rb.velocity.magnitude >= parentBoat.dangerousBoatSpeed)
            {
                parentBoat.BoatTakeDamage(1);
            }
        }
    }
}
