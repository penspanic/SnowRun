using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour
{
    static GameObject sharedObject;
    void Awake()
    {
        if(sharedObject == null)
        {
            sharedObject = Resources.Load<GameObject>("ASDASD");
        }
    }
    void Update()
    {

    }
}
 