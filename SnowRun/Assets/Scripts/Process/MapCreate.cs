using UnityEngine;
using System.Collections;

public class MapCreate : MonoBehaviour
{
    public ObjectPool tilePool;
    public ObjectPool tile_treePool;

    void Awake()
    {
        tilePool.InitPool(10);
    }
}