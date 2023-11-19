using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceThrow : MonoBehaviour
{
    public Vector3Int DirectionValues;
    private Vector3Int OpposingDirectionValues;
    private int[] FaceRepresent = new int[] { 1, 2, 3, 4, 5, 6 };
    public Rigidbody diceRigidbody;
    public float force = 10f;

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

            diceRigidbody.AddForce(Random.onUnitSphere * force, ForceMode.Impulse);
            StartCoroutine(GetUpperSideNumberCoroutine());
        }
        else
        {
            Debug.LogError("Rigidbody component is missing on the dice object.");
        }
    }

    IEnumerator GetUpperSideNumberCoroutine()
    {
       
        // пока не придумал
        yield return new WaitForSeconds(3);

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
    
