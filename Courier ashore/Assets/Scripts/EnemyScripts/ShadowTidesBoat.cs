using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShadowTidesBoat : MonoBehaviour
{
    [Header("Movement & AI")]
    public float rotationSpeed;
    public float playerDetectRadius;
    public NavMeshAgent navMeshAgent;

    [Header("Shooting")]
    public float fireRate;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public LayerMask whatIsPlayer;

    [Header("References")]
    public GameObject graphics;
    public GameObject deathPrefab;

    [HideInInspector] public float startingRadius;
    private Transform playerPos;
    private float timer = 0f;
    private WorldBorders worldBorders;
    private bool playerDetected = false;
    private float randomSpotTimer;
    private Vector2 randomSpot;
    private Boat playerBoat;
    private bool isChasing = false;
    private bool wasChasing = false;
    private bool wasShootingAtPlayer = false;

    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
        playerBoat = playerPos.gameObject.GetComponent<Boat>();

        worldBorders = FindObjectOfType<WorldBorders>();
        startingRadius = playerDetectRadius;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        randomSpot = worldBorders.ChooseRandomSpot();
    }
    void Update()
    {
        playerDetected = Physics2D.OverlapCircle(transform.position, playerDetectRadius, whatIsPlayer);

        if (playerPos != null)
        {
            if (playerBoat.isCarryingContraband && playerDetected)
            {
                FollowPlayer();
                if (Time.time >= timer)
                {
                    ShootWater();
                    timer = Time.time + fireRate;
                }
            }
        }
        if (playerDetected == false || playerBoat.isCarryingContraband == false)
        {
            GoToRandomPoint();
        }
    }

    void ShootWater()
    {
        GameObject newBullet = Instantiate(bulletPrefab, shootPoint.position, transform.localRotation);

        Vector2 direction = shootPoint.up;
        newBullet.GetComponent<EnemyBullet>().shootDirection = direction;
    }
    void FollowPlayer()
    {
        navMeshAgent.speed = 5.5f;
        playerDetectRadius = startingRadius * 2;
        if (playerBoat.isBeingChased == false || isChasing == true)
        {
            playerBoat.isBeingChased = true;
            isChasing = true;
            wasChasing = true;
            if (navMeshAgent.pathPending == false)
            {
                navMeshAgent.SetDestination(playerPos.position);
            }
        }
        if (isChasing == false)
        {
            wasShootingAtPlayer = true;
            if (navMeshAgent.pathPending == false)
                navMeshAgent.SetDestination(transform.position);
        }

        Vector3 direction = playerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        graphics.transform.rotation = Quaternion.RotateTowards(graphics.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void GoToRandomPoint()
    {
        navMeshAgent.speed = 3f;
        playerDetectRadius = startingRadius;
        isChasing = false;
        if (Time.time >= randomSpotTimer || wasShootingAtPlayer == true || wasChasing == true)
        {
            if (wasChasing == true)
            {
                playerBoat.isBeingChased = false;
                wasChasing = false;
            }
            wasShootingAtPlayer = false;
            randomSpot = worldBorders.ChooseRandomSpot();
            navMeshAgent.SetDestination(randomSpot);
            randomSpotTimer = Time.time + 45f;
        }

        RotateBoat();

        if (Vector2.Distance(transform.position, randomSpot) < 10f)
        {
            randomSpotTimer = 0;
        }
    }

    void RotateBoat()
    {
        float angle = Mathf.Atan2(navMeshAgent.velocity.y, navMeshAgent.velocity.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        graphics.transform.rotation = Quaternion.RotateTowards(graphics.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            playerDetectRadius = startingRadius * 2;
        }
    }

    public void EnemyDeath()
    {
        if (wasChasing || isChasing)
        {
            playerBoat.isBeingChased = false;
        }
        isChasing = false;
        wasChasing = false;
        FindObjectOfType<EnemySpawner>().SpawnNewEnemy();
        GameObject deathBoat = Instantiate(deathPrefab, transform.position, graphics.transform.rotation);
        Destroy(deathBoat, 3f);
        Destroy(gameObject);
    }
}
