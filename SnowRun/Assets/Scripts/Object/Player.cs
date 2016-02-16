using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, System.IComparable<Player>
{
    public int rarity;


    void Awake()
    {

    }

    void Update()
    {

    }



    public int CompareTo(Player obj)
    {
        if (obj.rarity > this.rarity)
            return -1;
        else if (obj.rarity < this.rarity)
            return 1;
        return 0;
    }
}