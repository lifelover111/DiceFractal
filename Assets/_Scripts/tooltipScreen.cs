using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tooltipScreen : MonoBehaviour
{
    [SerializeField]
    private Camera uiCamera;

    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    private static tooltipScreen instance;
    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("tooltipText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }

    private void SetText (string tooltipText)
    {
        gameObject.SetActive(true);
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(10, 10);

        backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.SetText(tooltipString);
    }

    public static void HideTooltip_static()
    {
        instance.HideTooltip();
    }
}
