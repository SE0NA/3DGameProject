using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 *  버튼 클릭시의 본 기능
 */

public enum MainMenuBtn
{
    start=1, option=2, quit=3,
    play5=5, play7=7, play10=10, back=11
}

public class MainBtnClick : MonoBehaviour
{
    public GameObject titleText = null;
    public GameObject mainMenuPanel1 = null;
    public GameObject mainMenuPanel2 = null;
    public GameObject optionPanel = null;

    StageInfo _stageInfo = null;

    void Start()
    {
        mainMenuPanel2.SetActive(false);
        optionPanel.SetActive(false);

        _stageInfo = FindObjectOfType<StageInfo>();
    }
    
    public void OnClickFunc(int input)
    {
        MainMenuBtn current = (MainMenuBtn)input;
        switch (current)
        {
            // 메인 메뉴1
            case MainMenuBtn.start:
                mainMenuPanel1.SetActive(false);
                mainMenuPanel2.SetActive(true);
                break;

            case MainMenuBtn.option:
                titleText.SetActive(false);
                mainMenuPanel1.SetActive(false);
                optionPanel.SetActive(true);
                break;

            case MainMenuBtn.quit:
                Application.Quit();
                break;

            // 메인 메뉴2
            case MainMenuBtn.play5:
                mainMenuPanel1.SetActive(false);
                mainMenuPanel2.SetActive(true);
                _stageInfo.currentStageLevel = StageLevel.stage5x5;
                SceneManager.LoadScene("Game");
                break;

            case MainMenuBtn.play7:
                mainMenuPanel1.SetActive(false);
                mainMenuPanel2.SetActive(true);
                _stageInfo.currentStageLevel = StageLevel.stage7x7;
                SceneManager.LoadScene("Game");
                break;

            case MainMenuBtn.play10:
                mainMenuPanel1.SetActive(false);
                mainMenuPanel2.SetActive(true);
                _stageInfo.currentStageLevel = StageLevel.stage10x10;
                SceneManager.LoadScene("Game");
                break;

            case MainMenuBtn.back:
                mainMenuPanel1.SetActive(true);
                mainMenuPanel2.SetActive(false);
                break;
        }
    }
}
