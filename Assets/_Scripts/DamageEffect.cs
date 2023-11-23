using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    RectTransform canvasRectTransform;
    TMP_Text text;
    Color deltaColor;
    private void Awake()
    {
        canvasRectTransform = GameManager.instance.canvasRectTransform;
        text = GetComponent<TMP_Text>();
        deltaColor = new Color(0, 0, 0, 1f);
    }
    private void Update()
    {
        transform.position += Vector3.up * 30*Time.deltaTime;
        text.color -= deltaColor*Time.deltaTime;
        if (text.color.a < 0.05f)
            Destroy(gameObject);
    }
    public void SetDamageEffect(float value, Vector3 pos)
    {
        if(value > 0)
        {
            text.color = Color.red;
            text.text = '-' + value.ToString();
        }
        else
        {
            text.color = Color.green;
            text.text = '+' + (-value).ToString();
        }
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Camera.main.WorldToScreenPoint(pos + 0.5f * Vector3.right + 2f * Vector3.up), null, out canvasPos);
        canvasPos += (Vector2)canvasRectTransform.transform.position;
        transform.position = canvasPos;
    }
}
