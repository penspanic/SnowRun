﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Player : MonoBehaviour
{

    public float moveSpeed = 0.5f;

    Rigidbody rgdBdy;

    float maxVelocity = 7f;
    float velocityIncreasement = 0.0003f;

    InGame inGame;

    void Awake()
    {
        rgdBdy = GetComponent<Rigidbody>();
        inGame = GameObject.FindObjectOfType<InGame>();
    }
    void Update()
    {
        if (isVelocityLimited == false)
        {
            if (rgdBdy.velocity.magnitude >= 3.0f && isEntered)
                isVelocityLimited = true;
        }
        Move();
    }

    Vector3 prevVelocity;

    bool isVelocityLimited = false;
    void FixedUpdate()
    {
        if (prevVelocity.magnitude + velocityIncreasement <= rgdBdy.velocity.magnitude)
        {
            if (isVelocityLimited == false)
            {
                prevVelocity = rgdBdy.velocity;
                return;
            }

            rgdBdy.velocity = prevVelocity + prevVelocity * velocityIncreasement;
        }

        rgdBdy.velocity = Vector3.ClampMagnitude(rgdBdy.velocity, maxVelocity);  // 최고 속도 10 넘지 않게

        prevVelocity = rgdBdy.velocity;
    }
    bool isEntered = false;

    Quaternion rotation;
    Vector3 rotateVec;
    Vector3 moveVec;

    // -1.3 ~ 1.3

    Vector3 prevPos;
    float yRotation = 0f;
    void Move()
    {

        transform.Translate(moveVec * Time.deltaTime * moveSpeed * rgdBdy.velocity.magnitude);

        float yAdditionalRotation;
        if (moveVec == Vector3.zero) // Control하고 있지 않을 때
        {
            RotationRevert();
        }
        else
        {
            //yAdditionalRotation = transform.position.x - prevPos.x * 10;
            //transform.rotation = Quaternion.Euler(-30, 0, 0);
            yAdditionalRotation = (moveVec == Vector3.left ? 1f : -1f) * Time.deltaTime * 40f;
            transform.Rotate(0, yAdditionalRotation, 0);
            yRotation += yAdditionalRotation;
            //Debug.Log("Y Rotation : " + yRotation);
        }

        prevPos = transform.position;
        //RotationRevert();
    }
    void RotationRevert()
    {

        transform.Rotate(0, -yRotation / 10, 0);
        yRotation *= 0.9f;

    }
    public void OnWaitEnd()
    {
        if (moveVec != Vector3.zero)
            return;
        moveVec = Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right;
    }
    public void OnTouchAreaEnter()
    {
        if (moveVec == Vector3.right)
            moveVec = Vector3.left;
        else
            moveVec = Vector3.right;
    }
    public void OnTouchAreaExit()
    {
        //moveVec = Vector3.zero;
    }

    // 속도 증가시 회전 속도도 같이 늘려야 함
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Ground")
            isEntered = true;
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log(other.gameObject.name);
            inGame.OnGameEnd();
        }
    }
    void OnTriggerEnter(Collider other) // 장애물 통과시 점수 처리
    {
        if(other.CompareTag("Coin"))
        {
            inGame.CoinGet();
            Transform parent = other.transform.parent;
            Destroy(parent.gameObject);
        }
        else if(other.CompareTag("Obstacle Point"))
        {
            inGame.SetScore(inGame.score + other.GetComponent<ObstaclePoint>().point);
            inGame.PointGet();
            Destroy(other.gameObject);
        }
    }      
}
