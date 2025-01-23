using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundScrollbarScript : MonoBehaviour
{
    
    [SerializeField] private AudioMixer audioMixer;

    // UI 요소: 배경음 볼륨 스크롤바
    [SerializeField] private Slider backgroundSlider;

    // UI 요소: 효과음 볼륨 스크롤바
    [SerializeField] private Slider effectSlider;

    // UI 요소: 진동 슬라이더 (0: 꺼짐, 1: 켜짐)
    [SerializeField] private Slider vibrationSlider;

    private void Start()
    {
        // SoundManager에서 저장된 값을 로드, 스크롤바 및 진동 슬라이더 초기화
        if (SoundManager.Instance != null)
        {
            backgroundSlider.value = SoundManager.Instance.BackgroundVolume;
            effectSlider.value = SoundManager.Instance.EffectVolume;
            vibrationSlider.value = SoundManager.Instance.VibrationEnabled ? 1.0f : 0.0f;
        }

        // 스크롤바 및 진동 슬라이더 값 변경
        backgroundSlider.onValueChanged.AddListener(OnBackgroundVolumeChanged);
        effectSlider.onValueChanged.AddListener(OnEffectVolumeChanged);
        vibrationSlider.onValueChanged.AddListener(OnVibrationValueChanged);

        audioMixer.SetFloat("BGM", Mathf.Log10(backgroundSlider.value) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(effectSlider.value) * 20);
        // 진동 추가시 여기에 값 변경
    }

    // 배경음 스크롤바 값이 변경되었을 때 호출되는 메서드
    private void OnBackgroundVolumeChanged(float value)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SaveBackgroundVolume(value);
            audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
        }
    }

    // 효과음 스크롤바 값이 변경되었을 때 호출되는 메서드
    private void OnEffectVolumeChanged(float value)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SaveEffectVolume(value);
            audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        }
    }

    // 진동 슬라이더 값이 변경되었을 때 호출되는 메서드
    private void OnVibrationValueChanged(float value)
    {
        if (SoundManager.Instance != null)
        {
            bool isVibrationOn = Mathf.Approximately(value, 1.0f); // 슬라이더 값이 1이면 true, 0이면 false
            SoundManager.Instance.ToggleVibration(isVibrationOn);
        }
    }
}
