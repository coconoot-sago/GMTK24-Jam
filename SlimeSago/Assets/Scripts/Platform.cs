using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private enum PlatformType
    {
        Unknown,
        Horizontal,
        Vertical
    }

    //private double minScale = 0.5;
    //private double maxScale = 10;

    // CONSTANTS --------------------------------------------------

    // How much to multiply scale by, for each increment. 
    // [SerializeField]
    private float scaleFactor = 1.001f;
    private PlatformType platformType;
    

    // Start is called before the first frame update
    void Start()
    {
        platformType = getPlatformType();
    }

    // Update is called once per frame
    void Update()
    {
        float increment = 0.2f; // TODO: update with button input
        transform.localScale = Vector3.Scale(transform.localScale, getScaleMultiplier(increment));
    }

    // increment should either +1 or -1
    // returns new scale to multiply current scale by
    private Vector3 getScaleMultiplier(float increment)
    {
        float currScaleFactor = increment > 0 ? scaleFactor : 1 / scaleFactor;
        if (platformType == PlatformType.Horizontal)
        {
            return new Vector3(currScaleFactor, 1,1);
        } else if (platformType == PlatformType.Vertical)
        {
            return new Vector3(1, currScaleFactor, 1);
        } else
        {
            return Vector3.one;
        }
    }

    // Whether platform is horizontal or vertical
    private PlatformType getPlatformType()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        string name = spriteRenderer.sprite.name;
        if (name.Contains("horizontal", StringComparison.OrdinalIgnoreCase))
        {
            return PlatformType.Horizontal;
        } else if (name.Contains("vertical", StringComparison.OrdinalIgnoreCase))
        {
            return PlatformType.Vertical;
        }
        else
        {
            return PlatformType.Unknown;
        }
    }
}
