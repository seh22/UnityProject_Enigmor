using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{

    private bool isFadeInOUtActivate = false;
    private GameObject fadeInOutObj;

    float duration = 0.5f; // 0.5�� ���� Ȱ��ȭ
    float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// ����Ʈ �� �ڷ�ƾ
    /// </summary>
    IEnumerator FadeIn()
    {
        Color color = fadeInOutObj.GetComponent<Image>().color;
        if (isFadeInOUtActivate == false)
        {
            fadeInOutObj.SetActive(true); // fadeInOut Ȱ��ȭ
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                
                color.a = progress; // ���� ����
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
    ///  ���̵� �ƿ� �ڷ�ƾ
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

                color.a = 1 - progress; // ���� ����
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
