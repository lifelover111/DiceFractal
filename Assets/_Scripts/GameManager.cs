using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject dicePrefab;
    public Vector3 dicesPosition = new Vector3(0, -1000, 0);
    [SerializeField] GameObject blackScreen;
    [SerializeField] Transform forest;
    [SerializeField] Transform loseScreen;
    [SerializeField] public RectTransform canvasRectTransform;
    [SerializeField] public GameObject damageEffectPrefab;
    [SerializeField] public GameObject inventoryItemPrefab;

    [SerializeField] Transform[] abilityEndTurnButtons;
    [SerializeField] Transform[] equippedItemSlots;
    Character[] characters = new Character[3];

    [SerializeField] Item[] additionalStartItems;

    public int battlesWon = 0;
    public int enemiesKilled = 0;
    public int damageDealed = 0;
    public int damageTaken = 0;
    public int healthRestored = 0;

    public AudioSource mainSound;

    DifficultyProgression progression;

    private void Awake()
    {
        instance = this;
        Physics.gravity = new Vector3(0, 0, 9.8f);
        StaticBatchingUtility.Combine(forest.gameObject);
        forest.position = new Vector3(Random.Range(-42, 2), 3.2f, 0);
        SetCharacters();
        PrepareCharacters();
        foreach (var c in characters)
            c.OnCharacterDied += () => {
                if (characters.Where(c => !c.isDead).Count() == 0)
                    LoseGame();
            };
    }
    private void Start()
    {
        foreach (Item item in additionalStartItems)
        {
            Inventory.instance.AddItem(item.Copy());
        }
        progression = new DifficultyProgression();
        Fight fight = new Fight(characters, progression);
        fight.OnFightEnd += progression.IncrementFightNumber;
        fight.OnFightEnd += () => { battlesWon++; };
        fight.OnFightEnd += NewFight;
        fight.StartFight();
    }

    void SetCharacters()
    {
        GameObject party = new GameObject("Party");
        for(int i = 0; i < characters.Length; i++) 
        {
            characters[2 - i] = Instantiate(PartyKeeper.instance.GetParty()[i]).GetComponent<Character>();
            characters[2 - i].abilityTurnButton = abilityEndTurnButtons[2 - i];
            characters[2 - i].transform.SetParent(party.transform, true);

            Button button = characters[2 - i].abilityTurnButton.GetComponent<Button>();
            button.interactable = false;
            button.onClick.AddListener(() => { button.interactable = false; });

            characters[2 - i].equippedItemSlot = equippedItemSlots[2 - i];
        }
        PartyKeeper.instance.Destroy();
    }

    void PrepareCharacters()
    {
        foreach (Character character in characters)
        {
            character.OnAbilityUse += () =>
            {
                foreach (Character c in characters)
                {
                    c.abilityTurnButton.GetComponent<Button>().interactable = false;  //.gameObject.SetActive(false);
                }
            };
        }
    }
    void NewFight()
    {
        StartCoroutine(NewFightCoroutine());
    }

    IEnumerator NewFightCoroutine()
    {
        foreach(Character character in characters)
        {
            character.GoForward();
        }
        blackScreen.gameObject.SetActive(true);
        SpriteRenderer sRend = blackScreen.GetComponent<SpriteRenderer>();
        float p = 0;
        while(sRend.color.a != 1)
        {
            Color col = sRend.color;
            col.a = Mathf.Lerp(0, 1, p);
            sRend.color = col;
            p += Time.deltaTime/2;
            foreach (var c in characters)
            {
                if(!c.isDead)
                    c.gameObject.transform.position += 5*Time.deltaTime * Vector3.right;
            }
            yield return null;
        }
        foreach (var c in characters)
        {
            c.SetIdle();
        }
        DiceThrow.DestroyAllDicesDelegate.Invoke();
        forest.position = new Vector3(Random.Range(-42, 2), 3.2f, 0);
        Fight fight = new Fight(characters, progression);

        fight.OnFightEnd += progression.IncrementFightNumber;
        fight.OnFightEnd += () => { battlesWon++; };
        fight.OnFightEnd += NewFight;

        p = 0;
        while (sRend.color.a != 0)
        {
            Color col = sRend.color;
            col.a = Mathf.Lerp(1, 0, p);
            sRend.color = col;
            p += Time.deltaTime/2;
            yield return null;
        }
        blackScreen.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        fight.StartFight();
    }

    void LoseGame()
    {
        loseScreen.gameObject.SetActive(true);
        Transform statistics = loseScreen.GetChild(1);
        statistics.GetChild(0).GetComponent<TMP_Text>().text += battlesWon;
        statistics.GetChild(1).GetComponent<TMP_Text>().text += enemiesKilled;
        statistics.GetChild(2).GetComponent<TMP_Text>().text += damageDealed;
        statistics.GetChild(3).GetComponent<TMP_Text>().text += damageTaken;
        statistics.GetChild(4).GetComponent<TMP_Text>().text += healthRestored;
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
