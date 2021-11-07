using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public StageLevel minimapSize;
    public GameObject playerPointer;
    public GameObject minimapCam;

    public GameObject roomPanelList;

    PlayerController _playerController;
    StageInfo _stageInfo;

    float playerPointerY;
    float cameraY;
    
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _stageInfo = FindObjectOfType<StageInfo>();
        playerPointerY = playerPointer.transform.position.y;
        cameraY = minimapCam.transform.position.y;

        for (int i = 0; i < roomPanelList.transform.childCount; i++)
            roomPanelList.transform.GetChild(i).GetComponent<Renderer>().materials[0].color = Color.black;
        roomPanelList.transform.GetChild(_stageInfo.startRoomNum - 1).GetComponent<Renderer>().materials[0].color = Color.white;

    }
    
    void Update()
    {
        // 미니맵의 플레이어의 위치와 카메라 위치를 실시간으로 변경
        playerPointer.transform.position = new Vector3(_playerController.transform.position.x
                                                      , playerPointerY
                                                      , _playerController.transform.position.z);
        minimapCam.transform.position = new Vector3(_playerController.transform.position.x
                                                      , minimapCam.transform.position.y
                                                      , _playerController.transform.position.z);
    }

    public void ChangeRoomPanelState(int roomNum, int changeTo, bool hasBomb)
    {
        if (changeTo == 0)      // 문 열기
        {
            if (hasBomb)
                roomPanelList.transform.GetChild(roomNum).GetComponent<Renderer>().material.color = Color.red;
            else
                roomPanelList.transform.GetChild(roomNum).GetComponent<Renderer>().material.color = Color.white;
        }
        else if(changeTo==1)    // 플래그
        {
            roomPanelList.transform.GetChild(roomNum).GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if(changeTo==2)    // None
        {
            roomPanelList.transform.GetChild(roomNum).GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
