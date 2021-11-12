using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageLevel
{
    stage5x5, stage7x7, stage9x9
}

public class StageInfo : MonoBehaviour
{
    public StageLevel currentStage;

    public RoomInfo[] roomList;
    // 배열 인덱스 + 1 = 실제 방번호
    [SerializeField] int bombCnt = 0; // 폭탄 수

    public int startRoomNum;    // 게임 시작시 플레이어의 위치
    public int stageLine;       // 스테이지의 크기

    CanvasManager _canvasManager;

    public int currentBombCnt;
    public int checkedRoomCnt;

    private void Start()
    {
        _canvasManager = FindObjectOfType<CanvasManager>();

        currentBombCnt = bombCnt;
        checkedRoomCnt = 0;
        _canvasManager.SetBombCnt(currentBombCnt);
    }

    public int GetBombCnt()
    {
        return bombCnt;
    }
    public void BombCntUp()
    {
        currentBombCnt++;
        _canvasManager.SetBombCnt(currentBombCnt);
    }
    public void BombCntDown()
    {
        currentBombCnt--;
        _canvasManager.SetBombCnt(currentBombCnt);
    }

    public bool CheckAllRoom()
    {
        // 모든 방이 체크됨 > 게임 클리어
        if (checkedRoomCnt == roomList.Length)
        {
            return true;
        }

        // (접근 불가) 남은 방수 == 폭탄수 -> 폭탄이 있는 곳만 열리지 않음
        // > 게임 클리어
        int remainRoom = 0;

        for(int i = 0; i < roomList.Length; i++)
        {
            if (!roomList[i].isOpened)
                remainRoom++;
        }
        if(remainRoom == bombCnt)
        {
            return true;
        }
        return false;
    }
}
