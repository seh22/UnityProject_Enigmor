using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Christina.UI
{
    public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
    {
        [Header("Slider Setup")]
        [SerializeField, Range(0, 1f)]
        protected float sliderValue; // 슬라이더 값 (0과 1 사이)
        public bool CurrentValue { get; private set; } // 현재 토글 상태

        private bool _previousValue; // 이전 토글 상태
        private Slider _slider; // Unity의 슬라이더 UI 컴포넌트

        [Header("Animation")]
        [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f; // 애니메이션 지속 시간
        [SerializeField]
        private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1); // 애니메이션 곡선

        private Coroutine _animateSliderCoroutine; // 슬라이더 애니메이션 코루틴

        [Header("Events")]
        [SerializeField] private UnityEvent onToggleOn; // 토글이 켜질 때 이벤트
        [SerializeField] private UnityEvent onToggleOff; // 토글이 꺼질 때 이벤트

        protected Action transitionEffect; // 전환 효과를 위한 액션

        protected virtual void OnValidate()
        {
            // 에디터에서 값이 변경될 때마다 슬라이더 설정 업데이트
            SetupToggleComponents();

            _slider.value = sliderValue;
        }

        private void SetupToggleComponents()
        {
            if (_slider != null)
                return;

            SetupSliderComponent(); // 슬라이더 컴포넌트 설정
        }

        private void SetupSliderComponent()
        {
            _slider = GetComponent<Slider>(); // 슬라이더 컴포넌트 가져오기

            if (_slider == null)
            {
                Debug.Log("No slider found!", this); // 슬라이더가 없으면 로그 출력
                return;
            }

            _slider.interactable = false; // 슬라이더를 조작할 수 없게 설정
            var sliderColors = _slider.colors;
            sliderColors.disabledColor = Color.white; // 비활성화 색상 설정
            _slider.colors = sliderColors;
            _slider.transition = Selectable.Transition.None; // 전환 효과 없음
        }

        protected virtual void Awake()
        {
            SetupSliderComponent(); // 컴포넌트가 생성될 때 슬라이더 설정
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Toggle(); // 클릭할 때 토글
        }

        private void Toggle()
        {
            SetStateAndStartAnimation(!CurrentValue); // 현재 상태 반대로 변경
        }

        private void SetStateAndStartAnimation(bool state)
        {
            _previousValue = CurrentValue;
            CurrentValue = state; // 현재 토글 상태 설정

            // 상태가 변경되었을 때 이벤트 호출
            if (_previousValue != CurrentValue)
            {
                if (CurrentValue)
                    onToggleOn?.Invoke();
                else
                    onToggleOff?.Invoke();
            }

            // 기존 애니메이션 코루틴이 있으면 중단
            if (_animateSliderCoroutine != null)
                StopCoroutine(_animateSliderCoroutine);

            // 슬라이더 애니메이션 시작
            _animateSliderCoroutine = StartCoroutine(AnimateSlider());
        }

        private IEnumerator AnimateSlider()
        {
            float startValue = _slider.value; // 시작 슬라이더 값
            float endValue = CurrentValue ? 1 : 0; // 목표 슬라이더 값 (토글 상태에 따라 0 또는 1)

            float time = 0;
            if (animationDuration > 0)
            {
                // 애니메이션 진행
                while (time < animationDuration)
                {
                    time += Time.deltaTime;

                    float lerpFactor = slideEase.Evaluate(time / animationDuration); // 애니메이션 곡선에 따른 보간
                    _slider.value = sliderValue = Mathf.Lerp(startValue, endValue, lerpFactor);

                    transitionEffect?.Invoke(); // 전환 효과 호출

                    yield return null;
                }
            }

            _slider.value = endValue; // 최종 슬라이더 값 설정
        }



    }
}
