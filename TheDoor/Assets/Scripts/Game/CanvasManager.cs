using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Text bombCntText = null;
    [SerializeField] Text timerText = null;
    [SerializeField] Text scannerText = null;
    public float currentTime = 0f;

    private void Update()
    {
        currentTime += Time.deltaTime;
        Timer();
    }

    public void SetBombCnt(int n)
    {
        bombCntText.text = n.ToString();
    }

    private void Timer()
    {
        // 1:07 형식
        timerText.text = Mathf.Round(currentTime / 60).ToString() + ":" +
                        Mathf.Round(currentTime % 60 / 10).ToString() + Mathf.Round(currentTime % 60 % 10).ToString();
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
}
