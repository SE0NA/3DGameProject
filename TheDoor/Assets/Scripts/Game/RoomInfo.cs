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
    public bool hasFlag = false;

    public Light lightObject;

    StageInfo _stageInfo;

    private void Start()
    {
        _stageInfo = FindObjectOfType<StageInfo>();

        lightObject.enabled = false;
        lightObject.color = Color.white;
        if (roomNum == 13) lightObject.enabled = true;
    }

    public void Open()
    {
        isOpened = true;
        lightObject.enabled = true;
        if (hasBomb)
        {
            lightObject.color = Color.red;
        }
    }
    public void Flag()
    {
        if (!hasFlag)   // 플래그 표시하기
        {
            hasFlag = true;
            lightObject.color = new Color(1f, 0.62f, 0f);
            lightObject.enabled = true;
            _stageInfo.BombCntDown();
        }
        else            // 플래그 취소하기
        {
            hasFlag = false;
            lightObject.color = Color.white;
            lightObject.enabled = false;
            _stageInfo.BombCntUp();
        }
    }
}
