using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float fireInterval = 0.5f; // Time interval between shots
    public float bulletLifetime = 2f; // Lifetime of bullets in seconds

    private bool canFire = true;
    private float lastFireTime;

    void Start()
    {
        lastFireTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && canFire)
        {
            Shoot();
            lastFireTime = Time.time;
        }

        // Reset canFire after fireInterval seconds
        if (Time.time - lastFireTime >= fireInterval)
        {
            canFire = true;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Determine bullet direction based on player's facing direction
        Vector2 bulletDirection = Vector2.right;
        if (transform.localScale.x < 0f) // Check if player is facing left
        {
            bulletDirection = Vector2.left;

            // Flip the bullet's sprite when facing left
            SpriteRenderer bulletSpriteRenderer = bullet.GetComponent<SpriteRenderer>();
            if (bulletSpriteRenderer != null)
            {
                bulletSpriteRenderer.flipX = true;
            }
        }

        rb.AddForce(bulletDirection * bulletForce, ForceMode2D.Impulse);

        // Destroy the bullet after a time interval
        Destroy(bullet, bulletLifetime);

        canFire = false; // Prevent continuous firing
    }
}
