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

    #region IScene
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
    #endregion

    UIManager uiMgr;
    GameManager gameMgr;

    InGame inGame;

    List<Player> prefabList;
    List<GameObject> characterList = new List<GameObject>();

    GameObject objectsParent;
    Vector3 parentCenter;
    float characterCircleRad = 10f;

    Color[][] originalColors;

    bool isCharacterMoving = false;
    int selectedIndex = 0;
    void Awake()
    {
        uiMgr = GameObject.FindObjectOfType<UIManager>();
        gameMgr = GameObject.FindObjectOfType<GameManager>();
        inGame = GameObject.FindObjectOfType<InGame>();
        objectsParent = GameObject.Find("Objects Parent");
        parentCenter = objectsParent.transform.position;
        parentCenter.x = 0;

        prefabList = new List<Player>(Resources.LoadAll<Player>("Object/Character"));
        prefabList.Sort(); // Sort by Player's IComparable implement

        originalColors = new Color[prefabList.Count][];

        int i = 0;

        foreach (Player eachPrefab in prefabList)
        {
            Vector3 createPos = new Vector3();
            float angle = 20 * i * Mathf.Deg2Rad;
            createPos.x = parentCenter.x + Mathf.Sin(angle) * characterCircleRad;
            createPos.z = parentCenter.z - Mathf.Cos(angle) * characterCircleRad / 2;

            GameObject newCharacter = Instantiate(eachPrefab.gameObject);
            newCharacter.transform.localPosition = createPos;

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
        if (uiMgr.state != GameState.Shop)
            return;

        uiMgr.SetGameState(GameState.InGame);
        inGame.GameStart(prefabList[selectedIndex]);
    }

    public void OnRightButtonDown()
    {
        if (isCharacterMoving || selectedIndex >= characterList.Count - 1)
            return;
        float currAngle;
        float targetAngle;
        for (int i = 0; i < characterList.Count; i++)
        {
            currAngle = 10 * (i - selectedIndex) * Mathf.Deg2Rad;
            targetAngle = 10 * (i - selectedIndex - 1) * Mathf.Deg2Rad;
            StartCoroutine(MoveCharacter(characterList[i], currAngle, targetAngle));
        }
        selectedIndex++;

        if(gameMgr.IsOwn(characterList[selectedIndex].name))
           goButton.interactable = true;
        else
            goButton.interactable = false;
    }

    public void OnLeftButtonDown()
    {
        if (isCharacterMoving || selectedIndex == 0)
            return;
        float currAngle;
        float targetAngle;
        for (int i = 0; i < characterList.Count; i++)
        {
            currAngle = 10 * (i - selectedIndex) * Mathf.Deg2Rad;
            targetAngle = 10 * (i - selectedIndex + 1) * Mathf.Deg2Rad;
            StartCoroutine(MoveCharacter(characterList[i], currAngle, targetAngle));
        }
        selectedIndex--;

        if (gameMgr.IsOwn(characterList[selectedIndex].name))
            goButton.interactable = true;
        else
            goButton.interactable = false;
    }

    public void Enter()
    {
        SetCharacterColor();
    }

    public void Leave()
    {

    }

    public void EscapeProcess()
    {
        uiMgr.SetGameState(GameState.Main);

    }

    #endregion

    void SetCharacterColor()
    {
        Renderer[] renderers;
        int i = 0;
        foreach (GameObject eachCharacter in characterList)
        {
            renderers = eachCharacter.GetComponentsInChildren<Renderer>();
            if (gameMgr.IsOwn(eachCharacter.name))
            {
                for(int j = 0;j<renderers.Length;j++)
                {
                    renderers[j].material.color = originalColors[i][j];
                }
            }
            else
            {
                for (int j = 0; j < renderers.Length; j++)
                {
                    Color newColor = Color.black;
                    renderers[j].material.color = newColor;
                }
            }
            i++;
        }
    }

    IEnumerator MoveCharacter(GameObject target, float startAngle, float endAngle)
    {
        isCharacterMoving = true;

        float elapsedTime = 0f;
        float moveTime = 0.5f;
        Vector3 currPos = Vector3.zero;
        float currAngle = 0f;
        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            currAngle = EasingUtil.easeInOutBack(startAngle, endAngle, elapsedTime / moveTime);
            currPos.x = parentCenter.x + Mathf.Sin(currAngle) * characterCircleRad;
            currPos.z = parentCenter.z - Mathf.Cos(currAngle) * characterCircleRad / 2;
            target.transform.localPosition = currPos;

            yield return null;
        }
        currPos.x = parentCenter.x + Mathf.Sin(endAngle) * characterCircleRad;
        currPos.z = parentCenter.z - Mathf.Cos(endAngle) * characterCircleRad / 2;
        target.transform.localPosition = currPos;

        isCharacterMoving = false;
    }

    IEnumerator RotateCharacters()
    {
        while(true)
        {

            yield return null;
        }
    }

    Vector3 GetCharacterSetPosition(int index)
    {
        return Vector3.zero;
    }

    void OnMoveEnd()
    {
        isCharacterMoving = false;
    }
}