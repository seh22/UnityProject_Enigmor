using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{

    //public bool isFadeInOUtActivate = false;
    public GameObject fadeInOutObj;

    float duration = 0.5f; // 0.5�� ���� Ȱ��ȭ
    float elapsedTime = 0f;


    private void Start()
    {
        StartCoroutine(FadeOut());

    }
    /// <summary>
    /// ����Ʈ �� �ڷ�ƾ
    /// </summary>
    public IEnumerator FadeIn()
    {
        Color color = fadeInOutObj.GetComponent<Image>().color;
        
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
    }

    /// <summary>
    ///  ���̵� �ƿ� �ڷ�ƾ
    /// </summary>
    public IEnumerator FadeOut()
    {
        Color color = fadeInOutObj.GetComponent<Image>().color;
        

        
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

        
        fadeInOutObj.SetActive(false);
    }
}
