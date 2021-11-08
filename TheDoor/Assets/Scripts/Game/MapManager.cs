using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    StageInfo _stageInfo;
    PlayerController _playerController;

    private void Start()
    {
        _stageInfo = FindObjectOfType<StageInfo>();
        _playerController = FindObjectOfType<PlayerController>();
        
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
        }
        gameObject.transform.GetChild(_stageInfo.startRoomNum - 1).GetComponent<Image>().color = Color.white;

        gameObject.SetActive(false);
    }

    public void SetButtonColor()
    {
        gameObject.SetActive(true);
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (_stageInfo.roomList[i].isOpened)
                gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            else if (_stageInfo.roomList[i].hasFlag)
                gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.yellow;
            else
                gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
        }
    }

    public void ClickMoveRoomButton(int roomNum)
    {
        // 플레이어 이동
        _playerController.transform.position = _stageInfo.roomList[roomNum - 1].roomPos.position;
        _playerController.PlayerOpenDoor(roomNum - 1);
    }
}
