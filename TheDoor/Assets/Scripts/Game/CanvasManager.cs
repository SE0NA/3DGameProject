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
    [SerializeField] MapManager[] minimapUIList = null;
    MapManager minimapUI = null;

    private float sec = 0f;
    private int min = 0;

    private void Start()
    {
        deadPanel.SetActive(false);

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
        Timer();
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
}
