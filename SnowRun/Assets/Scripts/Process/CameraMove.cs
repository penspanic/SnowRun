﻿using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    public float menuCameraSpeed;
    public bool isMenuMoving = true;

    public static Vector3 dirVec;

    GameObject player;
    UIManager uiMgr;
    InGame inGame;

    bool isGameProcessing = false;
    bool isFirstTime = true;

    Vector3 forwardVec;
    Vector3 playerPrevPos;

    void Awake()
    {
        uiMgr = GameObject.FindObjectOfType<UIManager>();
        inGame = GameObject.FindObjectOfType<InGame>();

        Quaternion mapRotation = Quaternion.Euler(-30, 0, 0);
        GameObject temp = new GameObject();
        dirVec = Vector3.one;
        temp.transform.rotation = mapRotation;
        dirVec = temp.transform.forward * -1;
        Destroy(temp);

        forwardVec = transform.forward;
    }

    void Update()
    {
        if (isGameProcessing)
        {
            if (isFirstTime)
            {
                playerPrevPos = new Vector3(0, player.transform.position.y, player.transform.position.z);
                isFirstTime = false;
            }
            Vector3 currPos = new Vector3(0, player.transform.position.y, player.transform.position.z);
            transform.position += currPos - playerPrevPos;
            playerPrevPos = currPos;
        }
        else
        {
            if (!isMenuMoving)
                return;
            transform.Translate(dirVec * Time.deltaTime * menuCameraSpeed, Space.World);
        }
    }

    public void SetFollowPlayer(bool value)
    {
        isGameProcessing = value;
        if (isGameProcessing)
        {
            player = inGame.currPlayer.gameObject;
            playerPrevPos = player.transform.position;
            isFirstTime = true;
        }
    }

    public void MoveCamera(bool value)
    {
        isMenuMoving = value;
    }

}
