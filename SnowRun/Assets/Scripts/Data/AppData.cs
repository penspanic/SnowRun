using UnityEngine;
using System.Collections.Generic;

struct CharacterData
{
    public CharacterData(string name, int rarityValue,bool isCharacterHave = false)
    {
        this.name = name;
        this.rarityValue = rarityValue;
        this.isCharacterHave = isCharacterHave;
    }
    public string name;
    public int rarityValue;
    public bool isCharacterHave;
}
static class AppData
{
    public static List<CharacterData> charactersList;

    static AppData()
    {
        Init();
    }
    public static void Init()
    {
        charactersList = new List<CharacterData>()
        {
            new CharacterData("Snowman",0,true),
            new CharacterData("Robot",1,true),
            new CharacterData("Snow Ball",2,true), 
            //new CharacterData("Cow Boy",3),
            //new CharacterData("Summer Snowman",4),
            //new CharacterData("Ninja Snowman",5),
            new CharacterData("Penguin",7,true),
            new CharacterData("Carrot Snowman",8,true),
            new CharacterData("Bing Su",9,true),
            new CharacterData("Gentleman",9,true),

        };
    }
    public static void SetData(List<string> dataList)
    {

    }
    public static CharacterData[] GetCharactersData()
    {
        return charactersList.ToArray();
    }
    public static int GetRarity(string name)
    {
        foreach (CharacterData eachData in charactersList)
        {
            if (eachData.name == name)
                return eachData.rarityValue;
        }
        throw new UnityException("There is no Character! : " + name);
    }
    public static bool IsCharacterHave(string name)
    {
        foreach(CharacterData eachData in charactersList)
        {
            if(eachData.name == name)
            {
                if (eachData.isCharacterHave)
                    return true;
            }
        }
        return false;
    }
    public static CharacterData[] GetNotHaveCharacters()
    {
        List<CharacterData> returnList = new List<CharacterData>();

        for(int i = 0;i<charactersList.Count;i++)
        {
            if (!charactersList[i].isCharacterHave)
                returnList.Add(charactersList[i]);
        }
        return returnList.ToArray();
    }
    public static void CharacterGet(string name)
    {
        for(int i = 0;i<charactersList.Count;i++)
        {
            if(charactersList[i].name == name)
            {
                CharacterData newData = charactersList[i];
                newData.isCharacterHave = true;
                charactersList[i] = newData;
            }
        }
    }
}