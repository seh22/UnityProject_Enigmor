using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //BGM ������
    public enum EBgm
    {
        BGM_TITLE,
        BGM_GAME
    }

    //SFX ������
    public enum ESfx
    {
        SFX_BUTTON,
        SFX_ENDING,
        SFX_TOKEN,
        SFX_BOTTLE,
        SFX_OPENDOOR,
        SFX_MissionClear
    }

    //audio clip ���� �� �ִ� �迭
    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] sfxs;

    //�÷����ϴ� AudioSource
    [SerializeField] AudioSource audioBgm;
    [SerializeField] AudioSource audioSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // EBgm �������� �Ű������� �޾� �ش��ϴ� ��� ���� Ŭ���� ���
    public void PlayBGM(EBgm bgmIdx)
    {
        //enum int������ ����ȯ ����
        audioBgm.clip = bgms[(int)bgmIdx];
        audioBgm.Play();
    }

    // ���� ��� ���� ��� ���� ����
    public void StopBGM()
    {
        audioBgm.Stop();
    }

    // ESfx �������� �Ű������� �޾� �ش��ϴ� ȿ���� Ŭ���� ���
    public void PlaySFX(ESfx esfx)
    {
        audioSfx.PlayOneShot(sfxs[(int)esfx]);
    }

}
