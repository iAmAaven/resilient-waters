using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int enemyHealth;
    public SpriteRenderer graphics;
    public AudioClip[] damageAudios;
    private AudioSource oneShotAudio;
    private bool isAughFrames = false;
    private ShadowTidesBoat shadowTidesBoat;
    void Start()
    {
        oneShotAudio = GameObject.FindWithTag("OneShotAudio").GetComponent<AudioSource>();
        shadowTidesBoat = GetComponent<ShadowTidesBoat>();
        enemyHealth *= PlayerPrefs.GetInt("WaterGunLevel", 1);
    }
    public void TakeDamage()
    {
        enemyHealth--;
        shadowTidesBoat.playerDetectRadius = shadowTidesBoat.startingRadius * 2;
        oneShotAudio.PlayOneShot(damageAudios[Random.Range(0, damageAudios.Length)]);

        if (isAughFrames == false)
        {
            StartCoroutine(AughFrames());
        }

        if (enemyHealth <= 0)
        {
            GetComponent<ShadowTidesBoat>().EnemyDeath();
        }
    }

    IEnumerator AughFrames()
    {
        isAughFrames = true;
        for (int i = 0; i < 5; i++)
        {
            graphics.color = new Color32(255, 255, 255, 255);
            yield return new WaitForSeconds(0.1f);
            graphics.color = new Color32(255, 255, 255, 0);
            yield return new WaitForSeconds(0.1f);
        }

        graphics.color = new Color32(255, 255, 255, 255);
        isAughFrames = false;
    }
}
