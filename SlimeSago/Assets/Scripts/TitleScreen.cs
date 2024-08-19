using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    private bool loaded = false;
    private const float loadTime = 1.0f;
    private float timer = 2.0f;
    private const string levelOne = "AshLevel";
    public Material mat;

    void Start() {
        mat = GetComponent<Renderer>().material;
        updateTransparency(0.001f);
    }

    void Update()
    {
        if (!loaded)
        {
            updateTransparency(mat.color.a + (Time.deltaTime / loadTime));
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (mat.color.a >= 0.99f)
        {
            if (!loaded)
            {
                updateTransparency(1);
            }
            loaded = true;
        }
        else if (loaded && mat.color.a < 0.01f)
        {
            updateTransparency(0);
            SceneManager.LoadScene(levelOne);
        }
        if (loaded && timer <= 0)
        {
            updateTransparency(mat.color.a - (Time.deltaTime / loadTime));
        }
    }

    void updateTransparency(float newAlpha) {
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, newAlpha);
    }
}
