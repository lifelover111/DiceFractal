using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBoxKeeper : MonoBehaviour
{
    public Bounds bounds;
    
    void Update()
    {
        if (!bounds.Contains(transform.position))
        {
            transform.position = bounds.ClosestPoint(transform.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Dice"))
            return;
        collision.rigidbody.AddForce((collision.transform.position - transform.position).normalized * 2, ForceMode.Impulse);
    }
}
