using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DiceThrow diceThrow;

    void Start()
    {

        if (diceThrow == null)
        {
            Debug.LogError("DiceThrow component is not assigned!");
        }
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            diceThrow.ThrowDice();
        }
    }

}
