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
    public RoomType roomtype;

    public int aroundBomb = 0;
    public int roomNum;
    public bool isOpened = false;
    public bool hasBomb = false;

    public GameObject lightObject;

    private void Start()
    {
        lightObject.SetActive(false);
    }

    public void LightOn()
    {
        lightObject.SetActive(true);
    }
}
