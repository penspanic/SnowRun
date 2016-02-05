using UnityEngine;
using System.Collections;

public class DeleteZone : MonoBehaviour
{

    public ObjectPool linePool;

    void OnCollisionEnter(Collision other)
    {
        GameObject collidedObject = other.gameObject;

        if(other.transform.parent.CompareTag("Line"))
        {
            linePool.ReturnObject(other.transform.parent.gameObject);
        }
    }
}
