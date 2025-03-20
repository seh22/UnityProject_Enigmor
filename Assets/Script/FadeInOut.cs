using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{

    //public bool isFadeInOUtActivate = false;
    public GameObject fadeInOutObj;

    float duration = 0.5f; // 0.5초 동안 활성화
    float elapsedTime = 0f;


    private void Start()
    {
        StartCoroutine(FadeOut());

    }
    /// <summary>
    /// 페이트 인 코루틴
    /// </summary>
    public IEnumerator FadeIn()
    {
        Color color = fadeInOutObj.GetComponent<Image>().color;
        
        fadeInOutObj.SetActive(true); // fadeInOut 활성화
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            color.a = progress; // 투명도 조절
            fadeInOutObj.GetComponent<Image>().color = color;
            yield return null;
        }

        Color finalColor = fadeInOutObj.GetComponent<Image>().color;
        finalColor.a = 1;
        fadeInOutObj.GetComponent<Image>().color = finalColor;
    }

    /// <summary>
    ///  페이드 아웃 코루틴
    /// </summary>
    public IEnumerator FadeOut()
    {
        Color color = fadeInOutObj.GetComponent<Image>().color;
        

        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            color.a = 1 - progress; // 투명도 조절
            fadeInOutObj.GetComponent<Image>().color = color;
            yield return null;
        }

        Color finalColor = fadeInOutObj.GetComponent<Image>().color;
        finalColor.a = 0;
        fadeInOutObj.GetComponent<Image>().color = finalColor;

        
        fadeInOutObj.SetActive(false);
    }
}
