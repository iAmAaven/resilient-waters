using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHP : MonoBehaviour
{
    public int boatHitPoints;
    public int boatMaxHitPoints;
    public float dangerousBoatSpeed;
    public float damageCooldown;
    public bool isFinale = false;

    public AudioClip damageSFX, destroyedSFX;
    public SpriteRenderer graphics;

    private AudioSource oneShotAudio;
    private HealthUI healthUI;
    private bool isAughFrames = false;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public float hitTimer;
    private BoatMovement boatMovement;

    void Start()
    {
        oneShotAudio = GameObject.FindWithTag("OneShotAudio").GetComponent<AudioSource>();
        healthUI = FindObjectOfType<HealthUI>();
        dangerousBoatSpeed = PlayerPrefs.GetFloat("BoatSpeed") - 2f;
        if (dangerousBoatSpeed < 4f)
        {
            dangerousBoatSpeed = 4f;
        }
        boatMovement = GetComponent<BoatMovement>();
        boatMaxHitPoints = PlayerPrefs.GetInt("BoatDurabilityLevel", 1) * 10;
        boatHitPoints = PlayerPrefs.GetInt("BoatHP", 10);

        if (isFinale == true)
        {
            boatHitPoints = boatMaxHitPoints;
            PlayerPrefs.SetInt("BoatHP", boatHitPoints);
        }

        healthUI.RefreshHealth();
        rb = GetComponent<Rigidbody2D>();
    }

    public void BoatTakeDamage(int damage)
    {
        if (boatMovement.playerPassedOut == false)
        {
            boatHitPoints -= damage;
            hitTimer = Time.time + damageCooldown;
            Debug.Log("Boat took damage, HP: " + boatHitPoints);
            if (isAughFrames == false)
            {
                StartCoroutine(AughFrames());
            }
            PlayerPrefs.SetInt("BoatHP", boatHitPoints);
            healthUI.RefreshHealth();

            if (boatHitPoints <= 0)
            {
                Debug.Log("Boat broke");
                boatHitPoints = 0;
                PlayerPrefs.SetInt("BoatHP", boatHitPoints);
                healthUI.RefreshHealth();
                FindObjectOfType<PassOut>().BoatDestroyed();
                oneShotAudio.PlayOneShot(destroyedSFX);
                return;
            }
            oneShotAudio.PlayOneShot(damageSFX);
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
