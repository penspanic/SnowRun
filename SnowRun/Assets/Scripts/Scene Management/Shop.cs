using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour, IScene
{

    public Text characterNameText;
    public Text selectedNumberText;
    public Text coinText;
    public Button goButton;


    public Vector2 originalPos
    {
        get
        {
            return new Vector2(720, 0);
        }
    }
    public GameObject sceneObject
    {
        get
        {
            return this.gameObject;
        }
    }

    UIManager uiMgr;
    GameManager gameMgr;

    List<Player> prefabList;
    List<GameObject> characterList = new List<GameObject>();

    GameObject objectsParent;

    Color[][] originalColors;

    int selectedIndex = 0;
    void Awake()
    {
        uiMgr = GameObject.FindObjectOfType<UIManager>();
        gameMgr = GameObject.FindObjectOfType<GameManager>();
        objectsParent = GameObject.Find("Objects Parent");

        prefabList = new List<Player>(Resources.LoadAll<Player>("Object/Character"));
        prefabList.Sort(); // Sort by Player's IComparable implement

        originalColors = new Color[prefabList.Count][];

        int i = 0;
        Debug.Log(prefabList.Count);
        foreach(Player eachPrefab in prefabList)
        {
            Vector3 createPos = Vector3.zero + Vector3.right * i;
            GameObject newCharacter = Instantiate(eachPrefab.gameObject, createPos, eachPrefab.transform.rotation) as GameObject;

            newCharacter.transform.SetParent(objectsParent.transform, false);
            Destroy(newCharacter.GetComponent<Player>());
            Destroy(newCharacter.GetComponent<Rigidbody>());

            Renderer[] renderers = newCharacter.GetComponentsInChildren<Renderer>();

            originalColors[i] = new Color[renderers.Length];
            for (int j = 0; j < renderers.Length; j++)
                originalColors[i][j] = renderers[j].material.color;
            characterList.Add(newCharacter);
            i++;
        }
    }

    #region Events

    public void OnGoButtonDown()
    {
        uiMgr.SetGameState(GameState.InGame);
    }

    public void OnLeftButtonDown()
    {
        MoveCharacters(false);
    }

    public void OnRightButtonDown()
    {
        MoveCharacters(true);
    }

    public void Enter()
    {

    }

    public void Leave()
    {

    }

    public void EscapeProcess()
    {
        uiMgr.SetGameState(GameState.Main);
    }

    #endregion

    void SetCharacters()
    {

    }

    bool isCharacterMoving = false;

    void MoveCharacters(bool moveRight)
    {
        if (isCharacterMoving)
            return;
        if(moveRight)
        {
            if (selectedIndex + 1 >= characterList.Count)
                return;
            selectedIndex++;
        }
        else
        {
            if (selectedIndex - 1 < 0)
                return;
            selectedIndex--;
        }
        //float cx = objectsParent.transform.localPosition.x;
        //float cy = objectsParent.transform.localPosition.y;
        //float rad = 1000 / 2;

        foreach(var character in characterList)
        {
            Vector3 endPos = character.transform.localPosition;
            endPos.x -= moveRight ? 1 : -1;
            StartCoroutine(MoveCharacter(character, endPos));
        }
    }

    IEnumerator MoveCharacter(GameObject target, Vector3 endPos)
    {
        float elapsedTime = 0f;
        float moveTime = 0.5f;
        isCharacterMoving = true;
        Vector3 startPos = target.transform.localPosition;
        while(elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            target.transform.localPosition = EasingUtil.EaseVector3(EasingUtil.easeInOutBack, startPos, endPos, elapsedTime / moveTime);
            yield return null;
        }
        isCharacterMoving = false;
    }

    Vector3 GetCharacterSetPosition(int index)
    {
        return Vector3.zero;
    }

    void OnMoveEnd()
    {
        isCharacterMoving = false;
    }

    void SetCharacterColor(GameObject character)
    {

    }
}