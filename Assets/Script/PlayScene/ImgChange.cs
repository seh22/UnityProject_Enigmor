using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgChange : MonoBehaviour
{
    public List<StageImgData> stageDataList;

    [SerializeField]
    private Image image;

    void Start()
    {
        int stage = DataManager.Instance.selectedStage;
        int episode = DataManager.Instance.selectedEpisode;
        Debug.Log("stage : " + (stage+1) + "ep : " + (episode+1));

        UpdateUI(stage, episode);
    }

    private void UpdateUI(int stage, int episode)
    {
        EpisodeImgData episodeData = stageDataList[stage].episodes[episode];
        if (episodeData==null)
        {
            episodeData = stageDataList[0].episodes[0];
            Debug.Log("not Update, Stage1 - ep1");   
        }
       
        image.sprite = episodeData.episodeSprite;
    }
}
