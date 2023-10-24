using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerManager PlayerManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spider"))
        {
            Destroy(gameObject); // Destroy the bullet upon collision
            if (PlayerManager != null)
            {
                PlayerManager.AddPoints(300);
            }
        }
    }
}
