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
    public Transform roomPos;

    StageInfo _stageInfo;
    MiniMapManager _minimapManager;
    MapManager _mapManager;
    private void Start()
    {
        _stageInfo = FindObjectOfType<StageInfo>();
        _minimapManager = FindObjectOfType<MiniMapManager>();
        _mapManager = FindObjectOfType<MapManager>();

        lightObject.enabled = false;
        lightObject.color = Color.white;
        if (roomNum == 13) lightObject.enabled = true;
    }

    public void Open()
    {
        isOpened = true;
        lightObject.enabled = true;
        _minimapManager.ChangeRoomPanelState(roomNum - 1, 0, false);
        _mapManager.ChangeStateMapButton(roomNum - 1, 0);
        if (hasBomb)
        {
            lightObject.color = Color.red;
            _minimapManager.ChangeRoomPanelState(roomNum - 1, 0, true);
        }
        else if (hasFlag)
        {
            hasFlag = false;
            lightObject.color = Color.white;
            _stageInfo.BombCntUp();
        }
    }
    public void Flag()
    {
        if (!hasFlag)   // 플래그 표시하기
        {
            hasFlag = true;
            lightObject.color = new Color(1f, 0.62f, 0f);
            lightObject.enabled = true;
            _minimapManager.ChangeRoomPanelState(roomNum - 1, 1, false);
            _mapManager.ChangeStateMapButton(roomNum - 1, 1);
            _stageInfo.BombCntDown();
        }
        else            // 플래그 취소하기
        {
            hasFlag = false;
            lightObject.color = Color.white;
            lightObject.enabled = false;
            _minimapManager.ChangeRoomPanelState(roomNum - 1, 2, false);
            _mapManager.ChangeStateMapButton(roomNum - 1, 2);
            _stageInfo.BombCntUp();
        }
    }
}
