using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public int coin
    {
        get;
        private set;
    }

    void Awake()
    {
        SceneFader.SomeMethod();

    }

}