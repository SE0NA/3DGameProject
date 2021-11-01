using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public StageLevel currentStageLevel;

    public List<GameObject> stagePrefabs = new List<GameObject>();
    public GameObject PlayerObject;

    StageInfo _stageInfo;

    private void Awake()
    {
        // 스테이지 생성
        GameObject gameStage = null;

        switch (currentStageLevel)
        {
            case StageLevel.stage5x5:
                gameStage = Instantiate(stagePrefabs[0], new Vector3(0, 0), Quaternion.identity);
                break;
        }

        _stageInfo = FindObjectOfType<StageInfo>(); // stagePrefabs에 포함

        // 캐릭터 생성
        /*
        if(gameStage != null)
        {

        }
        */
    }

}
