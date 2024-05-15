using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShadowTidesBoat : MonoBehaviour
{
    public float rotationSpeed;
    public float fireRate;
    public float playerDetectRadius;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public NavMeshAgent navMeshAgent;
    public LayerMask whatIsPlayer;

    private Transform playerPos;
    private float timer = 0f;
    private WorldBorders worldBorders;
    private bool playerDetected = false;
    [HideInInspector] public float startingRadius;
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
            if (playerDetected)
                FollowPlayer();
            else
            {
                GoToRandomPoint();
            }
        }

        if (playerDetected && Time.time >= timer)
        {
            ShootWater();
            timer = Time.time + fireRate;
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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void GoToRandomPoint()
    {
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

        AngleBoat();

        if (Vector2.Distance(transform.position, randomSpot) < 5f)
        {
            randomSpotTimer = 0;
        }
    }

    void AngleBoat()
    {
        float angle = Mathf.Atan2(navMeshAgent.velocity.y, navMeshAgent.velocity.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
        Destroy(gameObject);
    }
}
