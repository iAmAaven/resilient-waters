using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHP : MonoBehaviour
{
    public int boatHitPoints;
    public int boatMaxHitPoints;
    public float dangerousBoatSpeed;
    public float damageCooldown;

    private SpriteRenderer graphics;
    private HealthUI healthUI;
    private bool isAughFrames = false;
    private Rigidbody2D rb;
    private float timer;
    private BoatMovement boatMovement;

    void Start()
    {
        graphics = GetComponentInChildren<SpriteRenderer>();
        dangerousBoatSpeed = PlayerPrefs.GetFloat("BoatSpeed") - 2f;
        if (dangerousBoatSpeed < 4f)
        {
            dangerousBoatSpeed = 4f;
        }
        boatMovement = GetComponent<BoatMovement>();
        boatHitPoints = PlayerPrefs.GetInt("BoatHP");
        boatMaxHitPoints = PlayerPrefs.GetInt("BoatDurabilityLevel") * 10;
        if (boatHitPoints <= 0)
        {
            boatHitPoints = boatMaxHitPoints;
        }

        healthUI = FindObjectOfType<HealthUI>();
        healthUI.RefreshHealth(boatHitPoints, boatMaxHitPoints);
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
        if (boatMovement.playerPassedOut == false)
        {
            boatHitPoints -= damage;
            timer = Time.time + damageCooldown;
            Debug.Log("Boat took damage, HP: " + boatHitPoints);
            if (isAughFrames == false)
            {
                StartCoroutine(AughFrames());
            }
            healthUI.RefreshHealth(boatHitPoints, boatMaxHitPoints);
            PlayerPrefs.SetInt("BoatHP", boatHitPoints);

            if (boatHitPoints <= 0)
            {
                // TODO: Trigger a mini game where the player has to patch holes in the boat
                Debug.Log("Boat broke");
                boatHitPoints = 0;
                healthUI.RefreshHealth(boatHitPoints, boatMaxHitPoints);
                PlayerPrefs.SetInt("BoatHP", boatHitPoints);
            }
        }
    }


    IEnumerator AughFrames()
    {

        isAughFrames = true;
        for (int i = 0; i < 5; i++)
        {
            graphics.color = new Color32(255, 255, 255, 255);
            yield return new WaitForSeconds(0.1f);
            // Player becomes invisible (and invinsible XD) for a second
            graphics.color = new Color32(255, 255, 255, 0);
            yield return new WaitForSeconds(0.1f);
        }

        graphics.color = new Color32(255, 255, 255, 255);
        isAughFrames = false;
    }
}
