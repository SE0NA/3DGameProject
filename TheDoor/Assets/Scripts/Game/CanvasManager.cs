using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Text bombCntText = null;
    [SerializeField] Text timerText = null;
    [SerializeField] Text scannerText = null;
    [SerializeField] GameObject deadPanel = null;
    [SerializeField] GameObject clearPanel = null;
    [SerializeField] Text clearText = null;

    [SerializeField] MapManager[] minimapUIList = null;
    MapManager minimapUI = null;

    private float sec = 0f;
    private int min = 0;

    public bool isEnd = false;

    private void Start()
    {
        deadPanel.SetActive(false);
        clearPanel.SetActive(false);

        switch (FindObjectOfType<StageInfo>().currentStage)
        {
            case StageLevel.stage5x5:
                minimapUI = minimapUIList[0];
                break;
            case StageLevel.stage7x7:
                minimapUI = minimapUIList[1];
                break;
            case StageLevel.stage9x9:
                minimapUI = minimapUIList[2];
                break;
        }
    }

    private void Update()
    {
        if (!isEnd)
        {
            Timer();
        }
    }

    public void SetBombCnt(int n)
    {
        bombCntText.text = n.ToString();
    }

    private void Timer()
    {
        sec += Time.deltaTime;

        timerText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
        if((int)sec > 59)
        {
            sec = 0;    min++;
        }
    }

    public void SetScanner(int n)
    {
        scannerText.text = n.ToString();
        if (n == 0)
            scannerText.color = Color.green;
        else if (n == 1)
            scannerText.color = Color.blue;
        else if (n == 2)
            scannerText.color = Color.magenta;
        else if (n == 3)
            scannerText.color = Color.yellow;
        else if (n == 4)
            scannerText.color = Color.red;
    }

    public void ActiveMap()
    {
        minimapUI.SetButtonColor();
    }
    public void CloseMap()
    {
        minimapUI.gameObject.SetActive(false);
    }

    public void PopDeadPanel()
    {
        deadPanel.SetActive(true);
    }

    public void PopClearPanel()
    {
        clearPanel.SetActive(true);
        int record = min * 60 + (int)sec;
        bool isBest = false;
        int best = 0;
        StageInfo _stageInfo = FindObjectOfType<StageInfo>();

        switch (_stageInfo.currentStage)
        {
            case StageLevel.stage5x5:
                best = PlayerPrefs.GetInt("BestScore_5x5");
                if (record > best)
                {
                    isBest = true;
                    PlayerPrefs.SetInt("BestScore_5x5", record);
                }
                break;

            case StageLevel.stage7x7:
                best = PlayerPrefs.GetInt("BestScore_7x7");
                if (record > best)
                {
                    isBest = true;
                    PlayerPrefs.SetInt("BestScore_7x7", record);
                }
                break;

            case StageLevel.stage9x9:
                best = PlayerPrefs.GetInt("BestScore_9x9");
                if (record > best)
                {
                    isBest = true;
                    PlayerPrefs.SetInt("BestScore_9x9", record);
                }
                break;
        }
        if (isBest)
            clearText.text = "New Record!";
        else
        {
            clearText.text = "Best Score: " + string.Format("{0:D2}:{1:D2}", best / 60, best % 60);
        }
    }
}
