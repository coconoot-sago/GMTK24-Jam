using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * TODO:
 *  - fix isgrounded to prevent infinite jumps
 * 
 */

public class Player : MonoBehaviour
{
    private float horizontal;
    private float speed = 2f;
    private float jumpingPower = 4f;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Transform tf;
    private LayerMask groundLayerMask;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        groundLayerMask = LayerMask.GetMask("Ground");
    }
    void Update()
    {
        // Read player controls.
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        //if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        //}

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(tf.position, Vector2.one, 0, groundLayerMask);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}

// ref: https://gist.github.com/bendux/5fab0c176855d4e37bf6a38bb071b4a4



// alt option: https://www.youtube.com/watch?v=Gf8LOFNnils&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=2