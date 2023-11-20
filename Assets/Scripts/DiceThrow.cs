using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceThrow : MonoBehaviour
{
    private Vector3Int DirectionValues = new Vector3Int(4, 2, 1);
    private Vector3Int OpposingDirectionValues;
    private int[] FaceRepresent = new int[] { 1, 2, 3, 4, 5, 6 };
    public Rigidbody diceRigidbody;
    public float force = 10f;
    public Vector3 tossDirection = Vector3.up;
    public float tossForce = 10f;

    void Start()
    {

        OpposingDirectionValues = 6 * Vector3Int.one - DirectionValues;
    }


    public void ThrowDice()
    {
        if (diceRigidbody != null)
        {

            diceRigidbody.velocity = Vector3.zero;
            diceRigidbody.angularVelocity = Vector3.zero;

            diceRigidbody.AddForce(tossDirection * tossForce, ForceMode.Impulse);
            diceRigidbody.AddTorque(Random.insideUnitSphere * force, ForceMode.Impulse);
            diceRigidbody.AddTorque(Random.insideUnitSphere * force * 1.5f, ForceMode.Impulse);
            //diceRigidbody.AddForce(Random.onUnitSphere * force, ForceMode.Impulse);
            StartCoroutine(GetUpperSideNumberCoroutine());
        }
        else
        {
            Debug.LogError("Rigidbody component is missing on the dice object.");
        }
    }

    IEnumerator GetUpperSideNumberCoroutine()
    {
        yield return new WaitWhile(() => diceRigidbody.angularVelocity == Vector3.zero);

        while (diceRigidbody.angularVelocity != Vector3.zero)
        {
            yield return null; 
        }

        int sideNum = DetermineNumber();
        Debug.Log(sideNum);
    }

    public int DetermineNumber()
    {
        OpposingDirectionValues = 6 * Vector3Int.one - DirectionValues;
        if (Vector3.Cross(Vector3.up, transform.right).magnitude < 0.5f) 
                                                                        
        {
            if (Vector3.Dot(Vector3.up, transform.right) > 0)
            {
                return FaceRepresent[DirectionValues.x - 1];
                
            }
            else
            {
                return FaceRepresent[OpposingDirectionValues.x];          
            }
        }
        else if (Vector3.Cross(Vector3.up, transform.up).magnitude < 0.5f) 
        {
            if (Vector3.Dot(Vector3.up, transform.up) > 0)
            {
                return FaceRepresent[DirectionValues.y - 1];
            }
            else
            {
                return FaceRepresent[OpposingDirectionValues.y];
            }
        }
        else if (Vector3.Cross(Vector3.up, transform.forward).magnitude < 0.5f) 
        {
            if (Vector3.Dot(Vector3.up, transform.forward) > 0)
            {
                return FaceRepresent[DirectionValues.z - 1];
            }
            else
            {
                return FaceRepresent[OpposingDirectionValues.z];
            }
        }
        return 0;
    }

}
    
