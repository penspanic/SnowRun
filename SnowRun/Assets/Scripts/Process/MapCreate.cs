using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MapPattern
{
    public float[] xMoveValue;
    int currentIndex = 0;
    public float GetNextValue()
    {
        if (currentIndex + 1 >= xMoveValue.Length)
        {
            currentIndex = 0;
            return -100f;
        }
        else
        {
            return xMoveValue[currentIndex++];
        }
    }
}
public class MapCreate : MonoBehaviour
{
    List<MapPattern> patternList;
    ObjectCreate objectCreate;
    ObstaclePattern currObsPattern;
    MapPattern currentPattern;
    public float tileXInterval = 0.5f;

    [HideInInspector]
    public Vector3 tileScale;
    [HideInInspector]
    public Vector3 startTileCreatePos;
    [HideInInspector]
    public Vector3 createPos;
    [HideInInspector]
    public Transform prevTileTransform;
    [HideInInspector]
    public Transform mapTransform;
    [HideInInspector]
    public GameObject obstaclePoint;
    #region Objects
    GameObject tile;
    GameObject tile_tree;
    GameObject[] trees;
    GameObject ground;
    #endregion
    void Awake()
    {

        tile = Resources.Load<GameObject>("Object/Tile/tile");
        tile_tree = Resources.Load<GameObject>("Object/Tile/tile_tree");
        trees = Resources.LoadAll<GameObject>("Object/Tree");
        obstaclePoint = Resources.Load<GameObject>("Object/Obstacle Point");

        objectCreate = GameObject.FindObjectOfType<ObjectCreate>();
        mapTransform = GameObject.Find("Map").transform;
        ground = GameObject.Find("Ground");
        tileScale = tile.transform.localScale;

        startTileCreatePos = GameObject.Find("Map").transform.position;

        #region Pattern Add

        patternList = new List<MapPattern>(){
            new MapPattern(){xMoveValue = new float[]{0.25f,    0.25f,  0.25f,  0.25f,  0.25f,  0.25f,  0.25f}},
            new MapPattern(){xMoveValue = new float[]{-0.25f,   -0.25f, -0.25f, -0.25f, -0.25f, -0.25f, -0.25f}},
        };

        currentPattern = new MapPattern() { xMoveValue = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
        #endregion

        for (int i = 0; i < 30; i++)
            CreateLine();
    }
    void Update()
    {

    }
    public void CreateLine()
    {
        GameObject newTile;


        if (prevTileTransform == null)
            createPos = startTileCreatePos;
        else
            createPos = prevTileTransform.position;
        #region Create Tile

        for (int i = 0; i < 4; i++)
        {

            newTile = Instantiate(tile, createPos, tile.transform.rotation) as GameObject;
            newTile.transform.SetParent(mapTransform);
            newTile.transform.Translate(tileScale.x * i, 0, -tileScale.z);

            Renderer rend = newTile.GetComponent<Renderer>();
            float brightness = Random.Range(0.8f, 1f);
            rend.material.color = new Color(brightness, brightness, brightness);

            if (i == 0)
                prevTileTransform = newTile.transform;
        }

        #endregion
        #region Create Obstacle

        if (currObsPattern != null) // 이번 라인에 장애물을 생성해야할 때
        {
            PatternData data = currObsPattern.GetObstacles();
            GameObject newObstacle = null;
            for (int i = 0; i < data.createPosIndex.Length; i++)
            {
                // 장애물 생성
                int obstacleIndex = currObsPattern.isSingleObject ? 0 : Random.Range(0, data.Objects.Length);
                newObstacle = Instantiate(data.Objects[obstacleIndex],
                    data.Objects[obstacleIndex].transform.position + createPos,
                    data.Objects[obstacleIndex].transform.rotation) as GameObject;
                newObstacle.transform.Rotate(new Vector3(-30, 0, 0), Space.World);
                newObstacle.transform.Translate(data.createPosIndex[i] * 1f, 0, 0, Space.World);
                newObstacle.transform.localScale *= currObsPattern.scale;
                objectCreate.obstacleList.Add(newObstacle);
            }
            if (currObsPattern.isScoreContains) // 장애물을 지나갔을 때 점수를 줄 경우
            {
                // 플레이어와 닿으면 CollisionEnter 메서드를 통해 점수 계산
                GameObject newScore = Instantiate<GameObject>(obstaclePoint);
                newScore.transform.SetParent(newObstacle.transform, true);
                newScore.transform.position = createPos;
                newScore.transform.Translate(1.5f, 0, 0, Space.World);
                newScore.AddComponent<ObstaclePoint>().point = currObsPattern.point;
            }

            currObsPattern = null;
        }
        #endregion
        #region Create Tile_Tree & Tree
        GameObject newTile_Tree;
        GameObject newTree;
        GameObject tree_original;

        float treeScale;

        newTile_Tree = Instantiate(tile_tree, new Vector3(createPos.x - 0.75f, prevTileTransform.position.y, prevTileTransform.position.z), tile.transform.rotation) as GameObject;
        newTile_Tree.transform.SetParent(mapTransform);

        tree_original = trees[Random.Range(0, trees.Length)];
        newTree = Instantiate(tree_original, newTile_Tree.transform.position, tile.transform.rotation) as GameObject;
        newTree.transform.Translate(tree_original.transform.position);
        newTree.transform.SetParent(mapTransform);
        treeScale = Random.Range(1f, 2f);
        newTree.transform.localScale *= treeScale;

        newTile_Tree = Instantiate(tile_tree, new Vector3(createPos.x + 3.75f, prevTileTransform.position.y, prevTileTransform.position.z), tile.transform.rotation) as GameObject;
        newTile_Tree.transform.SetParent(mapTransform);

        tree_original = trees[Random.Range(0, trees.Length)];
        newTree = Instantiate(tree_original, newTile_Tree.transform.position, tile.transform.rotation) as GameObject;
        newTree.transform.Translate(tree_original.transform.position);
        newTree.transform.SetParent(mapTransform);
        treeScale = Random.Range(1f, 2f);
        newTree.transform.localScale *= treeScale;
        #endregion
    }
    float PatternProcess()
    {
        float xMoveValue = currentPattern.GetNextValue();
        if (xMoveValue == -100)
        {
            currentPattern = patternList[Random.Range(0, patternList.Count)];
            xMoveValue = currentPattern.GetNextValue();
            MoveGroundX(xMoveValue);
            return xMoveValue;
        }
        else
        {
            MoveGroundX(xMoveValue);
            return xMoveValue;
        }
    }
    public void NewObstacles(ObstaclePattern pattern)
    {
        currObsPattern = pattern;
    }
    public void MoveGroundZ()
    {
        ground.transform.Translate(Vector3.back * 0.5f);
    }
    void MoveGroundX(float value)
    {
        ground.transform.Translate(Vector3.right * value);
    }
}