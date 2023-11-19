using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    int price;

    public bool CheckPrice(int value)
    {
        return price == value;
    }
}
