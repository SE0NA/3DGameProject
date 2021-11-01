using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    CornerUL, CornerUR, CornerDL, CornerDR,
    SideU, SideD, SideL, SideR,
    Middle
}

public class RoomInfo : MonoBehaviour
{
    public int roomNum;
    public bool isOpen = false;
    public bool haveBomb = false;

    public RoomType roomtype;
    public int aroundBomb;

    public GameObject lightObject;

    private void Start()
    {
        lightObject.SetActive(false);
    }
}
