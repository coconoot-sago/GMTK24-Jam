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
    public Animator animator;
    
    private LayerMask nonPlayerMask;
    private float horizontal;
    private float speed = 4f;
    private float jumpingPower = 8f;
    private bool jump = false;

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
        animator.SetFloat("Horizontal", horizontal);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        
    }

    private void FixedUpdate()
    {
        bool allowedToJump = canJump();
        if (jump && allowedToJump) {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if (allowedToJump) {
            animator.SetBool("IsJumping", false);
        }
        
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        jump = false;
    }

    

    private bool canJump()
    {   
        Vector3 transformWithOffset = new Vector3(tf.position.x, tf.position.y + 0.4f, tf.position.z);
        return Physics2D.OverlapBox(transformWithOffset, new Vector2(1.0f, 0.8f), 0, nonPlayerMask);
    }

    private void UpdateCamera()
    {
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
    }

}

// ref: https://gist.github.com/bendux/5fab0c176855d4e37bf6a38bb071b4a4



// alt option: https://www.youtube.com/watch?v=Gf8LOFNnils&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=2
