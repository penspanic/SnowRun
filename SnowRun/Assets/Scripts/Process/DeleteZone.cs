using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeleteZone : MonoBehaviour
{

    MapCreate mapCreate;
    ObjectCreate objectCreate;
    GameObject tile;


    void Awake()
    {
        mapCreate = GameObject.FindObjectOfType<MapCreate>();
        objectCreate = GameObject.FindObjectOfType<ObjectCreate>();
        tile = Resources.Load<GameObject>("Object/tile");
    }
    void Update()
    {

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            return;

        TileDetactProcess(other.gameObject);
        ObstacleDetactProcess(other.gameObject);

    }
    void OnTriggerEnter(Collider other)
    {

        TileDetactProcess(other.gameObject);
        ObstacleDetactProcess(other.gameObject);


    }
    void TileDetactProcess(GameObject tile)
    {
        if(tile.CompareTag("Tile"))
        {
            if(tile.gameObject.transform.localPosition.x == 0)
            {
                mapCreate.CreateLine();
                mapCreate.MoveGroundZ();
            }
            Destroy(tile);
        }
    }
    void ObstacleDetactProcess(GameObject obstacle)
    {
        if (!(obstacle.CompareTag("Obstacle")|| obstacle.CompareTag("Coin")))
            return;
        GameObject parent = null;
        if(obstacle.transform.parent != null)
            parent = obstacle.transform.parent.gameObject;   // 단일 오브젝트 일 시 null
        List<GameObject> children = new List<GameObject>();
        if(parent == null) // 단일 오브젝트 일 때
        {
            if (objectCreate.obstacleList.Contains(obstacle))
                objectCreate.obstacleList.Remove(obstacle);

            Destroy(obstacle);
        }
        else           // 단일 오브젝트가 아닐 때
        {

            if(objectCreate.obstacleList.Contains(parent))
                objectCreate.obstacleList.Remove(parent);

            Destroy(parent);      
        }
    }
}
