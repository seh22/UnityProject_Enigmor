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

        //�ڷ�ƾ ����
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
            Debug.Log("����");
    }

    //��ü ���ϸ��̼� ���� �ڷ�ƾ
    IEnumerator AllAnimation()
    {
        //�� ���� ���̵� �ƿ�
        yield return StartCoroutine(ActivateFadeOut());

        // 1
        yield return StartCoroutine(NormalChat(ChatText_1, "������ �ϻ��� ���ӵǴ� ��� ��, \r\n�� ���� ������ �� �� �տ� ������ �־���."));
        new WaitForSeconds(2f);
        StartCoroutine(FadeInTouchBar());
        touchBar.SetActive(true);
        yield return ActivateTouch();
        touchBar.SetActive(false);
        yield return StartCoroutine(ActivateFadeIn());//���̵� ��
        ChatText_1.text = " ";

        // 2
        yield return StartCoroutine(ActivateFadeOut());//���̵� �ƿ�
        letterObject.SetActive(true);
        yield return StartCoroutine(NormalChat(ChatText_1, "������ �����ʹ� �繵 �޶���."));
        new WaitForSeconds(2f);
        //StartCoroutine(FadeInTouchBar());
        yield return ActivateTouch();

        //3
        yield return StartCoroutine(NormalChat(ChatText_1, "'����ðڽ��ϱ�?'"));
        button.gameObject.SetActive(true);
        yield return WaitForButtonPress();
        yield return StartCoroutine(ActivateFadeIn());//���̵���
        ChatText_1.text = " ";

        //4
        button.gameObject.SetActive(false);
        letterObject.SetActive(false);
        ChatText_1.gameObject.SetActive(false);
        letterPaper.SetActive(true);
        yield return StartCoroutine(ActivateFadeOut());//���̵�ƿ�
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //5
        letterPaper.transform.localScale = new Vector2(0.7f, 0.7f);
        letterPaper.transform.localPosition = new Vector2(0, 200);
        ChatText_1.rectTransform.anchoredPosition = new Vector2(0, -410); // �ؽ�Ʈ ��ġ ����
        ChatText_1.gameObject.SetActive(true);
        yield return StartCoroutine(NormalChat(ChatText_1, "������ ���� ����� �α� ������ �����̾���."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //6
        yield return StartCoroutine(NormalChat(ChatText_1, "������ ���� �̻��� ����� �������ٴ� �ҽ��̾���."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();
        yield return StartCoroutine(ActivateFadeIn());//���̵� ��
        ChatText_1.text = " ";

        //7
        letter_1.SetActive(false);
        letter_2.text = " ";
        letter_3.text = " ";
        letterPaper.transform.localScale = new Vector2(1f, 1f);
        Key.transform.localPosition = new Vector2(0, 0);
        yield return StartCoroutine(ActivateFadeOut()); //���̵� �ƿ�
        yield return StartCoroutine(NormalChat(ChatText_1, "������ ���� ����,\r\n ȣ��ɰ� �ҾȰ����� ������ �αٰŷȴ�."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //8
        yield return StartCoroutine(NormalChat(ChatText_1, "���� �Ź����� ���鼮�� �����̶��...."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //9
        letterPaper.SetActive(false);
        ChatText_1.rectTransform.anchoredPosition = new Vector2(0, 0);
        yield return StartCoroutine(NormalChat(ChatText_1, "���� ������ ���� �߰����� �Ű��."));
        ButtonText.text = "�ý� Ÿ��";
        button.gameObject.SetActive(true);
        yield return WaitForButtonPress();
        yield return StartCoroutine(ActivateFadeIn()); //���̵� ��
        ChatText_1.text = " ";

        //10

        button.gameObject.SetActive(false);
        backgroundObject.SetActive(true);
        yield return StartCoroutine(ActivateFadeOut()); //���̵� �ƿ�
        yield return StartCoroutine(NormalChat(ChatText_1, "�ýø� Ÿ�� ������ ������ ��ҿ� �׿��־���."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //11
        yield return StartCoroutine(NormalChat(ChatText_1, "...������...! �١أ�....!��"));
        ChatText_2.gameObject.SetActive(true);
        yield return StartCoroutine(NormalChat(ChatText_2, "������ ������� ���� \r\n ����������� �Ҷ������� �Ҹ��� ����Դ�."));
        backgroundObject.SetActive(true);
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //12
        ChatText_2.gameObject.SetActive(false);
        yield return StartCoroutine(NormalChat(ChatText_1, "���� �..�� �١أ�....!��"));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //13
        yield return StartCoroutine(NormalChat(ChatText_1, " "));
        ButtonText.text = "�ٰ�����";
        button.gameObject.SetActive(true);
        yield return WaitForButtonPress();

        //14
        button.gameObject.SetActive(false);
        yield return StartCoroutine(NormalChat(ChatText_1, "�׵��� ������ �ƹ��� �������� ���� �ʾҴ�.\r\n���� �������� �����ϰ� ���� ���� �� ������. \r\n�׵��� �ٶ󺸰� �ִ� ����....."));
        //yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();

        //15
        yield return StartCoroutine(NormalChat(ChatText_1, "���� �Ƿ� ����ϰ� ���� �������� \r\n ����� �ִ� ������ �ٶ󺸰� �־���."));
        ChatText_1.text = ChatText_1.text.Replace("���� ��", "<color=red>���� ��</color>");
        yield return StartCoroutine(FadeInImage1());
       // yield return StartCoroutine(ActivateTouchBar());
        yield return ActivateTouch();
        yield return StartCoroutine(ActivateFadeIn()); //���̵� ��

        //�̸� ����
        backgroundObject.SetActive(false);
        image1.SetActive(false);
        ChatText_1.gameObject.SetActive(false);
        CreateName.SetActive(true);
        yield return StartCoroutine(ActivateFadeOut()); //���̵� �ƿ�
    }

    //���̵� �� �ƿ� �ڷ�ƾ
    IEnumerator ActivateFadeIn()
    {
        float duration = 0.5f; // 0.5�� ���� Ȱ��ȭ
        float elapsedTime = 0f;
        fadeInOut.SetActive(true);

        if (isFadeInOUtActivate == false)
        {
            fadeInOut.SetActive(true); // fadeInOut Ȱ��ȭ
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                Color color = fadeSpr.color;
                color.a = progress; // ���� ����
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
    
    //�� ���� �ڷ�ƾ
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

    //��ư Ŭ�� �ڷ�ƾ
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

    //��ġ��
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

    //������ ��Ÿ����
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

    //ȭ�� Ŭ���� 
    /*IEnumerator ActivateTouchBar()
    {
        touchBar.SetActive(true);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        touchBar.SetActive(false);
    }*/

    //ȭ�� Ŭ����
    IEnumerator ActivateTouch()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
    }

}
