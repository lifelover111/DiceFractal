using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject dicePrefab;
    public GameObject[] enemyPrafabs;
    public Vector3 dicesPosition = new Vector3(0, -1000, 0);

    [SerializeField] Character[] charactersTest;

    private void Awake()
    {
        instance = this;
        Physics.gravity = new Vector3(0, 0, -Physics.gravity.y);
        PrepareCharacters();
    }
    private void Start()
    {
        Fight fight = new Fight(charactersTest);
    }
    void PrepareCharacters()
    {
        foreach (Character character in charactersTest)
        {
            character.OnAbilityUse += () =>
            {
                foreach (Character c in charactersTest)
                {
                    c.abilityTurnButton.gameObject.SetActive(false);
                }
            };
        }
    }
}
