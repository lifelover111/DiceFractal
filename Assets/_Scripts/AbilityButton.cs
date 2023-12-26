using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Ability ability;
    Image image;
    TMP_Text cost;
    Button button;
    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        cost = GetComponentInChildren<TMP_Text>();
        button.onClick.AddListener(() => { GameInfo.instance.gameObject.SetActive(false); });
    }

    public void SetAbility(Ability ability)
    {
        this.ability = ability;
        image.sprite = ability.abilityIcon;


        int costInt = int.Parse(ability.GetStringCost());
        cost.text = GetDiceSymbol(costInt);
    }


    public static string GetDiceSymbol(int count)
    {
        switch (count)
        {
            case 1:
                return "●";
            case 2:
                return "● ●";
            case 3:
                return "● ● ●";
            case 4:
                return "● ● \n● ●";
            case 5:
                return "● ●\n● ● ●";
            case 6:
                return "● ● ●\n● ● ●";
            default:
                return "";
        }
    }



    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if(GameInfo.instance.transform.gameObject.activeInHierarchy)
    //        GameInfo.instance.StartCoroutine(GameInfo.instance.Close());
    //}


    public void OnPointerEnter(PointerEventData eventData)
    {
  
        GameInfo.instance.gameObject.SetActive(true);
        GameInfo.instance.SetInfo(transform.position + 100 * Vector3.right, ability.name, ability.GetDescription());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameInfo.instance.gameObject.activeInHierarchy)
        {
            GameInfo.instance.Close(); 
            GameInfo.instance.gameObject.SetActive(false);
        }
    }

}
