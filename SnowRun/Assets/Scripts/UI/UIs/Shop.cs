using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Shop : MonoBehaviour,IUserInterface
{
    List<GameObject> characterList = new List<GameObject>();
    List<GameObject> prefabList;

    Text selectedName;
    Text selectedNumber;
    Text money;
    Button goButton;

    GameManager gameMgr;
    UIManager   uiMgr;
    GameObject objectParent;
    Color[][] originalColors;

    int selectedIndex;
    bool isCharacterMoving = false;
    void Awake()
    {
        selectedName    = GameObject.Find("Character Name").GetComponent<Text>();
        selectedNumber  = GameObject.Find("Selected Number").GetComponent<Text>();
        money           = GameObject.Find("Money").GetComponent<Text>();
        goButton        = GameObject.Find("Go").GetComponent<Button>();
        gameMgr         = GameObject.FindObjectOfType<GameManager>();
        uiMgr           = GameObject.FindObjectOfType<UIManager>();
        objectParent    = GameObject.Find("Shop Objects");
        prefabList      = new List<GameObject>(Resources.LoadAll<GameObject>("Object/Character"));
        prefabList.Sort(SortCharacter);

        originalColors = new Color[prefabList.Count][];
        for(int i = 0;i<prefabList.Count;i++)
        {
            Vector3 createPos = Vector3.zero + Vector3.right * i;
            GameObject newCharacter = Instantiate(prefabList[i], createPos, prefabList[i].transform.rotation) as GameObject;
            newCharacter.transform.SetParent(objectParent.transform, false);

            Destroy(newCharacter.GetComponent<Player>());
            Destroy(newCharacter.GetComponent<Rigidbody>());

            Renderer[] renderers = newCharacter.GetComponentsInChildren<Renderer>();

            originalColors[i] = new Color[renderers.Length];
            for(int j = 0; j<renderers.Length;j++)
            {
                originalColors[i][j] = renderers[j].material.color;
            }
            characterList.Add(newCharacter);
        }
        selectedIndex = 0;
    }
    void Update()
    {
        RotateCharacters();
    }
    #region Events
    public void Enter()
    {
        Debug.Log("Shop Enter!");
        this.gameObject.SetActive(true);

        SetCharacterName();
        SetSelectedNumber();
        SetMoneyText();
        SetCharacterColor();
        SetGoButtonColor();


    }
    public void Leave()
    {
        Debug.Log("Shop Leave!");
        this.gameObject.SetActive(false);
    }
    public void OnGoButtonDown()
    {
        if (!AppData.IsCharacterHave(prefabList[selectedIndex].name))
            return;
        uiMgr.SetGameState(GameState.InGame);
        uiMgr.inGame.OnInGameProcess(prefabList[selectedIndex]);
    }
    public void OnLeftArrowDown()
    {
        MoveCharacters(false);
    }
    public void OnRightArrowDown()
    {
        MoveCharacters(true);
    }
    public void OnMovingEnd()
    {
        isCharacterMoving = false;
    }
    #endregion

    int SortCharacter(GameObject a, GameObject b)
    {
        string aName = a.name;
        string bName = b.name;

        int aRarity = AppData.GetRarity(aName);
        int bRarity = AppData.GetRarity(bName);

        if (aRarity > bRarity)
            return 1;
        else if (aRarity < bRarity)
            return -1;
        else
            return 0;
    }
    void SetCharacterName()
    {
        selectedName.text = prefabList[selectedIndex].name;
    }
    void SetSelectedNumber()
    {
        selectedNumber.text = (selectedIndex + 1).ToString() + "/" + prefabList.Count.ToString();
    }
    void SetMoneyText()
    {
        money.text = gameMgr.money.ToString();
    }
    void SetCharacterColor()
    {
        for(int i = 0;i<prefabList.Count;i++)
        {
            string name = prefabList[i].name;

            Renderer[] renderers = characterList[i].GetComponentsInChildren<Renderer>();
            if (AppData.IsCharacterHave(name))
                for (int j = 0; j < renderers.Length; j++)
                    renderers[j].material.color = originalColors[i][j];
            else
                for (int j = 0; j < renderers.Length; j++)
                    renderers[j].material.color = new Color(originalColors[i][j].r * 0.5f, originalColors[i][j].g * 0.5f, originalColors[i][j].b * 0.5f, 0.3f);
        }
    }
    void SetGoButtonColor()
    {
        if (AppData.IsCharacterHave(prefabList[selectedIndex].name))
            goButton.image.color = new Color(1, 1, 1, 1);
        else
            goButton.image.color = new Color(1, 1, 1, 0.5f);
    }
    void MoveCharacters(bool isMoveRight)
    {
        if (isCharacterMoving)
            return;
        if (isMoveRight)
        {
            if (selectedIndex + 1 >= characterList.Count)
                return;
            selectedIndex++;
            foreach (var character in characterList)
            {
                Vector3 endPos = character.transform.localPosition;
                endPos.x -= 1;
                StartCoroutine(CoroutineManager.LerpMove(
                    character, character.transform.localPosition, endPos, 3, true, true, this.gameObject, "OnMovingEnd"));
            }
        }
        else
        {
            if (selectedIndex - 1 < 0)
                return;
            selectedIndex--;
            foreach (var character in characterList)
            {
                Vector3 endPos = character.transform.localPosition;
                endPos.x += 1;
                StartCoroutine(CoroutineManager.LerpMove(
                    character, character.transform.localPosition, endPos, 3, true, true, this.gameObject, "OnMovingEnd"));
            }
        }
        isCharacterMoving = true;
        SetCharacterName();
        SetSelectedNumber();
        SetGoButtonColor();
    }
    void RotateCharacters()
    {
        for (int i = 0; i < characterList.Count; i++)
            characterList[i].transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 100);
    }
}