using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    GameObject player;
    UIManager uiMgr;
    InGame inGame;

    public float menuMapSpeed;

    bool isGameProcessing;
    public bool isMenuMoving = true;

    public static Vector3 dirVec;

    Vector3 forwardVec;
    Vector3 playerPrevPos;
    void Awake()
    {
        uiMgr   = GameObject.FindObjectOfType<UIManager>();
     
        Quaternion mapRotation = Quaternion.Euler(-30, 0, 0);
        GameObject temp = new GameObject();
        dirVec = Vector3.one;
        temp.transform.rotation = mapRotation;
        dirVec = temp.transform.forward * -1;
        Destroy(temp.gameObject);

        
        forwardVec = transform.forward;
      
    }
    void Start()
    {
        inGame = uiMgr.inGame;
    }
    bool isFirstTime = true;
    void Update()
    {
        if (isGameProcessing)
        {
            if (isFirstTime)
            {
                //transform.position += new Vector3(0, player.transform.position.y, player.transform.position.z);
                playerPrevPos = new Vector3(0, player.transform.position.y, player.transform.position.z);
                isFirstTime = false;
            }
            Vector3 curPos = new Vector3(0, player.transform.position.y, player.transform.position.z);
            transform.position += curPos - playerPrevPos;
            playerPrevPos = curPos;
        }
        else
        {
            if (!isMenuMoving)
                return;
            transform.Translate(dirVec * Time.deltaTime * menuMapSpeed, Space.World);
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
            //this.transform.SetParent(player.transform);
        }
    }
    public void MoveCamera(bool value)
    {
        isMenuMoving = value;
    }
}
