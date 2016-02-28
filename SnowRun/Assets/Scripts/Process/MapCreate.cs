﻿using UnityEngine;
using System.Collections;

public class MapCreate : MonoBehaviour
{
    public ObjectPool linePool;
    public ObjectPool treePool;

    public Transform mapTransform;
    public GameObject ground;
    public GameObject ground2;

    Vector3 prevLinePos = Vector3.zero;
    Vector3 createPos;

    void Awake()
    {

        linePool.initMethod = target => { target.transform.position = Vector3.zero; };
        linePool.releaseMethod = target => { target.transform.position = Vector3.zero; };
        linePool.InitPool(35);

        treePool.initMethod = target => { target.transform.position = Vector3.zero; };
        treePool.releaseMethod = target => { target.transform.position = Vector3.zero; };
        treePool.InitPool(100);

        for (int i = 0; i < 30; i++)
            CreateLine();
    }

    public void CreateLine()
    {
        if (prevLinePos == Vector3.zero)
            createPos = mapTransform.position;
        else
            createPos = prevLinePos;

        GameObject newLine = linePool.GetObject();
        newLine.transform.position = createPos;
        newLine.transform.Translate(new Vector3(0, 0, -0.5f));
        newLine.transform.SetParent(mapTransform);

        // Set Tile Color
        Renderer[] tileRenderers = newLine.transform.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < tileRenderers.Length; i++)
            SetTileColor(tileRenderers[i]);

        prevLinePos = newLine.transform.position;

        // Set Trees
        GameObject newTree;
        for (int i = 0; i < 2; i++)
        {
            newTree = treePool.GetObject();
            newTree.transform.position = new Vector3(
                createPos.x - 2.25f + i * 4.5f, createPos.y, createPos.z);
            newTree.transform.Translate(Vector3.up * 0.3f);
            newTree.transform.SetParent(newLine.transform);
            newTree.transform.localRotation = new Quaternion();
        }

        // Set Obstacles


    }

    public void MoveGround()
    {
        ground.transform.Translate(-Vector3.forward * 0.5f);
        ground2.transform.Translate(-Vector3.forward * 0.5f);
    }

    void SetTileColor(Renderer tileRenderer)
    {
        float brightness = Random.Range(0.8f, 1f);
        tileRenderer.material.color = new Color(brightness, brightness, brightness);
    }
}