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
        protected float sliderValue; // �����̴� �� (0�� 1 ����)
        public bool CurrentValue { get; private set; } // ���� ��� ����

        private bool _previousValue; // ���� ��� ����
        private Slider _slider; // Unity�� �����̴� UI ������Ʈ

        [Header("Animation")]
        [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f; // �ִϸ��̼� ���� �ð�
        [SerializeField]
        private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1); // �ִϸ��̼� �

        private Coroutine _animateSliderCoroutine; // �����̴� �ִϸ��̼� �ڷ�ƾ

        [Header("Events")]
        [SerializeField] private UnityEvent onToggleOn; // ����� ���� �� �̺�Ʈ
        [SerializeField] private UnityEvent onToggleOff; // ����� ���� �� �̺�Ʈ

        protected Action transitionEffect; // ��ȯ ȿ���� ���� �׼�

        protected virtual void OnValidate()
        {
            // �����Ϳ��� ���� ����� ������ �����̴� ���� ������Ʈ
            SetupToggleComponents();

            _slider.value = sliderValue;
        }

        private void SetupToggleComponents()
        {
            if (_slider != null)
                return;

            SetupSliderComponent(); // �����̴� ������Ʈ ����
        }

        private void SetupSliderComponent()
        {
            _slider = GetComponent<Slider>(); // �����̴� ������Ʈ ��������

            if (_slider == null)
            {
                Debug.Log("No slider found!", this); // �����̴��� ������ �α� ���
                return;
            }

            _slider.interactable = false; // �����̴��� ������ �� ���� ����
            var sliderColors = _slider.colors;
            sliderColors.disabledColor = Color.white; // ��Ȱ��ȭ ���� ����
            _slider.colors = sliderColors;
            _slider.transition = Selectable.Transition.None; // ��ȯ ȿ�� ����
        }

        protected virtual void Awake()
        {
            SetupSliderComponent(); // ������Ʈ�� ������ �� �����̴� ����
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Toggle(); // Ŭ���� �� ���
        }

        private void Toggle()
        {
            SetStateAndStartAnimation(!CurrentValue); // ���� ���� �ݴ�� ����
        }

        private void SetStateAndStartAnimation(bool state)
        {
            _previousValue = CurrentValue;
            CurrentValue = state; // ���� ��� ���� ����

            // ���°� ����Ǿ��� �� �̺�Ʈ ȣ��
            if (_previousValue != CurrentValue)
            {
                if (CurrentValue)
                    onToggleOn?.Invoke();
                else
                    onToggleOff?.Invoke();
            }

            // ���� �ִϸ��̼� �ڷ�ƾ�� ������ �ߴ�
            if (_animateSliderCoroutine != null)
                StopCoroutine(_animateSliderCoroutine);

            // �����̴� �ִϸ��̼� ����
            _animateSliderCoroutine = StartCoroutine(AnimateSlider());
        }

        private IEnumerator AnimateSlider()
        {
            float startValue = _slider.value; // ���� �����̴� ��
            float endValue = CurrentValue ? 1 : 0; // ��ǥ �����̴� �� (��� ���¿� ���� 0 �Ǵ� 1)

            float time = 0;
            if (animationDuration > 0)
            {
                // �ִϸ��̼� ����
                while (time < animationDuration)
                {
                    time += Time.deltaTime;

                    float lerpFactor = slideEase.Evaluate(time / animationDuration); // �ִϸ��̼� ��� ���� ����
                    _slider.value = sliderValue = Mathf.Lerp(startValue, endValue, lerpFactor);

                    transitionEffect?.Invoke(); // ��ȯ ȿ�� ȣ��

                    yield return null;
                }
            }

            _slider.value = endValue; // ���� �����̴� �� ����
        }



    }
}
