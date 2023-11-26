using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public Item cheese;
    public Item apple;
    public Item commonHeal;
    public Item[] weapons;
    public Item[] disposableItems;

    private void Awake()
    {
        instance = this;
    }
}
