using UnityEngine;

public class JetController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed

    private Rigidbody2D rb;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            ToggleMovement();
        }

        if (isMoving)
        {
            Move();
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        //movement.Normalize(); // Normalize to ensure constant speed in all directions

        rb.velocity = movement * moveSpeed;
    }

    void ToggleMovement()
    {
        isMoving = !isMoving;
        rb.velocity = Vector2.zero; // Stop the player immediately when toggling off movement
    }
}
