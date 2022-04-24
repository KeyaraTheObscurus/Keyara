using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayFootstepSound : MonoBehaviour
{
    private AudioSource audioSource01;
    // private AudioSource audioSource02;
    private bool IsMoving;
 
    void Start()
    {
        audioSource01 = gameObject.GetComponent<AudioSource>();
        // audioSource02 = gameObject.GetComponent<AudioSource>();
    }
 
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0) IsMoving = true; // better use != 0 here for both directions
        else IsMoving = false;
 
        if (IsMoving && !audioSource01.isPlaying) audioSource01.Play(); // if player is moving and audiosource is not playing play it
        if (!IsMoving) audioSource01.Stop(); // if player is not moving and audiosource is playing stop it
    }
}