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
    private int MAX_SCALE_LEVEL = 1;
    private int MIN_SCALE_LEVEL = -1;
    // How much to multiply scale by, for each increment. 
    private float SCALE_FACTOR = 2.0f;

    // scale unit increment to target, per update
    private float scaleRate = 0.4f; 

    // CONSTANTS --------------------------------------------------
    private Vector3 originalScale;
    private PlatformType platformType;

    // STATE ------------------------------------------------------
    private int scaleLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set up fields
        platformType = getPlatformType();
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Update targetScale
        if (platformType == PlatformType.Xplatform)
        {
            setScaleLevelForXplatform();

        }
        else if (platformType == PlatformType.Yplatform)
        {
            setScaleLevelForYplatform();
        }
    }

    private void FixedUpdate()
    {
        // Increment platform towards targetScale
        transform.localScale = getClampedScale(transform.localScale, getTargetScale());
    }

    private Vector3 getClampedScale(Vector3 curr, Vector3 targ)
    {
        float x = curr.x + Mathf.Clamp(targ.x - curr.x, -scaleRate, scaleRate);
        float y = curr.y + Mathf.Clamp(targ.y - curr.y, -scaleRate, scaleRate);
        float z = curr.z + Mathf.Clamp(targ.z - curr.z, -scaleRate, scaleRate);
        return new Vector3(x, y, z);
    }

    private Vector3 getTargetScale()
    {
        if (platformType == PlatformType.Xplatform)
        {
            return Vector3.Scale(originalScale, new Vector3(Mathf.Pow(SCALE_FACTOR, scaleLevel), 1, 1));
        } else if (platformType == PlatformType.Yplatform) {
            return Vector3.Scale(originalScale, new Vector3(1, Mathf.Pow(SCALE_FACTOR, scaleLevel), 1));
        } else
        {
            return originalScale;
        }
    }

    private void setScaleLevelForXplatform()
    {
        if (Input.GetButtonDown("scaleUpX") && !Input.GetButtonDown("scaleDownX"))
        {
            scaleLevel = (int) Mathf.Clamp(scaleLevel + 1, MIN_SCALE_LEVEL, MAX_SCALE_LEVEL);
        }
        else if (Input.GetButtonDown("scaleDownX") && !Input.GetButtonDown("scaleUpX"))
        {
            scaleLevel = (int)Mathf.Clamp(scaleLevel - 1, MIN_SCALE_LEVEL, MAX_SCALE_LEVEL);
        }
    }

    private void setScaleLevelForYplatform()
    {
        if (Input.GetButtonDown("scaleUpY") && !Input.GetButtonDown("scaleDownY"))
        {
            scaleLevel = (int)Mathf.Clamp(scaleLevel + 1, MIN_SCALE_LEVEL, MAX_SCALE_LEVEL);
        }
        else if (Input.GetButtonDown("scaleDownY") && !Input.GetButtonDown("scaleUpY"))
        {
            scaleLevel = (int)Mathf.Clamp(scaleLevel - 1, MIN_SCALE_LEVEL, MAX_SCALE_LEVEL);
        }
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

    // Updates originalScale var, and will naturally rerender to correct scale.
    // Used by CheckpointPlatform script.
    public void setOriginalScale(Vector3 newScale)
    {
        originalScale = newScale;
    }

    // Used by CheckpointPlatform script.
    public Vector3 getOriginalScale()
    {
        return originalScale;
    }
}
