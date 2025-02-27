using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageDataLoader : MonoBehaviour
{
    /*public Text introStoryText;  // 인트로 스토리 UI
    public Text hintsText;       // 힌트 UI

    private StageData stageData;
    private string dataPath;

    public int selectedEpisode = 1; // 선택한 에피소드

    void Start()
    {
        dataPath = Path.Combine(Application.dataPath, "JsonData/EpData/Stage1.json");
        LoadStageData();
        DisplayEpisodeData(selectedEpisode);
    }

    /// <summary>
    /// JSON에서 데이터 불러오기
    /// </summary>
    void LoadStageData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            stageData = JsonUtility.FromJson<StageData>(json);
            Debug.Log("Stage data loaded successfully.");
        }
        else
        {
            Debug.LogWarning("Stage data not found! Initializing default data...");

        }
    }



    /// <summary>
    /// 선택한 에피소드 데이터 표시
    /// </summary>
    public void DisplayEpisodeData(int episodeNum)
    {
        if (stageData == null || stageData.episodes == null)
        {
            Debug.LogError("에피소드 데이터가 없습니다.");
            return;
        }

        EpisodeData selectedEpisode = stageData.episodes.Find(ep => ep.episode == episodeNum);
        if (selectedEpisode != null)
        {
            introStoryText.text = string.Join("\n", selectedEpisode.intro_story);
            hintsText.text = string.Join("\n", selectedEpisode.hints);
        }
        else
        {
            Debug.LogWarning($"에피소드 {episodeNum} 데이터를 찾을 수 없습니다.");
        }
    }*/
}