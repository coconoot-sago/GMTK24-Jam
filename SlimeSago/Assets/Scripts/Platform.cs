using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private enum PlatformType
    {
        Unknown,
        Xplatform,
        Yplatform
    }

    //private double minScale = 0.5;
    //private double maxScale = 10;

    // CONSTANTS --------------------------------------------------

    // How much to multiply scale by, for each increment. 
    // [SerializeField]
    private float scaleFactor = 2.0f;
    private PlatformType platformType;
    

    // Start is called before the first frame update
    void Start()
    {
        platformType = getPlatformType();
    }

    // Update is called once per frame
    void Update()
    {
        if (platformType == PlatformType.Xplatform)
        {
            // Scale platform in x direction
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(getScaleX(), 1, 1));

        }
        else if (platformType == PlatformType.Yplatform)
        {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1, getScaleY(), 1));
        }
    }

    private float getScaleX()
    {
        if (Input.GetButtonDown("scaleUpX") && !Input.GetButtonDown("scaleDownX"))
        {
            return scaleFactor;
        } else if (Input.GetButtonDown("scaleDownX") && !Input.GetButtonDown("scaleUpX"))
        {
            return 1/scaleFactor;
        }
        return 1;
    }

    private float getScaleY()
    {
        if (Input.GetButtonDown("scaleUpY") && !Input.GetButtonDown("scaleDownY"))
        {
            return scaleFactor;
        }
        else if (Input.GetButtonDown("scaleDownY") && !Input.GetButtonDown("scaleUpY"))
        {
            return 1 / scaleFactor;
        }
        return 1;
    }

    // Whether platform is horizontal or vertical
    private PlatformType getPlatformType()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        string name = spriteRenderer.sprite.name;
        if (name.Contains("horizontal", StringComparison.OrdinalIgnoreCase))
        {
            return PlatformType.Xplatform;
        } else if (name.Contains("vertical", StringComparison.OrdinalIgnoreCase))
        {
            return PlatformType.Yplatform;
        }
        else
        {
            return PlatformType.Unknown;
        }
    }
}
