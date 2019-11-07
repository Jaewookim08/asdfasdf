using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : MonoBehaviour
{
    public Camera Camera1, Camera2;

    // Start is called before the first frame update
    void Start()
    {
        float margin = (160f / Screen.height);
        Camera1.rect = new Rect(0f, margin, 0.49f, 1f-margin);
        Camera2.rect = new Rect(0.51f, margin, 0.49f, 1f-margin);
    }
}
