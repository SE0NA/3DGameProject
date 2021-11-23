using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Main 씬과 Game 씬 사이에 정보 전달
 *  정보를 전달한 후 GameManager에서 파괴
 */

public class SendStageInfo : MonoBehaviour
{
    public StageLevel selectStage = StageLevel.stage5x5;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
