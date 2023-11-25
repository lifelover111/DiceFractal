using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static GameInfo instance;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void SetInfo(Vector2 position, string title = "", string description = "")
    {
        this.title.text = title;
        this.description.text = description;
        transform.position = position;
    }

    public IEnumerator Close()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
