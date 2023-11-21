using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(5, 5, 5);
    public Vector3 targetPosition;
    Vector3 startPosition;
    float enableTime;
    public event System.Action OnZoom = () => { };

    private void OnEnable()
    {
        startPosition = transform.position;
        enableTime = Time.time;
        targetPosition -= new Vector3(0, 0, targetScale.z*0.7f);
    }
    private void Update()
    {
        transform.position = Vector3.Slerp(startPosition, targetPosition, Time.time - enableTime);
        transform.localScale = Vector3.Slerp(Vector3.one, targetScale, Time.time - enableTime);
        if((transform.position - targetPosition).magnitude < 0.5f && transform.localScale == targetScale)
        {
            OnZoom?.Invoke();
            Destroy(gameObject);
        }
    }
}
