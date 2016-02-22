﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeleteZone : MonoBehaviour
{

    public ObjectPool linePool;
    public ObjectPool treePool;

    public MapCreate mapCreate;

    List<GameObject> returnTreeList = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (other.transform.parent.CompareTag("Line"))
        {
            GameObject line = other.transform.parent.gameObject;
            returnTreeList.Clear();
            foreach (Transform child in line.transform)
            {
                Debug.Log(child.tag);
                if (child.CompareTag("Tree"))
                    returnTreeList.Add(child.gameObject);
            }
            foreach (GameObject eachTree in returnTreeList)
                treePool.ReturnObject(eachTree);

            linePool.ReturnObject(other.transform.parent.gameObject);
            mapCreate.CreateLine();
            mapCreate.MoveGround();
        }
    }
}
