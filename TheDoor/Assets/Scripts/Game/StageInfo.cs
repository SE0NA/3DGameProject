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
    [SerializeField] int bombCnt; // 폭탄 수

    public int startRoomNum;    // 게임 시작시 플레이어의 위치
    public int stageLine;       // 스테이지의 크기

    CanvasManager _canvasManager;

    public int currentBombCnt;

    private void Start()
    {
        _canvasManager = FindObjectOfType<CanvasManager>();

        currentBombCnt = bombCnt;
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
}
