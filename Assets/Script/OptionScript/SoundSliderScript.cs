using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundScrollbarScript : MonoBehaviour
{
    
    [SerializeField] private AudioMixer audioMixer;

    // UI ���: ����� ���� ��ũ�ѹ�
    [SerializeField] private Slider backgroundSlider;

    // UI ���: ȿ���� ���� ��ũ�ѹ�
    [SerializeField] private Slider effectSlider;

    // UI ���: ���� �����̴� (0: ����, 1: ����)
    [SerializeField] private Slider vibrationSlider;

    private void Start()
    {
        // SoundManager���� ����� ���� �ε�, ��ũ�ѹ� �� ���� �����̴� �ʱ�ȭ
        if (SoundManager.Instance != null)
        {
            backgroundSlider.value = SoundManager.Instance.BackgroundVolume;
            effectSlider.value = SoundManager.Instance.EffectVolume;
            vibrationSlider.value = SoundManager.Instance.VibrationEnabled ? 1.0f : 0.0f;
        }

        // ��ũ�ѹ� �� ���� �����̴� �� ����
        backgroundSlider.onValueChanged.AddListener(OnBackgroundVolumeChanged);
        effectSlider.onValueChanged.AddListener(OnEffectVolumeChanged);
        vibrationSlider.onValueChanged.AddListener(OnVibrationValueChanged);

        audioMixer.SetFloat("BGM", Mathf.Log10(backgroundSlider.value) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(effectSlider.value) * 20);
        // ���� �߰��� ���⿡ �� ����
    }

    // ����� ��ũ�ѹ� ���� ����Ǿ��� �� ȣ��Ǵ� �޼���
    private void OnBackgroundVolumeChanged(float value)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SaveBackgroundVolume(value);
            audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
        }
    }

    // ȿ���� ��ũ�ѹ� ���� ����Ǿ��� �� ȣ��Ǵ� �޼���
    private void OnEffectVolumeChanged(float value)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SaveEffectVolume(value);
            audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        }
    }

    // ���� �����̴� ���� ����Ǿ��� �� ȣ��Ǵ� �޼���
    private void OnVibrationValueChanged(float value)
    {
        if (SoundManager.Instance != null)
        {
            bool isVibrationOn = Mathf.Approximately(value, 1.0f); // �����̴� ���� 1�̸� true, 0�̸� false
            SoundManager.Instance.ToggleVibration(isVibrationOn);
        }
    }
}
