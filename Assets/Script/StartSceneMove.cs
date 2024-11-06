using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� �߰�

public class StartSceneMove : MonoBehaviour
{
    [SerializeField] private GameObject playBar_1;
    [SerializeField] private GameObject playBar_2;

    [SerializeField] private GameObject playBar;
    [SerializeField] private GameObject playBarBar;

    private Camera mainCamera;   // ���� ī�޶� �����ϱ� ���� ����
    [SerializeField] private float zoomTargetSize = 10f;   // ī�޶� �� Ÿ�� ������
    [SerializeField] private float zoomSpeed = 4.0f;       // ī�޶� �� �ӵ�
    private bool isZooming = false;      // ī�޶� �� ������ ���θ� Ȯ���ϴ� �÷���
    private bool isActivatingNotebook = false;  // ��Ʈ Ȱ��ȭ ������ ���θ� Ȯ���ϴ� �÷���

    public GameObject keyObject;        // ���� ������Ʈ
    public Vector3 keyNewPosition;      // ���� ������Ʈ�� ���ο� ��ġ

    public GameObject clockObject;      // �ð� ������Ʈ
    public Vector3 clockNewPosition;    // �ð� ������Ʈ�� ���ο� ��ġ

    public GameObject clockHand;        // �ð� �ٴ� ������Ʈ
    public float rotationSpeed = 6f;    // �ð� �ٴ� ȸ�� �ӵ� (1�ʿ� 6��)

    public GameObject minuteHand;       // ��ħ ������Ʈ
    public float minuteRotationSpeed = 6f / 60f;   // ��ħ ȸ�� �ӵ� (1�п� 6��)

    public GameObject magnifierObject;  // ������ ������Ʈ
    public Vector3 magnifierNewPosition;    // ������ ������Ʈ�� ���ο� ��ġ

    public GameObject noteObject;       // ��Ʈ ������Ʈ
    private SpriteRenderer noteSpr;     // ��Ʈ ������Ʈ�� ��������Ʈ ������

    public LineRenderer firstLine;      // �� 1
    public LineRenderer secondLine;     // �� 2
    public LineRenderer thirdLine;      // �� 3
    public LineRenderer fourthLine;     // �� 4
    public LineRenderer fifthLine;      // �� 5

    public Vector3[] firstLinePositions = new Vector3[3];   // �� 1�� ��ġ �迭
    public Vector3[] secondLinePositions = new Vector3[3];  // �� 2�� ��ġ �迭
    public Vector3[] thirdLinePositions = new Vector3[3];   // �� 3�� ��ġ �迭
    public Vector3[] fourthLinePositions = new Vector3[3];  // �� 4�� ��ġ �迭
    public Vector3[] fifthLinePositions = new Vector3[3];   // �� 5�� ��ġ �迭

    private bool isNotebookActivate = false;    // ��Ʈ Ȱ��ȭ ���θ� Ȯ���ϴ� �÷���

    private bool positionsChanged = false;      // ������Ʈ ��ġ ���� ���θ� Ȯ���ϴ� �÷���

    private Coroutine notebookCoroutine;    // ��Ʈ Ȱ��ȭ �ڷ�ƾ
    private Coroutine zoomCoroutine;        // �� �ƿ� �ڷ�ƾ

    private bool isPlayBarActive = false;   // playBar Ȱ��ȭ ���θ� Ȯ���ϴ� �÷���

    public GameObject playerName;
    public GameObject playerNameBar;

    public GameObject fadeInOut;
    private SpriteRenderer fadeSpr;
    private bool isFadeInOUtActivate = false;
    private Coroutine fadeCoroutine;

    void Start()
    {
        mainCamera = Camera.main;   // ���� ī�޶� ����
        noteSpr = noteObject.GetComponent<SpriteRenderer>();    // ��Ʈ ��������Ʈ ������ ����

        // fadeInOut�� Ȱ��ȭ�Ͽ� fadeSpr�� �ʱ�ȭ�� �� �ְ� ��
        fadeInOut.SetActive(true);
        fadeSpr = fadeInOut.GetComponent<SpriteRenderer>();    // fadeInOut ��������Ʈ ������ ����
        fadeInOut.SetActive(false); // ���� �ٽ� ��Ȱ��ȭ

        // ��Ʈ�� �ʱ� ������ �����ϰ� ����
        Color color = noteSpr.color;
        color.a = 0;
        noteSpr.color = color;

        // �� ���� ��ġ ī��Ʈ�� 3���� ����
        firstLine.positionCount = 3;
        secondLine.positionCount = 3;
        thirdLine.positionCount = 3;
        fourthLine.positionCount = 3;
        fifthLine.positionCount = 3;

        // �� ������ �ʱ� ��ġ�� ����
        SetLineInitialPositions(firstLine, firstLinePositions);
        SetLineInitialPositions(secondLine, secondLinePositions);
        SetLineInitialPositions(thirdLine, thirdLinePositions);
        SetLineInitialPositions(fourthLine, fourthLinePositions);
        SetLineInitialPositions(fifthLine, fifthLinePositions);

        // playBar �ʱ� ��Ȱ��ȭ
        playBar.SetActive(false);
        playBarBar.SetActive(false);
        playBar_1.SetActive(false);
        playBar_2.SetActive(false);

        fadeInOut.SetActive(false);
    }

    void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���ϰ� �� ���� �ƴϸ� ��Ʈ Ȱ��ȭ ���� �ƴ� ��
        if (Input.GetMouseButtonDown(0) && !isZooming && !isActivatingNotebook)
        {
            // ������ ��Ʈ Ȱ��ȭ �ڷ�ƾ�� ������ ����
            if (notebookCoroutine != null)
            {
                StopCoroutine(notebookCoroutine);
            }
            // ������ �� �ڷ�ƾ�� ������ ����
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }
            // ��Ʈ Ȱ��ȭ �� �� �ƿ� �ڷ�ƾ ����
            notebookCoroutine = StartCoroutine(ActivateNotebookAndZoomOut());
        }

        // �ð� �ٴ� ȸ��
        RotateClockHand();

        // playBar�� Ȱ��ȭ�� ���¿��� Ŭ���Ǹ� �� ��ȯ
        if (isPlayBarActive && Input.GetMouseButtonDown(0))
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(ActivateFadeinoutAndLoadScene());
        }
    }

    // ���̵� �� �� �� ��ȯ �ڷ�ƾ
    IEnumerator ActivateFadeinoutAndLoadScene()
    {
        yield return StartCoroutine(ActivateFadeIn());
        SceneManager.LoadScene("IntroScene");
    }

    // ���̵� ��
    IEnumerator ActivateFadeIn()
    {
        float duration = 0.5f; // 0.5�� ���� Ȱ��ȭ
        float elapsedTime = 0f;

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

    // ��Ʈ Ȱ��ȭ �� �� �ƿ� �ڷ�ƾ
    IEnumerator ActivateNotebookAndZoomOut()
    {
        isActivatingNotebook = true;

        // ��Ʈ ������Ʈ�� Ȱ��ȭ
        yield return StartCoroutine(ActivateNotebook());

        // ī�޶� �� �ƿ�
        zoomCoroutine = StartCoroutine(ZoomOutCoroutine());

        isActivatingNotebook = false;
    }

    // ��Ʈ Ȱ��ȭ �ڷ�ƾ
    IEnumerator ActivateNotebook()
    {
        float duration = 1.5f; // 1.5�� ���� Ȱ��ȭ
        float elapsedTime = 0f;

        if (isNotebookActivate == false)
        {
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                Color color = noteSpr.color;
                color.a = progress; // ���� ����
                noteSpr.color = color;
                yield return null;
            }

            Color finalColor = noteSpr.color;
            finalColor.a = 1;
            noteSpr.color = finalColor;

            isNotebookActivate = true;
        }
    }

    // ī�޶� �� �ƿ� �ڷ�ƾ
    IEnumerator ZoomOutCoroutine()
    {
        isZooming = true;
        float elapsedTime = 0f;
        float startSize = mainCamera.orthographicSize;
        float targetSize = zoomTargetSize;
        Vector3 startPosition = mainCamera.transform.position;
        Vector3 targetPosition = new Vector3(0, 0, -10); // ī�޶��� �⺻ ��ġ�� Z�� -10���� ����

        while (elapsedTime < zoomSpeed)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / zoomSpeed;
            float newSize = Mathf.Lerp(startSize, targetSize, progress);
            mainCamera.orthographicSize = newSize;
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);

            if (!positionsChanged && progress >= 0.4f)
            {
                StartCoroutine(ChangeObjectPosition(keyObject, keyNewPosition, zoomSpeed * 0.6f, EaseOutCubic));
                StartCoroutine(ChangeObjectPosition(clockObject, clockNewPosition, zoomSpeed * 0.6f, EaseOutCubic));
                StartCoroutine(ChangeObjectPosition(magnifierObject, magnifierNewPosition, zoomSpeed * 0.6f, EaseOutQuart));
                positionsChanged = true;
            }

            yield return null;
        }

        mainCamera.orthographicSize = targetSize;
        mainCamera.transform.position = targetPosition;
        isZooming = false;

        // �� �ƿ� �ִϸ��̼� �Ϸ� �� �� �ִϸ��̼� ����
        StartCoroutine(AnimateLines());
    }

    // ������Ʈ ��ġ ���� �ڷ�ƾ
    IEnumerator ChangeObjectPosition(GameObject obj, Vector3 targetPosition, float duration, System.Func<float, float> easingFunction)
    {
        if (obj != null)
        {
            Vector3 startPosition = obj.transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                obj.transform.position = Vector3.Lerp(startPosition, targetPosition, easingFunction(t));
                yield return null;
            }

            obj.transform.position = targetPosition;
        }
        else
        {
            Debug.LogError("Target Object is not assigned.");
        }
    }

    // ���� �ִϸ��̼� �ڷ�ƾ�� playBar Ȱ��ȭ
    IEnumerator AnimateLines()
    {
        // ��� ���� �ִϸ��̼��� ���ÿ� ����
        Coroutine[] lineCoroutines = new Coroutine[5];
        lineCoroutines[0] = StartCoroutine(AnimateLine(firstLine, firstLinePositions));
        lineCoroutines[1] = StartCoroutine(AnimateLine(secondLine, secondLinePositions));
        lineCoroutines[2] = StartCoroutine(AnimateLine(thirdLine, thirdLinePositions));
        lineCoroutines[3] = StartCoroutine(AnimateLine(fourthLine, fourthLinePositions));
        lineCoroutines[4] = StartCoroutine(AnimateLine(fifthLine, fifthLinePositions));

        // ��� ���� �ִϸ��̼��� �Ϸ�� ������ ��ٸ�
        foreach (Coroutine lineCoroutine in lineCoroutines)
        {
            yield return lineCoroutine;
        }

        // ��� ���� �ִϸ��̼��� �Ϸ�� �� playBar Ȱ��ȭ
        ActivatePlayBar();
    }

    // ���� �ִϸ��̼� �ڷ�ƾ
    IEnumerator AnimateLine(LineRenderer line, Vector3[] positions)
    {
        float duration = 2.0f; // 2�� ���� �ִϸ��̼�
        float halfDuration = duration / 2f;
        float elapsedTime = 0f;

        // ���������� �߰������� �ִϸ��̼�
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDuration;
            Vector3 newPosition = Vector3.Lerp(positions[0], positions[1], EaseInSine(t));
            line.SetPosition(1, newPosition);
            line.SetPosition(2, newPosition); // �߰��������� ������ ���� ������Ʈ
            yield return null;
        }

        // �߰������� �������� �ִϸ��̼�
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDuration;
            Vector3 newPosition = Vector3.Lerp(positions[1], positions[2], EaseInSine(t));
            line.SetPosition(2, newPosition);
            yield return null;
        }

        // ���� ��ġ ����
        line.SetPosition(2, positions[2]);
    }

    // playBar ���ڰŸ� �ִϸ��̼� �ڷ�ƾ

    IEnumerator BlinkPlayBars(params GameObject[] playBars)
    {
        isPlayBarActive = true;
        float blinkDuration = 1.0f;

        while (isPlayBarActive)
        {
            // Fade out
            for (float elapsedTime = 0f; elapsedTime < blinkDuration; elapsedTime += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1, 0, elapsedTime / blinkDuration);
                foreach (var playBar in playBars)
                {
                    SpriteRenderer playBarRenderer = playBar.GetComponent<SpriteRenderer>();
                    Color color = playBarRenderer.color;
                    color.a = alpha;
                    playBarRenderer.color = color;
                }
                yield return null;
            }

            // Fade in
            for (float elapsedTime = 0f; elapsedTime < blinkDuration; elapsedTime += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(0, 1, elapsedTime / blinkDuration);
                foreach (var playBar in playBars)
                {
                    SpriteRenderer playBarRenderer = playBar.GetComponent<SpriteRenderer>();
                    Color color = playBarRenderer.color;
                    color.a = alpha;
                    playBarRenderer.color = color;
                }
                yield return null;
            }
        }
    }



    // ���� �ʱ� ��ġ ���� �Լ�
    void SetLineInitialPositions(LineRenderer line, Vector3[] positions)
    {
        line.SetPosition(0, positions[0]);
        line.SetPosition(1, positions[0]);
        line.SetPosition(2, positions[0]);
    }

    // EaseOutCubic �Լ� (�ε巯�� ����)
    float EaseOutCubic(float t)
    {
        t--;
        return t * t * t + 1;
    }

    // EaseOutQuart �Լ� (�� �ε巯�� ����)
    float EaseOutQuart(float t)
    {
        t--;
        return 1 - t * t * t * t;
    }

    // EaseInSine �Լ� (�ε巯�� ����)
    float EaseInSine(float t)
    {
        return 1 - Mathf.Cos((t * Mathf.PI) / 2);
    }

    // playBar Ȱ��ȭ �Լ�
    void ActivatePlayBar()
    {
        playBar.SetActive(true);
        playBarBar.SetActive(true);
        playBar_1.SetActive(true);
        playBar_2.SetActive(true);
        isPlayBarActive = true;

        StartCoroutine(BlinkPlayBars(playBar, playBar_1, playBar_2));
        StartCoroutine(MoveAndBlinkPlayBars(playBar_1, playBar_2, playBar_1.transform.position, playBar_2.transform.position, 1f)); // �պ� 1�ʷ� ����
    }



    // �ð� �ٴ� ȸ�� �Լ�
    void RotateClockHand()
    {
        if (clockHand != null)
        {
            float angle = rotationSpeed * Time.deltaTime;
            clockHand.transform.Rotate(Vector3.forward, -angle); // �ð� �������� ȸ��
        }
        else
        {
            Debug.LogError("Clock Hand is not assigned.");
        }

        if (minuteHand != null)
        {
            float minuteAngle = minuteRotationSpeed * Time.deltaTime;
            minuteHand.transform.Rotate(Vector3.forward, -minuteAngle); // �ð� �������� ȸ��
        }
        else
        {
            Debug.LogError("Minute Hand is not assigned.");
        }
    }

    float EaseInOutCubic(float t)
    {
        return t < 0.5 ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
    }

    IEnumerator MoveAndBlinkPlayBars(GameObject playBar1, GameObject playBar2, Vector3 start1, Vector3 start2, float duration)
    {
        float elapsedTime = 0f;
        Vector3 end1 = new Vector3(-2, start1.y, start1.z);
        Vector3 end2 = new Vector3(2, start2.y, start2.z);
        Vector3 middle1 = new Vector3(-1.5f, start1.y, start1.z);
        Vector3 middle2 = new Vector3(1.5f, start2.y, start2.z);

        SpriteRenderer playBarRenderer1 = playBar1.GetComponent<SpriteRenderer>();
        SpriteRenderer playBarRenderer2 = playBar2.GetComponent<SpriteRenderer>();

        while (true)
        {
            elapsedTime = 0f;

            // ������ ��
            while (elapsedTime < duration )
            {
                elapsedTime += Time.deltaTime;
                float t = EaseInOutCubic(elapsedTime / (duration / 2));
                playBar1.transform.position = Vector3.Lerp(middle1, end1, t);
                playBar2.transform.position = Vector3.Lerp(middle2, end2, t);

                yield return null;
            }

            // �ٽ� ������ ��
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = EaseInOutCubic(elapsedTime / (duration / 2));
                playBar1.transform.position = Vector3.Lerp(end1, middle1, t);
                playBar2.transform.position = Vector3.Lerp(end2, middle2, t);

                yield return null;
            }
        }
    }

}