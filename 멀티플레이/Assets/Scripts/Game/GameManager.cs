using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    public StageLevel currentStageLevel;

    public List<GameObject> stagePrefabs = new List<GameObject>();
    public GameObject playerPrefab;

    StageInfo _stageInfo;
    Vector3 startPosition;

    private void Awake()
    {
        SendStageInfo _sendStageInfo = FindObjectOfType<SendStageInfo>();
        currentStageLevel = _sendStageInfo.selectStage;

        _stageInfo = FindObjectOfType<StageInfo>();

        // 캐릭터 생성
        GameObject player = null;
        player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        //   player = Instantiate(playerPrefab);
        player.transform.position = _stageInfo.roomList[_stageInfo.startRoomNum-1].roomPos.position;
        
        SetBombInRoom();    // 폭탄 설치
    }

    [PunRPC]
    void SetBombInRoom()
    {
        int currentBombCnt = 0;                 // 설치된 폭탄수
        
        // 폭탄 설치
        while (currentBombCnt < _stageInfo.GetBombCnt())
        {
            int randomNum = Random.Range(0, _stageInfo.roomList.Length);
            // 플레이어가 시작하는 방을 중심으로 사방은 폭탄 설치 불가
            // 이미 설치된 방은 설치 불가
            if (randomNum != _stageInfo.startRoomNum - 2 &&
                randomNum != _stageInfo.startRoomNum - 1 &&
                randomNum != _stageInfo.startRoomNum &&
                randomNum != _stageInfo.startRoomNum - 1 - _stageInfo.stageLine &&
                randomNum != _stageInfo.startRoomNum - 1 + _stageInfo.stageLine &&
                !_stageInfo.roomList[randomNum].hasBomb)
            {
                _stageInfo.roomList[randomNum].hasBomb = true;
                currentBombCnt++;
            }
        }
        
        // 모든 방의 RoomInfo.aroundBomb 입력
        // 문으로 연결된 방만 판단
        for(int i = 0; i < _stageInfo.roomList.Length; i++)
        {
            // RoomType으로 주변 방 위치 
            // Corner UL: 좌측 상단
            if (_stageInfo.roomList[i].roomtype == RoomType.CornerUL)
            {
                if (_stageInfo.roomList[i + 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i + _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
            }
            // Corner UR: 우측 상단
            else if (_stageInfo.roomList[i].roomtype == RoomType.CornerUR)
            {
                if (_stageInfo.roomList[i - 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i + _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
            }
            // Corner DL: 좌측 하단
            else if (_stageInfo.roomList[i].roomtype == RoomType.CornerDL)
            {
                if (_stageInfo.roomList[i + 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i - _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
            }
            // Corner DR: 우측 하단
            else if (_stageInfo.roomList[i].roomtype == RoomType.CornerDR)
            {
                if (_stageInfo.roomList[i - 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i - _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
            }
            // Side U: 윗면
            else if(_stageInfo.roomList[i].roomtype == RoomType.SideU)
            {
                if (_stageInfo.roomList[i - 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i + 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i + _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
            }
            // Side D: 아랫면
            else if (_stageInfo.roomList[i].roomtype == RoomType.SideD)
            {
                if (_stageInfo.roomList[i - 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i + 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i - _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
            }
            // Side L: 좌측면
            else if (_stageInfo.roomList[i].roomtype == RoomType.SideL)
            {
                if (_stageInfo.roomList[i - _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i + 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i + _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
            }
            // Side R: 우측면
            else if (_stageInfo.roomList[i].roomtype == RoomType.SideR)
            {
                if (_stageInfo.roomList[i - _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i - 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i + _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
            }
            // Center: 중앙 - 사방에 문이 있는 방
            else if (_stageInfo.roomList[i].roomtype == RoomType.Middle)
            {
                if (_stageInfo.roomList[i - 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i - _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i + 1].hasBomb) _stageInfo.roomList[i].aroundBomb++;
                if (_stageInfo.roomList[i + _stageInfo.stageLine].hasBomb) _stageInfo.roomList[i].aroundBomb++;
            }
        }
    }
}
