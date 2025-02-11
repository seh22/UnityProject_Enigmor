using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpUIUpdataScript : MonoBehaviour
{
    [SerializeField]
    private int stageNum; // 스테이지 번호 (1, 2, 3 등)
    [SerializeField]
    private int epNum;

    [SerializeField]
    private GameObject clearImg;

    
    void Start()
    {
        UpdateEpisodeClearStatus();
    }


    /// <summary>
    /// 이 스테이지의 모든 에피소드 클리어 여부에 따라 Clear 이미지를 업데이트
    /// </summary>
    private void UpdateEpisodeClearStatus()
    {
        
        if (clearImg != null)
        {
            bool isCleared = DataManager.Instance.IsEpisodeCleared(stageNum, epNum);
            clearImg.SetActive(isCleared);
        }
    }

    public void OnClickCloseButton()
    {
        
    }
}
