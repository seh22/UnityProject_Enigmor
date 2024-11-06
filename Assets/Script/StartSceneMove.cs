using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 추가

public class StartSceneMove : MonoBehaviour
{
    [SerializeField] private GameObject playBar_1;
    [SerializeField] private GameObject playBar_2;

    [SerializeField] private GameObject playBar;
    [SerializeField] private GameObject playBarBar;

    private Camera mainCamera;   // 메인 카메라를 참조하기 위한 변수
    [SerializeField] private float zoomTargetSize = 10f;   // 카메라 줌 타겟 사이즈
    [SerializeField] private float zoomSpeed = 4.0f;       // 카메라 줌 속도
    private bool isZooming = false;      // 카메라 줌 중인지 여부를 확인하는 플래그
    private bool isActivatingNotebook = false;  // 노트 활성화 중인지 여부를 확인하는 플래그

    public GameObject keyObject;        // 열쇠 오브젝트
    public Vector3 keyNewPosition;      // 열쇠 오브젝트의 새로운 위치

    public GameObject clockObject;      // 시계 오브젝트
    public Vector3 clockNewPosition;    // 시계 오브젝트의 새로운 위치

    public GameObject clockHand;        // 시계 바늘 오브젝트
    public float rotationSpeed = 6f;    // 시계 바늘 회전 속도 (1초에 6도)

    public GameObject minuteHand;       // 분침 오브젝트
    public float minuteRotationSpeed = 6f / 60f;   // 분침 회전 속도 (1분에 6도)

    public GameObject magnifierObject;  // 돋보기 오브젝트
    public Vector3 magnifierNewPosition;    // 돋보기 오브젝트의 새로운 위치

    public GameObject noteObject;       // 노트 오브젝트
    private SpriteRenderer noteSpr;     // 노트 오브젝트의 스프라이트 렌더러

    public LineRenderer firstLine;      // 선 1
    public LineRenderer secondLine;     // 선 2
    public LineRenderer thirdLine;      // 선 3
    public LineRenderer fourthLine;     // 선 4
    public LineRenderer fifthLine;      // 선 5

    public Vector3[] firstLinePositions = new Vector3[3];   // 선 1의 위치 배열
    public Vector3[] secondLinePositions = new Vector3[3];  // 선 2의 위치 배열
    public Vector3[] thirdLinePositions = new Vector3[3];   // 선 3의 위치 배열
    public Vector3[] fourthLinePositions = new Vector3[3];  // 선 4의 위치 배열
    public Vector3[] fifthLinePositions = new Vector3[3];   // 선 5의 위치 배열

    private bool isNotebookActivate = false;    // 노트 활성화 여부를 확인하는 플래그

    private bool positionsChanged = false;      // 오브젝트 위치 변경 여부를 확인하는 플래그

    private Coroutine notebookCoroutine;    // 노트 활성화 코루틴
    private Coroutine zoomCoroutine;        // 줌 아웃 코루틴

    private bool isPlayBarActive = false;   // playBar 활성화 여부를 확인하는 플래그

    public GameObject playerName;
    public GameObject playerNameBar;

    public GameObject fadeInOut;
    private SpriteRenderer fadeSpr;
    private bool isFadeInOUtActivate = false;
    private Coroutine fadeCoroutine;

    void Start()
    {
        mainCamera = Camera.main;   // 메인 카메라 참조
        noteSpr = noteObject.GetComponent<SpriteRenderer>();    // 노트 스프라이트 렌더러 참조

        // fadeInOut을 활성화하여 fadeSpr를 초기화할 수 있게 함
        fadeInOut.SetActive(true);
        fadeSpr = fadeInOut.GetComponent<SpriteRenderer>();    // fadeInOut 스프라이트 렌더러 참조
        fadeInOut.SetActive(false); // 이후 다시 비활성화

        // 노트의 초기 색상을 투명하게 설정
        Color color = noteSpr.color;
        color.a = 0;
        noteSpr.color = color;

        // 각 선의 위치 카운트를 3으로 설정
        firstLine.positionCount = 3;
        secondLine.positionCount = 3;
        thirdLine.positionCount = 3;
        fourthLine.positionCount = 3;
        fifthLine.positionCount = 3;

        // 각 라인의 초기 위치를 설정
        SetLineInitialPositions(firstLine, firstLinePositions);
        SetLineInitialPositions(secondLine, secondLinePositions);
        SetLineInitialPositions(thirdLine, thirdLinePositions);
        SetLineInitialPositions(fourthLine, fourthLinePositions);
        SetLineInitialPositions(fifthLine, fifthLinePositions);

        // playBar 초기 비활성화
        playBar.SetActive(false);
        playBarBar.SetActive(false);
        playBar_1.SetActive(false);
        playBar_2.SetActive(false);

        fadeInOut.SetActive(false);
    }

    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭하고 줌 중이 아니며 노트 활성화 중이 아닐 때
        if (Input.GetMouseButtonDown(0) && !isZooming && !isActivatingNotebook)
        {
            // 기존의 노트 활성화 코루틴이 있으면 중지
            if (notebookCoroutine != null)
            {
                StopCoroutine(notebookCoroutine);
            }
            // 기존의 줌 코루틴이 있으면 중지
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }
            // 노트 활성화 및 줌 아웃 코루틴 시작
            notebookCoroutine = StartCoroutine(ActivateNotebookAndZoomOut());
        }

        // 시계 바늘 회전
        RotateClockHand();

        // playBar가 활성화된 상태에서 클릭되면 씬 전환
        if (isPlayBarActive && Input.GetMouseButtonDown(0))
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(ActivateFadeinoutAndLoadScene());
        }
    }

    // 페이드 인 및 씬 전환 코루틴
    IEnumerator ActivateFadeinoutAndLoadScene()
    {
        yield return StartCoroutine(ActivateFadeIn());
        SceneManager.LoadScene("IntroScene");
    }

    // 페이드 인
    IEnumerator ActivateFadeIn()
    {
        float duration = 0.5f; // 0.5초 동안 활성화
        float elapsedTime = 0f;

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

    // 노트 활성화 및 줌 아웃 코루틴
    IEnumerator ActivateNotebookAndZoomOut()
    {
        isActivatingNotebook = true;

        // 노트 오브젝트를 활성화
        yield return StartCoroutine(ActivateNotebook());

        // 카메라 줌 아웃
        zoomCoroutine = StartCoroutine(ZoomOutCoroutine());

        isActivatingNotebook = false;
    }

    // 노트 활성화 코루틴
    IEnumerator ActivateNotebook()
    {
        float duration = 1.5f; // 1.5초 동안 활성화
        float elapsedTime = 0f;

        if (isNotebookActivate == false)
        {
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                Color color = noteSpr.color;
                color.a = progress; // 투명도 조절
                noteSpr.color = color;
                yield return null;
            }

            Color finalColor = noteSpr.color;
            finalColor.a = 1;
            noteSpr.color = finalColor;

            isNotebookActivate = true;
        }
    }

    // 카메라 줌 아웃 코루틴
    IEnumerator ZoomOutCoroutine()
    {
        isZooming = true;
        float elapsedTime = 0f;
        float startSize = mainCamera.orthographicSize;
        float targetSize = zoomTargetSize;
        Vector3 startPosition = mainCamera.transform.position;
        Vector3 targetPosition = new Vector3(0, 0, -10); // 카메라의 기본 위치를 Z축 -10으로 가정

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

        // 줌 아웃 애니메이션 완료 후 선 애니메이션 시작
        StartCoroutine(AnimateLines());
    }

    // 오브젝트 위치 변경 코루틴
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

    // 라인 애니메이션 코루틴과 playBar 활성화
    IEnumerator AnimateLines()
    {
        // 모든 라인 애니메이션을 동시에 시작
        Coroutine[] lineCoroutines = new Coroutine[5];
        lineCoroutines[0] = StartCoroutine(AnimateLine(firstLine, firstLinePositions));
        lineCoroutines[1] = StartCoroutine(AnimateLine(secondLine, secondLinePositions));
        lineCoroutines[2] = StartCoroutine(AnimateLine(thirdLine, thirdLinePositions));
        lineCoroutines[3] = StartCoroutine(AnimateLine(fourthLine, fourthLinePositions));
        lineCoroutines[4] = StartCoroutine(AnimateLine(fifthLine, fifthLinePositions));

        // 모든 라인 애니메이션이 완료될 때까지 기다림
        foreach (Coroutine lineCoroutine in lineCoroutines)
        {
            yield return lineCoroutine;
        }

        // 모든 라인 애니메이션이 완료된 후 playBar 활성화
        ActivatePlayBar();
    }

    // 라인 애니메이션 코루틴
    IEnumerator AnimateLine(LineRenderer line, Vector3[] positions)
    {
        float duration = 2.0f; // 2초 동안 애니메이션
        float halfDuration = duration / 2f;
        float elapsedTime = 0f;

        // 시작점에서 중간점까지 애니메이션
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDuration;
            Vector3 newPosition = Vector3.Lerp(positions[0], positions[1], EaseInSine(t));
            line.SetPosition(1, newPosition);
            line.SetPosition(2, newPosition); // 중간점까지는 끝점도 같이 업데이트
            yield return null;
        }

        // 중간점에서 끝점까지 애니메이션
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDuration;
            Vector3 newPosition = Vector3.Lerp(positions[1], positions[2], EaseInSine(t));
            line.SetPosition(2, newPosition);
            yield return null;
        }

        // 최종 위치 설정
        line.SetPosition(2, positions[2]);
    }

    // playBar 깜박거림 애니메이션 코루틴

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



    // 라인 초기 위치 설정 함수
    void SetLineInitialPositions(LineRenderer line, Vector3[] positions)
    {
        line.SetPosition(0, positions[0]);
        line.SetPosition(1, positions[0]);
        line.SetPosition(2, positions[0]);
    }

    // EaseOutCubic 함수 (부드러운 감속)
    float EaseOutCubic(float t)
    {
        t--;
        return t * t * t + 1;
    }

    // EaseOutQuart 함수 (더 부드러운 감속)
    float EaseOutQuart(float t)
    {
        t--;
        return 1 - t * t * t * t;
    }

    // EaseInSine 함수 (부드러운 가속)
    float EaseInSine(float t)
    {
        return 1 - Mathf.Cos((t * Mathf.PI) / 2);
    }

    // playBar 활성화 함수
    void ActivatePlayBar()
    {
        playBar.SetActive(true);
        playBarBar.SetActive(true);
        playBar_1.SetActive(true);
        playBar_2.SetActive(true);
        isPlayBarActive = true;

        StartCoroutine(BlinkPlayBars(playBar, playBar_1, playBar_2));
        StartCoroutine(MoveAndBlinkPlayBars(playBar_1, playBar_2, playBar_1.transform.position, playBar_2.transform.position, 1f)); // 왕복 1초로 설정
    }



    // 시계 바늘 회전 함수
    void RotateClockHand()
    {
        if (clockHand != null)
        {
            float angle = rotationSpeed * Time.deltaTime;
            clockHand.transform.Rotate(Vector3.forward, -angle); // 시계 방향으로 회전
        }
        else
        {
            Debug.LogError("Clock Hand is not assigned.");
        }

        if (minuteHand != null)
        {
            float minuteAngle = minuteRotationSpeed * Time.deltaTime;
            minuteHand.transform.Rotate(Vector3.forward, -minuteAngle); // 시계 방향으로 회전
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

            // 벌려질 때
            while (elapsedTime < duration )
            {
                elapsedTime += Time.deltaTime;
                float t = EaseInOutCubic(elapsedTime / (duration / 2));
                playBar1.transform.position = Vector3.Lerp(middle1, end1, t);
                playBar2.transform.position = Vector3.Lerp(middle2, end2, t);

                yield return null;
            }

            // 다시 좁아질 때
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