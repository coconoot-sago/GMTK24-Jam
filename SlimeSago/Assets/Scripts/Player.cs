using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/**
 * TODO:
 *  - fix isgrounded to prevent infinite jumps
 *  
 */

public class Player : MonoBehaviour
{
    private Transform cameraTransform;
    public Animator animator;
    private Rigidbody2D rb;
    private Transform tf;

    private LayerMask nonPlayerMask;
    private Vector3 originalPosition;

    private float horizontal;
    private float speed = 4f;
    private float jumpingPower = 8f; // This ends up being around 3 units tall
    private bool jump = false;
    private int wallJump = 0;

    private void Start()
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        nonPlayerMask = (LayerMask)(~(1 << playerLayer));
        originalPosition = transform.position;

        cameraTransform = Camera.main.transform;

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }
    void Update()
    {
        UpdateCamera();

        // Read player controls.
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Horizontal", horizontal);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        // For debug purposees
        if (Input.GetKeyDown(KeyCode.R) || transform.position.y < -20)
        {
            Reset();
        }
        
    }

    private void FixedUpdate()
    {
        bool isJumping = animator.GetBool("IsJumping");

        // if wall-jumping or free-falling, and no horizontal inputs from the player, deccelerate horizontally
        if ((horizontal == 0 || (wallJump != 0 && rb.velocity.x * horizontal < 0)) && isJumping)
        {
            rb.velocity = new Vector2(Math.Abs(rb.velocity.x) < 0.1 ? 0 : 0.9f * rb.velocity.x, rb.velocity.y);
        }
        // if free-falling and has horizontal inputs from the player, gradually change velocity to direction player inputted
        else if (isJumping)
        {
            float velocityX = rb.velocity.x + 0.5f * horizontal;
            velocityX = horizontal * Math.Min(speed, Math.Abs(velocityX));
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
        }
        // set horizontal speed based on inputs from player
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        bool allowedToJump = canJump();
        if (jump && allowedToJump)
        {
            animator.SetBool("IsJumping", true);

            bool grounded = isGrounded();
            bool leftWallJump = canWallJump(-1);
            bool rightWallJump = canWallJump(1);

            // if grounded, or can wall jump on both sides, jump up straight
            if (grounded || (leftWallJump && rightWallJump))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
            else if (leftWallJump)
            {
                rb.velocity = new Vector2(jumpingPower, jumpingPower);
                wallJump = -1;
            }
            else if (rightWallJump)
            {
                rb.velocity = new Vector2(-jumpingPower, jumpingPower);
                wallJump = 1;
            }
        }
        else if (allowedToJump)
        {
            animator.SetBool("IsJumping", false);
        }

        jump = false;

        animator.SetFloat("Vertical", rb.velocity.y);
        // animator is constructed s.t. if vertical < 0, slime will be considered "falling", and IsJumping needs
        // to be true for it to not land
        if (rb.velocity.y < -1.0f)
        {
            animator.SetBool("IsJumping", true);
        }

        if (wallJump != 0 && wallJump * rb.velocity.x >= 0) {
            wallJump = 0;
        }
    }

    

    private bool canJump()
    {   
        Vector3 transformWithOffset = new Vector3(tf.position.x, tf.position.y + 0.45f, tf.position.z);
        return Physics2D.OverlapBox(transformWithOffset, new Vector2(1.0f, 0.9f), 0, nonPlayerMask);
    }

    private bool isGrounded() {
        Vector3 transformWithOffset = new Vector3(tf.position.x, tf.position.y + 0.05f, tf.position.z);
        return Physics2D.OverlapBox(transformWithOffset, new Vector2(0.8f, 0.1f), 0, nonPlayerMask);
    }

    private bool canWallJump(int dir) {
        Vector3 transformWithOffset = new Vector3(tf.position.x + dir * 0.45f, tf.position.y + 0.5f, tf.position.z);
        return Physics2D.OverlapBox(transformWithOffset, new Vector2(0.1f, 0.8f), 0, nonPlayerMask);
    }

    private void UpdateCamera()
    {
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
    }

    // PUBLIC METHODS --------------------------------------------------------------------------------
    // Called by evilplatform, etc.
    public void Reset()
    {
        transform.position = originalPosition;
    }

}

// ref: https://gist.github.com/bendux/5fab0c176855d4e37bf6a38bb071b4a4



// alt option: https://www.youtube.com/watch?v=Gf8LOFNnils&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=2
