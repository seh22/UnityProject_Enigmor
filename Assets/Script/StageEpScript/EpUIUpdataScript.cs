using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EpUIUpdataScript : MonoBehaviour
{
    [SerializeField]
    private int stageNum; // �������� ��ȣ (1, 2, 3 ��)
    [SerializeField]
    private int epNum;
    [SerializeField]
    private GameObject clearImg;

    
    void Start()
    {
        UpdateEpisodeClearStatus();
    }


    /// <summary>
    /// �� ���������� ��� ���Ǽҵ� Ŭ���� ���ο� ���� Clear �̹����� ������Ʈ
    /// </summary>
    private void UpdateEpisodeClearStatus()
    {
        
        if (clearImg != null)
        {
            bool isCleared = DataManager.Instance.IsEpisodeCleared(stageNum, epNum);
            clearImg.SetActive(isCleared);
        }
    }

    public void OnClickEpButton()
    {
        DataManager.Instance.SetSelectedEpisode(stageNum-1,epNum-1);
        Debug.Log(stageNum + "//" + epNum);
        SceneManager.LoadScene("PlayScene");
    }
}
