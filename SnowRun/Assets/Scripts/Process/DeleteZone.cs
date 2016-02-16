using UnityEngine;
using System.Collections;

public class DeleteZone : MonoBehaviour
{

    public ObjectPool linePool;
    public ObjectPool treePool;

    public MapCreate mapCreate;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        GameObject collidedObject = other.gameObject;

        if(other.transform.parent.CompareTag("Line"))
        {
            GameObject line = other.transform.parent.gameObject;
            foreach (Transform child in line.transform)
            {
                Debug.Log(child.tag);
                if (child.CompareTag("Tree"))
                    treePool.ReturnObject(child.gameObject);
            }

            linePool.ReturnObject(other.transform.parent.gameObject);
            mapCreate.CreateLine();
            mapCreate.MoveGround();
        }
    }
}
