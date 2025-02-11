using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{

    private bool isFadeInOUtActivate = false;
    private GameObject fadeInOutObj;

    float duration = 0.5f; // 0.5초 동안 활성화
    float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 페이트 인 코루틴
    /// </summary>
    IEnumerator FadeIn()
    {
        Color color = fadeInOutObj.GetComponent<Image>().color;
        if (isFadeInOUtActivate == false)
        {
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

            isFadeInOUtActivate = true;
        }
    }

    /// <summary>
    ///  페이드 아웃 코루틴
    /// </summary>
    IEnumerator FadeOUt()
    {
        Color color = fadeInOutObj.GetComponent<Image>().color;
        

        if (isFadeInOUtActivate)
        {
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

            isFadeInOUtActivate = false;
            fadeInOutObj.SetActive(false);
        }
    }
}
