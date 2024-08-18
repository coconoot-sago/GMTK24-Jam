using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPlatform : MonoBehaviour
{
    private Vector3 originalPosition;

    private Transform playerTransform;
    private Platform platformScript;

    private int HEIGHT_INCREMENT = 20; // checkpoint every n units

    // STATE -----------------------------------------------------
    private int checkpoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        playerTransform = GameObject.FindWithTag("Player").transform;
        platformScript = GetComponent<Platform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Use int division to floor
        if ((((int) playerTransform.position.y) / HEIGHT_INCREMENT) > checkpoint)
        {
            // set checkpoint
            checkpoint = (((int)playerTransform.position.y) / HEIGHT_INCREMENT);
            //Debug.Log("checkpoint at: " + (checkpoint * HEIGHT_INCREMENT));
            // update scale and position
            float yScale = (checkpoint * HEIGHT_INCREMENT) / 0.75f;
            Vector3 newScale = new Vector3(1, yScale, 1);
            Vector3 newPosition = new Vector3(originalPosition.x, 1.0f + -(yScale/4.0f), originalPosition.z);
            platformScript.setOriginalScale(newScale);
            transform.position = newPosition;
        }
    }
}
