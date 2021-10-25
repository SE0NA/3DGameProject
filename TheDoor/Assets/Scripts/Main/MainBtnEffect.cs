using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 *  메인 메뉴의 버튼의 UI효과
 *  - 사운드, 텍스트 크기/색상
 */

public class MainBtnEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Text btnText = null;

    int defaultTextSize = 20;
    int pointerTextSize = 23;
    Color defaultTextColor = Color.gray;
    Color pointerTextColor = Color.white;
    Color clickTextColor = Color.black;

    public AudioSource mainBtnAudioSource = null;

    public void OnBtnClick()
    {
        mainBtnAudioSource.Play();
        btnText.fontSize = defaultTextSize;
        btnText.color = clickTextColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mainBtnAudioSource.Play();
        btnText.fontSize = pointerTextSize;
        btnText.color = pointerTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        btnText.fontSize = defaultTextSize;
        btnText.color = defaultTextColor;
    }
}
