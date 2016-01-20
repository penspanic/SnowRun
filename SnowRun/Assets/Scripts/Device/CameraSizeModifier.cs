using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraSizeModifier : MonoBehaviour
{
    public int DesiredWidth;
    public int DesiredHeight;
    public float scaler = 1.0f;
    Camera cam;
    // Update is called once per frame
    void Update()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }
        int width = Screen.width;
        int height = Screen.height;
        float desiredRatio = (float)DesiredWidth / (float)DesiredHeight;
        float curRatio = (float)width / (float)height;

        if (curRatio < desiredRatio)
        {
            cam.orthographicSize = 3.6f * (1.0f / curRatio) * scaler;
        }
        else
        {
            cam.orthographicSize = 3.6f;
        }
    }
}
