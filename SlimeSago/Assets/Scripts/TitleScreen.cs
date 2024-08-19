using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    private bool loaded = false;
    private float timer = 3.0f;
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
            updateTransparency(mat.color.a * 1.01f);
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer < 1 && mat.color.a > 0)
                {
                    updateTransparency(mat.color.a * 0.995f);
                }
            }
            else {
                updateTransparency(0);
            SceneManager.LoadScene(levelOne);
            }
        }

        if (mat.color.a >= 0.99f)
        {
            loaded = true;
        }
    }

    void updateTransparency(float newAlpha) {
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, newAlpha);
    }
}
