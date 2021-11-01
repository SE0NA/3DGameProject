using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public StageLevel currentStageLevel;

    public List<GameObject> stagePrefabs = new List<GameObject>();
    public GameObject playerPrefab;

    StageInfo _stageInfo;

    private void Awake()
    {
        // 스테이지 생성
        GameObject gameStage = null;

        switch (currentStageLevel)
        {
            case StageLevel.stage5x5:
                gameStage = Instantiate(stagePrefabs[0]);
                gameStage.transform.position = new Vector3(0, 0, 0);
                break;
        }

        _stageInfo = FindObjectOfType<StageInfo>(); // stagePrefabs에 포함

        // 캐릭터 생성
        GameObject player = null;
        if(gameStage != null)   // 스테이지 생성시 실행
        {
            player = Instantiate(playerPrefab);
            player.transform.localPosition = new Vector3(20.0f, 0.5f, -20.0f);
        }
    }

}
