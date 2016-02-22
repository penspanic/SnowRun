using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public int coin
    {
        get;
        private set;
    }

    List<string> ownCharacterList = new List<string>();
    
    void Awake()
    {
        SceneFader.SomeMethod();
        OwnCharacterLoad();
    }
    
    void OwnCharacterLoad()
    {
        GameObject[] characters = Resources.LoadAll<GameObject>("Object/Character");

        for(int i = 0;i<characters.Length;i++)
        {
            if(PlayerPrefs.HasKey(characters[i].name))
                ownCharacterList.Add(characters[i].name);
        }
    }

    public bool HasOwn(string characterName)
    {
        return ownCharacterList.Contains(characterName);
    }

    public void BuyCharacter(int cost)
    {
        coin -= cost;
    }
}