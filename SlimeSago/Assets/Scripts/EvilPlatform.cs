using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPlatform : MonoBehaviour
{
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.Reset();
        }
    }
}
