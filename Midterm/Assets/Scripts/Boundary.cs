using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    GameController gc;
    AudioSource audioSource;
    public AudioClip soundLose;
    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        if (other.gameObject.layer == 6)
        {
            gc.setGameOver(true);
            audioSource.PlayOneShot(soundLose);
        }
       
    }
}
