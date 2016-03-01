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

    public bool IsOwn(string characterName)
    {
        foreach(string eachName in ownCharacterList)
        {
            if (characterName.Contains(eachName))
                return true;
        }
        return false;
    }

    public void GetCoin(int amount = 1)
    {
        coin += amount;
    }

    public void BuyCharacter(int cost)
    {
        coin -= cost;
    }
}