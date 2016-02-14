using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectPool : MonoBehaviour
{
    public GameObject original;


    public delegate void InitDelegate(GameObject target);
    public delegate void ReleaseDelegate(GameObject target);

    public InitDelegate initMethod;
    public ReleaseDelegate releaseMethod;

    protected Queue<GameObject> remainingObjects = new Queue<GameObject>();
    public virtual void InitPool(int startCount = 0, GameObject original = null)
    {
        if (original != null)
            this.original = original;

        for (int i = 0; i < startCount; i++)
            remainingObjects.Enqueue(CreateNewObject());
    }

    public virtual GameObject CreateNewObject()
    {
        GameObject newObj;

        newObj = Instantiate(original);
        newObj.transform.SetParent(this.transform);
        newObj.SetActive(false);

        return newObj;
    }

    public virtual GameObject GetObject()
    {
        if (remainingObjects.Count == 0)
            remainingObjects.Enqueue(CreateNewObject());

        GameObject returnObj = remainingObjects.Dequeue();
        if (initMethod != null)
            initMethod(returnObj);

        returnObj.transform.SetParent(null);
        returnObj.SetActive(true);

        return returnObj;
    }

    public virtual void ReturnObject(GameObject obj)
    {
        if (releaseMethod != null)
            releaseMethod(obj);

        obj.transform.SetParent(this.transform);
        obj.SetActive(false);

        remainingObjects.Enqueue(obj);
    }
}
