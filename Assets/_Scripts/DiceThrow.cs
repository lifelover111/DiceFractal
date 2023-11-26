using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceThrow : MonoBehaviour
{
    [SerializeField] Transform[] sides;
    private Vector3Int DirectionValues = new Vector3Int(4, 2, 1);
    private Vector3Int OpposingDirectionValues;
    private int[] FaceRepresent = new int[] { 1, 2, 3, 4, 5, 6 };
    public Rigidbody diceRigidbody;
    public float force = 10f;
    public Vector3 tossDirection = Vector3.up;
    public float tossForce = 10f;
    private KeyValuePair<int, Transform> currentValue;
    public event System.Action OnValueGot = () => { };
    public static System.Action DestroyAllDicesDelegate = () => { };

    public AudioSource diceThrowAudio;

    void Start()
    {
        DestroyAllDicesDelegate += DestroyThis;
        OpposingDirectionValues = 6 * Vector3Int.one - DirectionValues;
    }
    private void OnDestroy()
    {
        DestroyAllDicesDelegate -= DestroyThis;
    }


    void OnCollisionEnter(Collision collision)
    {
        // ѕровер€ем, что столкновение произошло с кубиком
        if (collision.gameObject)
        {
            // ¬оспроизводим звук удара кости
            if (diceThrowAudio != null)
            {
                diceThrowAudio.Play();
            }
        }
    }

    public void ThrowDice()
    {

        diceRigidbody.velocity = Vector3.zero;
        diceRigidbody.angularVelocity = Vector3.zero;

        diceRigidbody.AddForce(tossDirection * (tossForce - 2*Random.value), ForceMode.Impulse);
        diceRigidbody.AddTorque(Vector3.forward * Random.Range(1, force), ForceMode.Impulse);
        diceRigidbody.AddTorque(Random.insideUnitSphere * force, ForceMode.Impulse);
        diceRigidbody.AddTorque(Random.insideUnitSphere * force * 1.5f, ForceMode.Impulse);
        //diceRigidbody.AddForce(Random.onUnitSphere * force, ForceMode.Impulse);
        //diceThrowAudio.Play();
        StartCoroutine(GetUpperSideNumberCoroutine());
    }


    IEnumerator GetUpperSideNumberCoroutine()
    {
        yield return new WaitWhile(() => diceRigidbody.IsSleeping());

        while (!diceRigidbody.IsSleeping())
        {
            yield return null; 
        }

        var sideNum = DetermineNumber();
        currentValue = sideNum;
        OnValueGot?.Invoke();
    }

    public KeyValuePair<int, Transform> DetermineNumber()
    {
        OpposingDirectionValues = 6 * Vector3Int.one - DirectionValues;
        if (Vector3.Cross(Vector3.back, transform.right).magnitude < 0.5f)

        {
            if (Vector3.Dot(Vector3.back, transform.right) > 0)
            {
                return new KeyValuePair<int, Transform>(FaceRepresent[DirectionValues.x - 1], sides[DirectionValues.x - 1]);

            }
            else
            {
                return new KeyValuePair<int, Transform>(FaceRepresent[OpposingDirectionValues.x], sides[OpposingDirectionValues.x]);
            }
        }
        else if (Vector3.Cross(Vector3.back, transform.up).magnitude < 0.5f)
        {
            if (Vector3.Dot(Vector3.back, transform.up) > 0)
            {
                return new KeyValuePair<int, Transform>(FaceRepresent[DirectionValues.y - 1], sides[DirectionValues.y - 1]);
            }
            else
            {
                return new KeyValuePair<int, Transform>(FaceRepresent[OpposingDirectionValues.y], sides[OpposingDirectionValues.y]);
            }
        }
        else if (Vector3.Cross(Vector3.back, transform.forward).magnitude < 0.5f)
        {
            if (Vector3.Dot(Vector3.back, transform.forward) > 0)
            {
                return new KeyValuePair<int, Transform>(FaceRepresent[DirectionValues.z - 1], sides[DirectionValues.z - 1]);
            }
            else
            {
                return new KeyValuePair<int, Transform>(FaceRepresent[OpposingDirectionValues.z], sides[OpposingDirectionValues.z]);
            }
        }
        else
        {
            Debug.Log(transform.rotation);
            transform.rotation = Quaternion.Euler((Mathf.RoundToInt(transform.rotation.x)/90)*90, (Mathf.RoundToInt(transform.rotation.y)/90)*90, (Mathf.RoundToInt(transform.rotation.z)/90)*90);
            Debug.Log(transform.rotation);
            //return new KeyValuePair<int, Transform>(0, null);
            return DetermineNumber();
        }
    }


    public KeyValuePair<int, Transform> GetValue()
    {
        return currentValue;
    }


    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
    
