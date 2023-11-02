using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBall : MonoBehaviour
{
    Rigidbody rb;
    public float tumple = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumple ;
    }
}
