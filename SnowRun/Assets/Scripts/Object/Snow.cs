using UnityEngine;
using System.Collections;

public class Snow : MonoBehaviour
{
    public bool clockWise;
    public float rotateSpeed;

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * (clockWise ? 1 : -1) * Time.deltaTime);
    }
}