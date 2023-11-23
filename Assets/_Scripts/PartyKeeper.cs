using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyKeeper : MonoBehaviour
{
    public static PartyKeeper instance;
    List<GameObject> party = new List<GameObject>();
    public event System.Action OnCharacterAdd = delegate { };
    public event System.Action OnCharacterRemove = delegate { };
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public List<GameObject> GetParty()
    {
        return party;
    }

    public void PartyAdd(GameObject character)
    {
        if (party.Count <= 3)
        {
            party.Add(character);
            OnCharacterAdd?.Invoke();
        }
    }
    public void PartyRemove(GameObject character) 
    {
        if (party.Contains(character))
        {
            party.Remove(character);
            OnCharacterRemove?.Invoke();
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
