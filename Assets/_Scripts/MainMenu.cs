using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    [SerializeField] Transform root;
    [SerializeField] Transform newGameMenu;
    [SerializeField] Transform charactersMenu;
    [SerializeField] Transform startGameButton;

    public event System.Action OnNewGameBack = delegate { };

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PartyKeeper.instance.OnCharacterAdd += () =>
        {
            if (PartyKeeper.instance.GetParty().Count == 3)
                startGameButton.gameObject.SetActive(true);
        };
        PartyKeeper.instance.OnCharacterRemove += () =>
        {
            if(PartyKeeper.instance.GetParty().Count < 3)
                startGameButton.gameObject.SetActive(false);
        };
    }



    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    public void Back()
    {
        OnNewGameBack?.Invoke();
        charactersMenu.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(false);
        newGameMenu.gameObject.SetActive(false);
        root.gameObject.SetActive(true);
    }
    public void NewGame()
    {
        root.gameObject.SetActive(false);
        charactersMenu.gameObject.SetActive(true);
        newGameMenu.gameObject.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
