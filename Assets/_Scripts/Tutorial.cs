using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    public void Confirm()
    {
        if(toggle.isOn)
        {
            PlayerPrefs.SetInt("SkipTutorial", 1);
        }
        gameObject.SetActive(false);
    }
}
