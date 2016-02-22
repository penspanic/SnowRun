using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
    public GameObject cubePrefab;

    float radius = 20f;
    float speed = 5f;
    Vector3 center = new Vector3(0, 0, 10);
    int count = 10;
    GameObject[] cubes;
    int selectedIndex = 0;
    void Awake()
    {
        cubes = new GameObject[count];
        Vector3 pos = new Vector3();
        for (int i = 0; i < count; i++)
        {
            cubes[i] = Instantiate(cubePrefab);
            float angle = 10 * i * Mathf.Deg2Rad;
            pos.x = center.x + Mathf.Sin(angle) * radius;
            pos.z = center.z - Mathf.Cos(angle) * radius / 2;
            Debug.Log(angle);
            cubes[i].transform.position = pos;
            cubes[i].GetComponent<Renderer>().material.color = Color.red *  (1 - 0.1f * i);
        }
    }

    void Update()
    {
        Vector3 newPos = new Vector3();
        float speedScale = 2 * Mathf.PI / speed;
        float angle = Time.time * speedScale;
        newPos.x = 0 + Mathf.Sin(angle) * radius;
        newPos.z = 0 + Mathf.Cos(angle) * radius;

        transform.position = newPos;

    }

    public void OnLeftButtonDown()
    {
        if (isMoving || selectedIndex >=  count - 1)
            return;
        float currAngle;
        float targetAngle;
        for(int i = 0;i<cubes.Length;i++)
        {
            currAngle = 10 * (i - selectedIndex) * Mathf.Deg2Rad;
            targetAngle = 10 *(i - selectedIndex - 1) * Mathf.Deg2Rad;
            StartCoroutine(MoveCube(cubes[i], currAngle, targetAngle));
        }
        selectedIndex++;
    }

    public void OnRightButtonDown()
    {
        if (isMoving || selectedIndex == 0)
            return;
        float currAngle;
        float targetAngle;
        for (int i = 0; i < cubes.Length; i++)
        {
            currAngle = 10 * (i - selectedIndex) * Mathf.Deg2Rad;
            targetAngle = 10 * (i - selectedIndex + 1) * Mathf.Deg2Rad;
            StartCoroutine(MoveCube(cubes[i], currAngle, targetAngle));
        }
        selectedIndex--;
    }

    const float moveTime = 1f;
    bool isMoving = false;
    IEnumerator MoveCube(GameObject cube, float startAngle, float endAngle)
    {
        isMoving = true;

        float elapsedTime = 0f;
        float currAngle = startAngle;
        Vector3 currPos = Vector3.zero;
        while(elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            currAngle = Mathf.SmoothStep(startAngle, endAngle, elapsedTime / moveTime);
            currPos.x = center.x + Mathf.Sin(currAngle) * radius;
            currPos.z = center.z - Mathf.Cos(currAngle) * radius / 2;
            cube.transform.position = currPos;
            yield return null;
        }
        currPos.x = center.x + Mathf.Sin(endAngle) * radius;
        currPos.z = center.z - Mathf.Cos(endAngle) * radius / 2;
        cube.transform.position = currPos;
        isMoving = false;
    }
}