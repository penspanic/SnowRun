using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pick : MonoBehaviour,IUserInterface
{
    GameObject sockPrefab;
    GameObject sock;
    GameObject sockParent;
    bool canTouch = true;

    CameraMove cameraMove;

    Vector3 originalGravity;
    void Awake()
    {
        sockPrefab = Resources.Load<GameObject>("Object/sock");
        sockParent = GameObject.Find("Sock Parent");

        cameraMove = GameObject.FindObjectOfType<CameraMove>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canTouch)
            Explosion();

    }
    public void Enter()
    {
        Debug.Log("Pick Enter!");
        this.gameObject.SetActive(true);

        cameraMove.MoveCamera(false);
        canTouch = true;
        PrepareSock();

        originalGravity = Physics.gravity;

        GameObject temp = new GameObject();
        temp.transform.rotation = Quaternion.Euler(-30, 0, 0);
        Physics.gravity = temp.transform.forward * 9.8f * -1;
        Destroy(temp);
    }
    public void Leave()
    {
        Debug.Log("Pick Leave!");
        this.gameObject.SetActive(false);

        Physics.gravity = originalGravity;
        cameraMove.MoveCamera(true);
    }
    void Explosion()
    {
        canTouch = false;
        Collider[] colliders = sock.GetComponentsInChildren<Collider>();

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rgdBdy = colliders[i].GetComponent<Rigidbody>();
            rgdBdy.WakeUp();
            rgdBdy.isKinematic = false;

            rgdBdy.AddExplosionForce(10, sock.transform.position, 1000);
            rgdBdy.useGravity = true;
        }
        StartCoroutine(RemovePixels());
    }
    void PrepareSock()
    {
        sock = Instantiate(sockPrefab, Vector2.zero, new Quaternion()) as GameObject;
        sock.transform.SetParent(sockParent.transform);
        sock.transform.localPosition = new Vector3(0, -1.5f, 0);
        sock.transform.localRotation = new Quaternion();

        Rigidbody[] rgdBdys = sock.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rgdBdys.Length; i++)
            rgdBdys[i].Sleep();

    }
    void RandomPick()
    {
        
    }
    IEnumerator RemovePixels()
    {
        yield return new WaitForSeconds(2f);
        Destroy(sock);
    }
}
