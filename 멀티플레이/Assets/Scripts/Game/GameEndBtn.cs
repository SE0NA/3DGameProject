using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameEndBtn : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource _audiosource;

    public void BtnClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _audiosource.Play();
    }
}
