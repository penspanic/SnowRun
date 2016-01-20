using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct PatternData
{
    public float[] createPosIndex;
    public GameObject[] Objects;
}
public class ObstaclePattern
{
    public bool isCreateRandomTile;
    public bool isSingleObject;
    public bool isScoreContains = true;
    public GameObject[] Obstacles;
    public float[][] createIndex;
    public float scale = 1;
    public int point;
    public PatternData GetObstacles()
    {
        int random = Random.Range(0, createIndex.Length);
        return new PatternData() { createPosIndex = createIndex[random], Objects = Obstacles };

    }
    public static int[] GetRandomNumber(int min, int max, int num)
    {
        int[] returnArr = new int[num];

        for (int i = 0; i < num; i++)
        {
            while (true)
            {
                bool isOverlapped = false;
                int randomNumber = Random.Range(min, max);
                for (int j = 0; j < num; j++)
                {
                    if (returnArr[j] == randomNumber)
                    {
                        isOverlapped = true;
                        break;
                    }
                }
                if (isOverlapped)
                    continue;
                returnArr[i] = randomNumber;
                if (i == num - 1)
                    break;
            }
        }
        return returnArr;
    }
}


public class ObjectCreate : MonoBehaviour
{
    public const float Create_Interval = 5f;
    public const float Decrease_Amount = 0.05f;
    public const float Minimum_Create_Inteval = 2f;
    MapCreate mapCreate;
    List<ObstaclePattern> obstaclePatterns;

    public List<GameObject> obstacleList = new List<GameObject>();
    public bool isCreating
    {
        get;
        private set;
    }

    GameObject[] trees;
    GameObject[] rocks;
    GameObject fallenLog;   
    GameObject corn;
    GameObject flag;
    GameObject coin;


    void Awake()
    {
        mapCreate = GameObject.FindObjectOfType<MapCreate>();

        trees = Resources.LoadAll<GameObject>("Object/Tree");
        rocks = Resources.LoadAll<GameObject>("Object/Obstacle/Rock");
        fallenLog = Resources.Load<GameObject>("Object/Obstacle/log");
        corn = Resources.Load<GameObject>("Object/Obstacle/corn");
        coin = Resources.Load<GameObject>("Object/Obstacle/coin");
        obstaclePatterns = new List<ObstaclePattern>(new ObstaclePattern[]{
            /*  강 추가 */
            new ObstaclePattern(){Obstacles = rocks, createIndex = new float[4][]{new float[]{0},new float[]{1},new float[]{2},new float[]{3}},point = 5},
            new ObstaclePattern(){Obstacles = rocks, createIndex = new float[1][]{new float[]{0,3}},point = 3},
            new ObstaclePattern(){Obstacles = new GameObject[]{fallenLog}, createIndex = new float[3][]{new float[]{0.5f}, new float[]{1.5f}, new float[]{2.5f}},point = 3},
            new ObstaclePattern(){Obstacles = rocks, createIndex = new float[3][]{new float[]{0.5f},new float[]{1.5f},new float[]{2.5f}}, scale = 2f, isSingleObject = true, point = 3},
            new ObstaclePattern(){Obstacles = new GameObject[]{corn},createIndex = new float[4][]{new float[]{0},new float[]{1},new float[]{2},new float[]{3}}, scale = 3f,isSingleObject =true, point = 5},
            new ObstaclePattern(){Obstacles = new GameObject[]{corn},createIndex = new float[4][]{new float[]{1,2,3},new float[]{0,2,3},new float[]{0,1,3},new float[]{0,1,2}},scale = 3f, isSingleObject = true, point = 3},
            new ObstaclePattern(){Obstacles = new GameObject[]{corn},createIndex = new float[3][]{new float[]{0,1},new float[]{1,2},new float[]{2,3}},scale = 3f,isSingleObject = true,point = 3},
            new ObstaclePattern(){Obstacles = new GameObject[]{corn},createIndex = new float[2][]{new float[]{0,2},new float[]{1,3}},scale = 3f, isSingleObject = true,point = 3},
            new ObstaclePattern(){Obstacles = new GameObject[]{corn},createIndex = new float[1][]{new float[]{0,3}},scale = 3f, isSingleObject = true,point = 5},
            new ObstaclePattern(){Obstacles = trees, createIndex = new float[4][]{new float[]{1,2,3},new float[]{0,2,3},new float[]{0,1,3},new float[]{0,1,2}},scale = 2f, point = 3},
            new ObstaclePattern(){Obstacles = trees, createIndex = new float[3][]{new float[]{0,1},new float[]{1,2}, new float[]{2,3}}, scale = 2f, point = 3},
            new ObstaclePattern(){Obstacles = trees, createIndex = new float[2][]{new float[]{0,2}, new float[]{1,3}}, scale = 2f,point = 3},
            new ObstaclePattern(){Obstacles = trees, createIndex = new float[1][]{new float[]{0,3}}, scale = 2f},
            // 깃발
            new ObstaclePattern(){Obstacles = new GameObject[]{coin},createIndex = new float[4][]{new float[]{0},new float[]{1}, new float[]{2}, new float[]{3}},isSingleObject = true, isScoreContains = false},

        });
    }

    float createInterval = Create_Interval;
    float elaspedTime = 0f;
    void Update()
    {
        if (isCreating)
        {
            if (elaspedTime >= createInterval)
            {
                elaspedTime = 0f;
                mapCreate.NewObstacles(obstaclePatterns[Random.Range(0, obstaclePatterns.Count)]);

                if (createInterval > Minimum_Create_Inteval)
                    createInterval -= Decrease_Amount;
            }
            else
            {
                elaspedTime += Time.deltaTime;
            }
        }
    }
    public void Init()
    {
        createInterval = 5f;
        for (int i = obstacleList.Count - 1; i >= 0; i--)
        {
            Destroy(obstacleList[i]);
            obstacleList.RemoveAt(i);
        }
        isCreating = true;
    }
    public void Release()
    {
        isCreating = false;
    }
    public void StartCreating()
    {
        isCreating = true;
    }
}
