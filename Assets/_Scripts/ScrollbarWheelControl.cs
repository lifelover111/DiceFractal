using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarWheelControl : MonoBehaviour
{
    public Scrollbar scrollbar;
    public float scrollSpeed = 0.1f;

    void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if (scrollDelta != 0f)
        {
            float newValue = scrollbar.value + scrollDelta * scrollSpeed;

            newValue = Mathf.Clamp01(newValue);

            scrollbar.value = newValue;
        }
    }
}
