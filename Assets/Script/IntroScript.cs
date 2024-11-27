using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private GameObject fadeInOut;
    [SerializeField] private SpriteRenderer fadeSpr;
    [SerializeField] private bool isFadeInOUtActivate = false;
    [SerializeField] private Coroutine fadeCoroutine;

    [SerializeField] private TMP_Text ChatText_1;
    [SerializeField] private TMP_Text ChatText_2;
    [SerializeField] private TMP_Text ButtonText;
    [SerializeField] private string writerText = "";

    [SerializeField] private GameObject touchBar;
    [SerializeField] private GameObject letterObject;
    [SerializeField] private GameObject letterPaper;
    [SerializeField] private GameObject backgroundObject;
    [SerializeField] private GameObject image1;
    [SerializeField] private Button button;

    [SerializeField] private GameObject CreateName;

    [SerializeField] private TMP_InputField UserNameInput;
    [SerializeField] private Button createNameButton;

    [SerializeField] private GameObject Key;
    [SerializeField] private GameObject letter_1;
    [SerializeField] private TMP_Text letter_2;
    [SerializeField] private TMP_Text letter_3;

    //private bool textPractiecController = false;

    void Start()
    {
        touchBar.SetActive(false);
        letterObject.SetActive(false);
        letterPaper.SetActive(false);
        backgroundObject.SetActive(false);
        image1.SetActive(false);
        button.gameObject.SetActive(false);
        ChatText_2.gameObject.SetActive(false);
        CreateName.SetActive(false);

        //코루틴 시작
        StartCoroutine(AllAnimation());

        createNameButton.onClick.AddListener(CreateUserNameOnClickButton);
    }

    void CreateUserNameOnClickButton()
    {
        String userNameText = UserNameInput.text;
        Console.WriteLine(userNameText);
        if (userNameText.Length <= 8 && userNameText.Length >= 2)
        {
            PlayerPrefs.SetString("UserName", userNameText);
            Debug.Log(PlayerPrefs.GetString("UserName"));
            SceneManager.LoadScene("ChapterScene");
        }
        else
            Debug.Log("오류");
    }

    //전체 에니메이션 관리 코루틴
    IEnumerator AllAnimation()
    {
        //씬 시작 페이드 아웃
        yield return StartCoroutine(ActivateFadeOut());

        // 1
        yield return StartCoroutine(NormalChat(ChatText_1, "지루한 일상이 지속되던 어느 날, \r\n한 통의 편지가 내 문 앞에 놓여져 있었다."));
        new WaitForSeconds(2f);
        StartCoroutine(FadeInTouchBar());
        touchBar.SetActive(true);
        yield return ActivateTouch();
        touchBar.SetActive(false);
        yield return StartCoroutine(ActivateFadeIn());//페이드 인
        ChatText_1.text = " ";

        // 2
        yield return StartCoroutine(ActivateFadeOut());//페이드 아웃
        letterObject.SetActive(true);
        yield return StartCoroutine(NormalChat(ChatText_1, "보통의 편지와는 사뭇 달랐다."));
        new WaitForSeconds(2f);
        //StartCoroutine(FadeInTouchBar());
        yield return ActivateTouch();

        //3
        yield return StartCoroutine(NormalChat(ChatText_1, "'열어보시겠습니까?'"));
        button.gameObject.SetActive(true);
        yield return WaitForButtonPress();
        yield return StartCoroutine(ActivateFadeIn());//페이드인
        ChatText_1.text = " ";

        //4
        button.gameObject.SetActive(false);
        letterObject.SetActive(false);
        ChatText_1.gameObject.SetActive(false);
        letterPaper.SetActive(true);
        yield return StartCoroutine(ActivateFadeOut());//페이드아웃
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //5
        letterPaper.transform.localScale = new Vector2(0.7f, 0.7f);
        letterPaper.transform.localPosition = new Vector2(0, 200);
        ChatText_1.rectTransform.anchoredPosition = new Vector2(0, -410); // 텍스트 위치 변경
        ChatText_1.gameObject.SetActive(true);
        yield return StartCoroutine(NormalChat(ChatText_1, "편지를 보낸 사람은 인근 마을의 시장이었다."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //6
        yield return StartCoroutine(NormalChat(ChatText_1, "마을에 무언가 이상한 사건이 벌어졌다는 소식이었다."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();
        yield return StartCoroutine(ActivateFadeIn());//페이드 인
        ChatText_1.text = " ";

        //7
        letter_1.SetActive(false);
        letter_2.text = " ";
        letter_3.text = " ";
        letterPaper.transform.localScale = new Vector2(1f, 1f);
        Key.transform.localPosition = new Vector2(0, 0);
        yield return StartCoroutine(ActivateFadeOut()); //페이드 아웃
        yield return StartCoroutine(NormalChat(ChatText_1, "편지를 읽은 순간,\r\n 호기심과 불안감으로 가슴이 두근거렸다."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //8
        yield return StartCoroutine(NormalChat(ChatText_1, "요즘 신문에서 떠들석한 마을이라니...."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //9
        letterPaper.SetActive(false);
        ChatText_1.rectTransform.anchoredPosition = new Vector2(0, 0);
        yield return StartCoroutine(NormalChat(ChatText_1, "급히 마을을 향해 발걸음을 옮겼다."));
        ButtonText.text = "택시 타기";
        button.gameObject.SetActive(true);
        yield return WaitForButtonPress();
        yield return StartCoroutine(ActivateFadeIn()); //페이드 인
        ChatText_1.text = " ";

        //10

        button.gameObject.SetActive(false);
        backgroundObject.SetActive(true);
        yield return StartCoroutine(ActivateFadeOut()); //페이드 아웃
        yield return StartCoroutine(NormalChat(ChatText_1, "택시를 타고 도착한 마을은 어둠에 쌓여있었다."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //11
        yield return StartCoroutine(NormalChat(ChatText_1, "...”여기...! ☆※＆....!”"));
        ChatText_2.gameObject.SetActive(true);
        yield return StartCoroutine(NormalChat(ChatText_2, "적막한 고요함을 깨는 \r\n 마을사람들의 소란스러운 소리가 들려왔다."));
        backgroundObject.SetActive(true);
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //12
        ChatText_2.gameObject.SetActive(false);
        yield return StartCoroutine(NormalChat(ChatText_1, "“헉 어떡..→ ☆※＆....!”"));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //13
        yield return StartCoroutine(NormalChat(ChatText_1, " "));
        ButtonText.text = "다가가기";
        button.gameObject.SetActive(true);
        yield return WaitForButtonPress();

        //14
        button.gameObject.SetActive(false);
        yield return StartCoroutine(NormalChat(ChatText_1, "그들은 나에게 아무런 눈길조차 주지 않았다.\r\n나의 존재조차 인지하고 있지 않은 듯 보였다. \r\n그들이 바라보고 있는 것은....."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //15
        yield return StartCoroutine(NormalChat(ChatText_1, "붉은 피로 흥건하게 물든 조각들이 \r\n 흩어져 있는 곳만을 바라보고 있었다."));
        ChatText_1.text = ChatText_1.text.Replace("붉은 피", "<color=red>붉은 피</color>");
        yield return StartCoroutine(FadeInImage1());
       // yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();
        yield return StartCoroutine(ActivateFadeIn()); //페이드 인

        //이름 생성
        backgroundObject.SetActive(false);
        image1.SetActive(false);
        ChatText_1.gameObject.SetActive(false);
        CreateName.SetActive(true);
        yield return StartCoroutine(ActivateFadeOut()); //페이드 아웃
    }

    //페이드 인 아웃 코루틴
    IEnumerator ActivateFadeIn()
    {
        float duration = 0.5f; // 0.5초 동안 활성화
        float elapsedTime = 0f;
        fadeInOut.SetActive(true);

        if (isFadeInOUtActivate == false)
        {
            fadeInOut.SetActive(true); // fadeInOut 활성화
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                Color color = fadeSpr.color;
                color.a = progress; // 투명도 조절
                fadeSpr.color = color;
                yield return null;
            }

            Color finalColor = fadeSpr.color;
            finalColor.a = 1;
            fadeSpr.color = finalColor;

            isFadeInOUtActivate = true;
        }
    }
    IEnumerator ActivateFadeOut()
    {
        float duration = 0.5f;
        float elapsedTime = 0f;

        if (isFadeInOUtActivate)
        {
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                Color color = fadeSpr.color;
                color.a = 1 - progress;
                fadeSpr.color = color;
                yield return null;
            }

            Color finalColor = fadeSpr.color;
            finalColor.a = 0;
            fadeSpr.color = finalColor;

            fadeInOut.SetActive(false);
            
        }
        isFadeInOUtActivate = false;
    }
    
    //글 쓰는 코루틴
    IEnumerator NormalChat(TMP_Text textWrite, string narration)
    {
        int a = 0;
        writerText = "";
        float typingDelay = 0.05f;

        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            textWrite.text = writerText;
            yield return new WaitForSeconds(typingDelay);
        }
    }

    //버튼 클릭 코루틴
    IEnumerator WaitForButtonPress()
    {
        bool buttonPressed = false;
        button.onClick.AddListener(() => buttonPressed = true);

        // Wait until the button is pressed
        while (!buttonPressed)
        {
            yield return null;
        }

        // Remove the listener to prevent it from being called multiple times
        button.onClick.RemoveAllListeners();
    }

    //터치바
    IEnumerator FadeInTouchBar()
    {
        float duration = 0.5f;
        float elapsedTime = 0f;

        touchBar.SetActive(true);
        Color color = touchBar.GetComponent<SpriteRenderer>().color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            color.a = progress;
            touchBar.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
    }

    //붉은기 나타내기
    IEnumerator FadeInImage1()
    {
        float duration = 0.5f;
        float elapsedTime = 0f;

        image1.SetActive(true);
        Color color = image1.GetComponent<SpriteRenderer>().color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            color.a = progress;
            image1.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
    }

    //화면 클릭시 
    /*IEnumerator ActivateTouchBar()
    {
        touchBar.SetActive(true);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        touchBar.SetActive(false);
    }*/

    //화면 클릭시
    IEnumerator ActivateTouch()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
    }

}
