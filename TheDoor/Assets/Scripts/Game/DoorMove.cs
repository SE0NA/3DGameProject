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
    
    Animation dooropenAnim;
    
    private void Start()
    {
        dooropenAnim = GetComponent<Animation>();
    }

    public void DoorOpen()
    {
        leftDoor.GetComponent<AudioSource>().Play();
        rightDoor.GetComponent<AudioSource>().Play();
        dooropenAnim.Play();
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
