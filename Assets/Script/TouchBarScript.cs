/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMesh Pro를 사용하기 위해 추가
using UnityEngine.UI;

public class TouchBarScript : MonoBehaviour
{
    public GameObject playBar_1;
    public GameObject playBar_2;

    public GameObject playBar;
    public GameObject playBarBar;
    bool isPlayBarActive = true;

    public Slider fadeDurationSlider;
    public Slider movementDurationSlider;

    public TMP_InputField fadeDurationInput; // TMP_InputField 추가
    public TMP_InputField movementDurationInput; // TMP_InputField 추가

    private float blinkDuration = 1.0f;
    private float movementDuration = 1.0f;

    private Coroutine moveAndBlinkCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        fadeDurationSlider.onValueChanged.AddListener(UpdateFadeDuration);
        movementDurationSlider.onValueChanged.AddListener(UpdateMovementDuration);

        fadeDurationInput.onEndEdit.AddListener(UpdateFadeDurationFromInput);
        movementDurationInput.onEndEdit.AddListener(UpdateMovementDurationFromInput);

        StartCoroutine(BlinkPlayBars(playBar, playBar_1, playBar_2));
        moveAndBlinkCoroutine = StartCoroutine(MoveAndBlinkPlayBars(playBar_1, playBar_2,
            playBar_1.transform.position, playBar_2.transform.position, movementDuration));
    }

    float EaseInOutCubic(float t)
    {
        return t < 0.5 ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
    }

    void UpdateFadeDuration(float value)
    {
        blinkDuration = value;
        fadeDurationInput.text = value.ToString(); // Slider 값 변경 시 InputField 업데이트
    }

    void UpdateMovementDuration(float value)
    {
        movementDuration = value;
        movementDurationInput.text = value.ToString(); // Slider 값 변경 시 InputField 업데이트

        if (moveAndBlinkCoroutine != null)
        {
            StopCoroutine(moveAndBlinkCoroutine);
        }
        moveAndBlinkCoroutine = StartCoroutine(MoveAndBlinkPlayBars(playBar_1, playBar_2,
            playBar_1.transform.position, playBar_2.transform.position, movementDuration));
    }

    void UpdateFadeDurationFromInput(string value)
    {
        if (float.TryParse(value, out float result))
        {
            blinkDuration = result;
            fadeDurationSlider.value = result; // InputField 값 변경 시 Slider 업데이트
        }
    }

    void UpdateMovementDurationFromInput(string value)
    {
        if (float.TryParse(value, out float result))
        {
            movementDuration = result;
            movementDurationSlider.value = result; // InputField 값 변경 시 Slider 업데이트

            if (moveAndBlinkCoroutine != null)
            {
                StopCoroutine(moveAndBlinkCoroutine);
            }
            moveAndBlinkCoroutine = StartCoroutine(MoveAndBlinkPlayBars(playBar_1, playBar_2,
                playBar_1.transform.position, playBar_2.transform.position, movementDuration));
        }
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

            // Move apart
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = EaseInOutCubic(elapsedTime / (duration / 2));
                playBar1.transform.position = Vector3.Lerp(middle1, end1, t);
                playBar2.transform.position = Vector3.Lerp(middle2, end2, t);

                yield return null;
            }

            // Move together
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

    IEnumerator BlinkPlayBars(params GameObject[] playBars)
    {
        isPlayBarActive = true;

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
}
*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMesh Pro를 사용하기 위해 추가
using UnityEngine.UI;

public class TouchBarScript : MonoBehaviour
{
    public GameObject playBar_1;
    public GameObject playBar_2;

    public GameObject playBar;
    public GameObject playBarBar;
    bool isPlayBarActive = true;

    private float blinkDuration = 1.4f; // fadeDurationSlider 값을 1.4로 고정
    private float movementDuration = 1.5f; // movementDurationSlider 값을 1.5로 고정

    private Coroutine moveAndBlinkCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlinkPlayBars(playBar, playBar_1, playBar_2));
        moveAndBlinkCoroutine = StartCoroutine(MoveAndBlinkPlayBars(playBar_1, playBar_2,
            playBar_1.transform.position, playBar_2.transform.position, movementDuration));
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

            // Move apart
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = EaseInOutCubic(elapsedTime / (duration / 2));
                playBar1.transform.position = Vector3.Lerp(middle1, end1, t);
                playBar2.transform.position = Vector3.Lerp(middle2, end2, t);

                yield return null;
            }

            // Move together
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

    IEnumerator BlinkPlayBars(params GameObject[] playBars)
    {
        isPlayBarActive = true;

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
}
*/

using System.Collections;
using UnityEngine;

public class TouchBarScript : MonoBehaviour
{
    public GameObject playBar_1;
    public GameObject playBar_2;
    public GameObject playBar;

    private float blinkDuration = 1.4f; // fadeDurationSlider 값을 1.4로 고정
    private float movementDuration = 1.5f; // movementDurationSlider 값을 1.5로 고정

    private Coroutine moveAndBlinkCoroutine;
    private Coroutine blinkCoroutine;

    private bool isPlayBarActive = true;

    void OnEnable()
    {
        StartPlayBarAnimations();
    }

    void OnDisable()
    {
        StopPlayBarAnimations();
    }

    void StartPlayBarAnimations()
    {
        blinkCoroutine = StartCoroutine(BlinkPlayBars(playBar, playBar_1, playBar_2));
        moveAndBlinkCoroutine = StartCoroutine(MoveAndBlinkPlayBars(playBar_1, playBar_2,
            playBar_1.transform.position, playBar_2.transform.position, movementDuration));
    }

    void StopPlayBarAnimations()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        if (moveAndBlinkCoroutine != null)
        {
            StopCoroutine(moveAndBlinkCoroutine);
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

            // Move apart
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = EaseInOutCubic(elapsedTime / (duration / 2));
                playBar1.transform.position = Vector3.Lerp(middle1, end1, t);
                playBar2.transform.position = Vector3.Lerp(middle2, end2, t);

                yield return null;
            }

            // Move together
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

    IEnumerator BlinkPlayBars(params GameObject[] playBars)
    {
        isPlayBarActive = true;

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
}
