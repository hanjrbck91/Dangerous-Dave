using UnityEngine;

public class JetController : MonoBehaviour
{
    public float jetSpeed = 5f; // Speed of jetpack movement
    public float maxJetDuration = 5f; // Maximum duration of jetpack use
    public float jetForce = 10f; // Force applied when using the jetpack

    private bool isJetActive = false;
    private float jetDuration = 0f;
    private Rigidbody2D rb;
    private bool isMovingVertically = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if both Alt keys are pressed
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (!isJetActive)
            {
                Debug.Log("Alt keys pressed. Activating jetpack.");
                ActivateJetpack();
            }
            else
            {
                DeactivateJetpack();
            }
        }

        if (isJetActive)
        {
            // Use input from arrow keys to control jet movement
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Check for vertical movement
            if (verticalInput != 0f)
            {
                isMovingVertically = true;
                rb.velocity = new Vector2(rb.velocity.x, verticalInput * jetSpeed);
            }
            else
            {
                rb.velocity = Vector2.zero; // No movement
                isMovingVertically = false;
            }

            // Decrease jet duration
            jetDuration -= Time.deltaTime;

            // Apply jet force
            if (isMovingVertically)
            {
                rb.AddForce(Vector2.up * jetForce, ForceMode2D.Force);
            }

            // Deactivate jetpack if duration is over
            if (jetDuration <= 0f)
            {
                DeactivateJetpack();
            }
        }
    }

    // Call this method when the player collects the jet icon item
    public void ActivateJetpack()
    {
        isJetActive = true;
        jetDuration = maxJetDuration;
        rb.gravityScale = 0f; // Disable gravity
    }

    // Deactivate the jetpack
    private void DeactivateJetpack()
    {
        isJetActive = false;
        rb.gravityScale = 1f; // Restore normal gravity
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset vertical velocity
    }

    // Property to check if the jetpack is active
    public bool IsJetActive => isJetActive;
}
