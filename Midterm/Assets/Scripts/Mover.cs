using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 6;
    // Update
    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
