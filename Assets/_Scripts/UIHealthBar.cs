using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] bool fade = true;
    [SerializeField] float fadeTime = 5;
    [SerializeField] TMP_Text text;

    IHealth target;
    GameObject healthLine;
    SpriteRenderer sRend;
    SpriteRenderer hsRend;
    float memorizedHealth;
    float memorizeTime;
    void Start()
    {
        target = transform.parent.GetComponent<IHealth>();
        healthLine = transform.GetChild(0).gameObject;
        sRend = GetComponent<SpriteRenderer>();
        hsRend = healthLine.GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (fade)
            Fade();
        healthLine.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0, 1, 1), 1 - target.health/target.maxHealth);
        healthLine.transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(-0.6f, 0, 0), 1 - target.health / target.maxHealth);
        text.text = target.health.ToString() + '/' + target.maxHealth.ToString();
    }

    void Fade()
    {
        if (Time.time - memorizeTime >= fadeTime)
        {
            sRend.color = Color.Lerp(sRend.color, new Color(sRend.color.r, sRend.color.g, sRend.color.b, 0), Time.deltaTime);
            hsRend.color = Color.Lerp(hsRend.color, new Color(hsRend.color.r, hsRend.color.g, hsRend.color.b, 0), Time.deltaTime);
        }
        else
        {
            sRend.color = new Color(sRend.color.r, sRend.color.g, sRend.color.b, 1);
            hsRend.color = new Color(hsRend.color.r, hsRend.color.g, hsRend.color.b, 1);
        }
        if (memorizedHealth != target.health)
        {
            memorizedHealth = target.health;
            memorizeTime = Time.time;
        }
    }
}
