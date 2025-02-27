using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageDataLoader : MonoBehaviour
{
    /*public Text introStoryText;  // ��Ʈ�� ���丮 UI
    public Text hintsText;       // ��Ʈ UI

    private StageData stageData;
    private string dataPath;

    public int selectedEpisode = 1; // ������ ���Ǽҵ�

    void Start()
    {
        dataPath = Path.Combine(Application.dataPath, "JsonData/EpData/Stage1.json");
        LoadStageData();
        DisplayEpisodeData(selectedEpisode);
    }

    /// <summary>
    /// JSON���� ������ �ҷ�����
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
    /// ������ ���Ǽҵ� ������ ǥ��
    /// </summary>
    public void DisplayEpisodeData(int episodeNum)
    {
        if (stageData == null || stageData.episodes == null)
        {
            Debug.LogError("���Ǽҵ� �����Ͱ� �����ϴ�.");
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
            Debug.LogWarning($"���Ǽҵ� {episodeNum} �����͸� ã�� �� �����ϴ�.");
        }
    }*/
}