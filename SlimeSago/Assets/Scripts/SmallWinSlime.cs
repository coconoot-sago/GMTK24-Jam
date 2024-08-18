using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallWinSlime : MonoBehaviour
{
    private float size = 1;
    private static float GROW_FACTOR = 0.01f;
    private static float MAX_SIZE = 5;

    // Update is called once per frame
    void Update()
    {
        if (size < MAX_SIZE) {
            size += GROW_FACTOR;
        }
    }

    void FixedUpdate() {
        transform.localScale = new Vector3(size, size, 1);
    }
}
