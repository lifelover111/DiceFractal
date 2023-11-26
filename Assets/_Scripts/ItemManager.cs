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

    private void Awake()
    {
        instance = this;
    }
}
