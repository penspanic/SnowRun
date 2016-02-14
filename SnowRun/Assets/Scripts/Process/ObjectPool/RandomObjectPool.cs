using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomObjectPool : ObjectPool
{
    public GameObject[] originals;

    List<GameObject> remainingObjectList = new List<GameObject>();

    public override void InitPool(int startCount = 0, GameObject original = null)
    {
        if (originals == null)
            originals = new GameObject[] { original };

        for (int i = 0; i < startCount; i++)
            remainingObjectList.Add(CreateNewObject());
    }

    public override GameObject CreateNewObject()
    {
        GameObject newObj;

        newObj = Instantiate(originals[Random.Range(0, originals.Length)]);
        newObj.transform.SetParent(this.transform);
        newObj.SetActive(false);

        return newObj;
    }

    public override GameObject GetObject()
    {
        if (remainingObjectList.Count == 0)
            remainingObjectList.Add(CreateNewObject());

        GameObject returnObj = remainingObjectList[Random.Range(0, remainingObjectList.Count)];

        remainingObjectList.Remove(returnObj);
        if (initMethod != null)
            initMethod(returnObj);

        returnObj.transform.SetParent(null);
        returnObj.SetActive(true);

        return returnObj;
    }

    public override void ReturnObject(GameObject obj)
    {
        if (releaseMethod != null)
            releaseMethod(obj);

        obj.transform.SetParent(this.transform);
        obj.SetActive(false);

        remainingObjectList.Add(obj);
    }

}
