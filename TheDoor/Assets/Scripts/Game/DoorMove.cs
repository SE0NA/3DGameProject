using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public bool isXaxis;

    public int roomNum1;
    public int roomNum2;

    AudioSource doorAudioSource;
    public AudioClip doorOpenClip;

    private void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }

    public void DoorOpen()
    {
        doorAudioSource.PlayOneShot(doorOpenClip);
        if (isXaxis)
        {
            ;
        }
    }
}
