using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageLevel
{
    stage5x5, stage7x7, stage10x10
}

public class StageInfo : MonoBehaviour
{
    public StageLevel currentStage;

    public AudioClip doorOpenClip;

    public Vector3 startPosition;
    
}
