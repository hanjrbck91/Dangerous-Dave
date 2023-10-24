using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform waypointsParent; // Reference to the waypoints parent GameObject
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 10f;
    public float shootInterval = 2f;

    public float enemySpeed = 3f; // Speed of the enemy, adjustable from the editor
    public float bulletLifetime = 3f; // Lifetime of bullets in seconds

    private int currentWaypointIndex = 0;
    private float lastShootTime;

    private Transform[] waypoints;

    [SerializeField] private AudioSource Deathsound;

    void Start()
    {
        waypoints = new Transform[waypointsParent.childCount];
        for (int i = 0; i < waypointsParent.childCount; i++)
        {
            waypoints[i] = waypointsParent.GetChild(i);
        }

        lastShootTime = Time.time;
    }

    void Update()
    {
        MoveOnPath();

        if (Time.time - lastShootTime >= shootInterval)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void MoveOnPath()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        // Move towards the current waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, Time.deltaTime * enemySpeed);

        // Check if the enemy reached the current waypoint
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void Shoot()
    {
        if (IsInCameraFrustum())
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Aim the bullet in the -x direction
            rb.AddForce(-firePoint.right * bulletForce, ForceMode2D.Impulse);

            // Destroy the bullet after a time interval
            Destroy(bullet, bulletLifetime);
        }
    }

    bool IsInCameraFrustum()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        return screenPos.x > 0f && screenPos.x < 1f && screenPos.y > 0f && screenPos.y < 1f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            // Instantiate the death tile at the player's position
            string deathTilePrefabPath = "Prefabs/DeathTilePrefab"; // Adjust the path based on your project structure
            GameObject deathTilePrefab = Resources.Load<GameObject>(deathTilePrefabPath);

            if (deathTilePrefab != null)
            {
                Debug.Log("Prefab loaded successfully.");
                GameObject deathTile = Instantiate(deathTilePrefab, transform.position, Quaternion.identity);

                // Destroy the death tile after 2 seconds
                Destroy(deathTile, 2f);
            }
            else
            {
                Debug.LogError("Prefab not found at path: " + deathTilePrefabPath);
            }

            Destroy(gameObject); // Destroy the enemy when hit by player's bullet
            Deathsound.Play();
            Debug.Log("Enemy destroyed");
        }
    }
}
