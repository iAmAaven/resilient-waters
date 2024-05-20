using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate;
    public bool firstLevel = false, secondLevel = false, thirdLevel = false;
    public AudioClip[] bulletSounds;
    private AudioSource oneShotAudio;

    [Header("1st level gun")]
    public Transform levelOneShootPoint;

    [Header("2nd level gun")]
    public Transform[] levelTwoShootPoints;

    [Header("3rd level gun")]
    public Transform[] levelThreeShootPoints;
    private float timer = 0f;
    private BoatMovement boatMovement;
    private Boat boat;

    void Start()
    {
        oneShotAudio = GameObject.FindWithTag("OneShotAudio").GetComponent<AudioSource>();

        fireRate = (float)PlayerPrefs.GetInt("WaterGunLevel", 1);
        boatMovement = GetComponentInParent<BoatMovement>();
        boat = GetComponentInParent<Boat>();
    }
    void Update()
    {
        if (boat.isCarryingContraband == true
            && boatMovement.canPlayerMove == true
            && boatMovement.playerPassedOut == false
            && Time.time >= timer && Input.GetButton("Fire1"))
        {
            ShootWatergun();
            timer = Time.time + 1f / fireRate;
        }
    }
    void ShootWatergun()
    {
        oneShotAudio.PlayOneShot(bulletSounds[Random.Range(0, bulletSounds.Length)]);

        if (firstLevel)
        {
            GameObject newBullet = Instantiate(bulletPrefab, levelOneShootPoint.position, levelOneShootPoint.localRotation);
            Vector2 direction = levelOneShootPoint.up;
            newBullet.GetComponent<PlayerBullet>().shootDirection = direction;
        }
        else if (secondLevel)
        {
            foreach (Transform shootPoint in levelTwoShootPoints)
            {
                GameObject newBullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                Vector2 direction = shootPoint.up;
                newBullet.GetComponent<PlayerBullet>().shootDirection = direction;
            }
        }
        else if (thirdLevel)
        {
            foreach (Transform shootPoint in levelThreeShootPoints)
            {
                GameObject newBullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                Vector2 direction = shootPoint.up;
                newBullet.GetComponent<PlayerBullet>().shootDirection = direction;
            }
        }
    }
}
