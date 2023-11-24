using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabManager : MonoBehaviour
{
    public static EnemyPrefabManager instance;

    [SerializeField] public GameObject[] earlyEnemies;
    [SerializeField] public GameObject[] midEnemies;
    [SerializeField] public GameObject[] lateEnemies;

    private void Awake()
    {
        instance = this;
    }
}
