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
    private Transform cameraTransform;
    private LayerMask nonPlayerMask;
    private float horizontal;
    private float speed = 2f;
    private float jumpingPower = 4f;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Transform tf;
    private void Start()
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        nonPlayerMask = (LayerMask)(~(1 << playerLayer));

        cameraTransform = Camera.main.transform;

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }
    void Update()
    {
        UpdateCamera();

        // Read player controls.
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && canJump())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    

    private bool canJump()
    {
        Vector3 transformWithOffset = new Vector3(tf.position.x, tf.position.y + 0.4f, tf.position.z);
        return Physics2D.OverlapBox(transformWithOffset, new Vector2(1.0f, 0.8f), 0, nonPlayerMask);
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

    private void UpdateCamera()
    {
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
    }

}

// ref: https://gist.github.com/bendux/5fab0c176855d4e37bf6a38bb071b4a4



// alt option: https://www.youtube.com/watch?v=Gf8LOFNnils&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=2
