using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class ScreenRatioModifier : MonoBehaviour
{
    public int DesiredWidth;
    public int DesiredHeight;
    CanvasScaler scaler;
    // Update is called once per frame
    void Update()
    {
        if (scaler == null)
        {
            scaler = GetComponent<CanvasScaler>();
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        }
        int width = Screen.width;
        int height = Screen.height;
        float desiredRatio = (float)DesiredWidth / (float)DesiredHeight;
        float curRatio = (float)width / (float)height;
        Debug.Log("CurWidth=" + width + " CurHeight=" + height);
        Debug.Log("CurRatio=" + curRatio + " desRatio=" + desiredRatio);
        if (curRatio < desiredRatio)
        {
            //            Debug.Log("Set to width;");
            scaler.matchWidthOrHeight = 0.0f;
        }
        else
        {
            //            Debug.Log("Set to height;");
            scaler.matchWidthOrHeight = 1.0f;
        }

    }
}
