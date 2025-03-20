using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingSceneController : MonoBehaviour
{
    float duration = 0.5f; // 0.5�� ���� Ȱ��ȭ
    

    public static string nextScene;
    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI progressText; // ����� ǥ�ÿ� TMP �ؽ�Ʈ
    [SerializeField] GameObject fadeInOutObj;

    private void Start()
    {
        StartCoroutine(LoadScene());

    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        StartCoroutine(FadeOut());
        yield return null;
        
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                progressText.text = Mathf.RoundToInt(progressBar.fillAmount * 100) + "%"; // TMP �ؽ�Ʈ ������Ʈ

                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                progressText.text = Mathf.RoundToInt(progressBar.fillAmount * 100) + "%";


                if (progressBar.fillAmount >= 1.0f) // 100% �Ϸ� ��
                {
                    yield return StartCoroutine(FadeIn()); //  FadeIn ���� �� �� ��ȯ
                    op.allowSceneActivation = true; // �� Ȱ��ȭ
                    yield break;
                }

                /*if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }*/
            }
        }
        
    }

    IEnumerator FadeOut()
    {
        
        Color color = fadeInOutObj.GetComponent<Image>().color;
        float elapsedTime = 0f;

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




    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
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
}
