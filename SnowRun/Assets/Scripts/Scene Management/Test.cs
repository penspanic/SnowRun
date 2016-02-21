using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
    float radius = 5f;
    float speed = 5f;
    void Awake()
    {

    }

    void Update()
    {
        Vector3 newPos = new Vector3();
        float speedScale =  2 * Mathf.PI / speed;
        float angle = Time.time * speedScale;
        newPos.x = 0 + Mathf.Sin(angle) * radius;
        newPos.y = 0 + Mathf.Cos(angle) * radius;

        transform.position = newPos;
        Debug.Log(Time.time);
    }
}
