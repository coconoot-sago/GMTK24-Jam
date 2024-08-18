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

    // scale unit increment to target, per update
    private float scaleRate = 0.4f; 

    // CONSTANTS --------------------------------------------------

    // How much to multiply scale by, for each increment. 
    // [SerializeField]
    private float scaleFactor = 2.0f;
    private PlatformType platformType;


    // STATE ------------------------------------------------------
    private Vector3 targetScale;

    // Start is called before the first frame update
    void Start()
    {
        // Set up fields
        platformType = getPlatformType();
        targetScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Update targetScale
        if (platformType == PlatformType.Xplatform)
        {
            // Scale platform in x direction
            targetScale = Vector3.Scale(targetScale, new Vector3(getScaleX(), 1, 1));

        }
        else if (platformType == PlatformType.Yplatform)
        {
            targetScale = Vector3.Scale(targetScale, new Vector3(1, getScaleY(), 1));
        }
    }

    private void FixedUpdate()
    {
        // Increment platform towards targetScale
        transform.localScale = getClampedScale(transform.localScale, targetScale);
    }

    private Vector3 getClampedScale(Vector3 curr, Vector3 targ)
    {
        float x = curr.x + Mathf.Clamp(targ.x - curr.x, -scaleRate, scaleRate);
        float y = curr.y + Mathf.Clamp(targ.y - curr.y, -scaleRate, scaleRate);
        float z = curr.z + Mathf.Clamp(targ.z - curr.z, -scaleRate, scaleRate);
        return new Vector3(x, y, z);
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
