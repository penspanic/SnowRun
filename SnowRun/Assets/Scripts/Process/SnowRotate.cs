using UnityEngine;
using System.Collections;

public enum RotateDirection
{
    ClockWise,
    CounterClockWise,
}
public class SnowRotate : MonoBehaviour
{
    public RotateDirection direction;
    public float speed;

    void Awake()
    {

    }
    void Update()
    {
        transform.Rotate(0, 0, speed * (direction == RotateDirection.ClockWise ? 1 : -1) * Time.deltaTime);
    }
}
