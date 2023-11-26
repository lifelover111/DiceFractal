using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacter : MonoBehaviour
{
    [SerializeField] GameObject characterPrefab;
    Transform glow;
    SpriteRenderer glowSpriteRend;
    bool chosen = false;
    bool mouseOn = false;

    private void Awake()
    {
        glow = transform.GetChild(0);
        glowSpriteRend = glow.GetComponent<SpriteRenderer>();
        MainMenu.instance.OnNewGameBack += () => {
            chosen = false;
            PartyKeeper.instance.PartyRemove(characterPrefab);
        };
    }

    private void Update()
    {
        glow.gameObject.SetActive(chosen || mouseOn);
    }

    private void OnMouseDown()
    {
        GameInfo.instance.gameObject.SetActive(false);
        if (PartyKeeper.instance.GetParty().Count >= 3 && !chosen)
            return;
        if (!chosen)
            ChooseThis();
        else
            UnchooseThis();
    }
    private void OnMouseEnter()
    {
        GameInfo.instance.gameObject.SetActive(true);
        Vector2 infoPosition = transform.position.x < 0 ? Camera.main.WorldToScreenPoint(transform.position) + 350*(Screen.width/1920) * Vector3.right : Camera.main.WorldToScreenPoint(transform.position) - 350 * (Screen.width / 1920) * Vector3.right;
        GameInfo.instance.SetInfo(infoPosition);

        if (chosen)
            return;
        mouseOn = true;
        Color color = glowSpriteRend.color;
        color.a = 0.2f;
        glowSpriteRend.color = color;
    }
    private void OnMouseExit()
    {
        //if(GameInfo.instance.gameObject.activeInHierarchy)
        //    GameInfo.instance.StartCoroutine(GameInfo.instance.Close());
        GameInfo.instance.gameObject.SetActive(false);
        mouseOn = false;
        Color color = glowSpriteRend.color;
        color.a = 0.5f;
        glowSpriteRend.color = color;
    }

    void ChooseThis()
    {
        Color color = glowSpriteRend.color;
        color.a = 0.5f;
        glowSpriteRend.color = color;
        chosen = !chosen;
        PartyKeeper.instance.PartyAdd(characterPrefab);
    }

    void UnchooseThis()
    {
        Color color = glowSpriteRend.color;
        color.a = 0.2f;
        glowSpriteRend.color = color;
        chosen = !chosen;
        mouseOn = true;
        PartyKeeper.instance.PartyRemove(characterPrefab);
    }
}
