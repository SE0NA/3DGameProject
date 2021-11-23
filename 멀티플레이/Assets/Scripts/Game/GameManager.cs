using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public StageLevel currentStageLevel;

    public List<GameObject> stagePrefabs = new List<GameObject>();
    public GameObject playerPrefab;

    StageInfo _stageInfo;
    Vector3 startPosition;

    public GameObject _doorInfoImage;

    private void Awake()
    {
        SendStageInfo _sendStageInfo = FindObjectOfType<SendStageInfo>();
        currentStageLevel = _sendStageInfo.selectStage;

        // 스테이지 생성
        GameObject gameStage = null;

        switch (currentStageLevel)
        {
            case StageLevel.stage5x5:
                gameStage = Instantiate(stagePrefabs[0]);
                gameStage.transform.position = new Vector3(0, 0, 0);
                startPosition = new Vector3(20.0f, 0.5f, -20.0f);
                break;

            case StageLevel.stage7x7:
                gameStage = Instantiate(stagePrefabs[1]);
                gameStage.transform.position = new Vector3(0, 0, 0);
                startPosition = new Vector3(33.0f, 0.5f, -33.0f);
                break;

            case StageLevel.stage9x9:
                gameStage = Instantiate(stagePrefabs[2]);
                gameStage.transform.position = new Vector3(0, 0, 0);
                startPosition = new Vector3(46.0f, 0.5f, -46.0f);
                break;
        }

        // 캐릭터 생성
        GameObject player = null;
        if(gameStage != null)   // 스테이지 생성시 실행
        {
            player = Instantiate(playerPrefab);
            player.transform.position = startPosition;
        }

        _stageInfo = FindObjectOfType<StageInfo>();
        _doorInfoImage.SetActive(false);
        SetBombInRoom();    // 폭탄 설치
    }

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
