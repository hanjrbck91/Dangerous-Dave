using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary> 
    /// PlayerMovement
    /// </summary>
    private float horizontal;
    [SerializeField] float speed = 4f;
    [SerializeField]  float jumpingPower = 4f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public Animator animator;

    bool isJumping;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource walkSound;

    private bool isMovementEnabled = true; // Flag to control player movement
    private bool isGravityEnabled = true; // Flag to control gravity

    private JetController jet;

    private void Start()
    {
        jet = GetComponent<JetController>();
    }
    private void Update()
    {
        if (jet != null && jet.isActiveAndEnabled)
        {
                    if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
                    {
                        ToggleGravity();
                        print("Jet is working Either on or off");
                    }
        }
        

        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && !jumpSound.isPlaying)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
        }
        else
        {
            walkSound.Stop();
        }

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && IsGrounded())
        {
            // Stop the walk sound if it's playing
            walkSound.Stop();

            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetBool("IsJumping", true);
            jumpSound.Play();
        }

        Flip();
    }



    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);

        // If the player is not jumping and is grounded, stop the jump animation
        if (!isJumping && IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }

        isJumping = false; // Reset isJumping flag
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void ToggleGravity()
    {
        isGravityEnabled = !isGravityEnabled;
        rb.gravityScale = isGravityEnabled ? 1f : 0f;
        if(!isGravityEnabled )
        {
            animator.SetBool("IsJetActive", true);
        }
        else
        {
            animator.SetBool("IsJetActive", false);
        }
    }


}
