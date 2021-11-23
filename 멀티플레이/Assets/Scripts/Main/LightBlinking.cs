using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  메인 화면의 깜빡임 효과
 */

public class LightBlinking : MonoBehaviour
{
    public float currentTime = 0.0f;
    public float nextTime = 2.0f;
    float blinkTimeMin = 0.5f;
    float blinkTimeMax = 5.0f;

    Animator blinkAnim = null;
    AudioSource blinkAS = null;

    void Start()
    {
        blinkAnim = GetComponent<Animator>();
        blinkAS = GetComponent<AudioSource>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > nextTime)
        {
            blinkAnim.Rebind();
            blinkAS.Play();
            blinkAnim.Play("BlinkLight");
            nextTime = Random.Range(blinkTimeMin, blinkTimeMax);
            currentTime = 0;
        }
    }
}
