using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Episode
{
    public int episode;
    public bool cleared;
}

[System.Serializable]
public class Stage
{
    public int stage;
    public List<Episode> episodes;
}

[System.Serializable]
public class GameData
{
    public List<Stage> stages;
}


public class DataManager : Singleton<DataManager>
{
    public GameData gameData; // ���� �����͸� �޸𸮿��� ����
    public int selectedStage; // ������ ��������
    public int selectedEpisode; // ������ ���Ǽҵ�

    private string dataPath;

    public override void Awake()
    {
        base.Awake(); // Singleton Awake ȣ��

        dataPath = Path.Combine(Application.dataPath, "JsonData/StageData.json");

        LoadGameData();
    }


    /// <summary>
    /// ���� �����͸� ����
    /// </summary>
    public void SaveGameData()
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(dataPath, json);
        Debug.Log($"Game data saved to: {dataPath}");
    }


    /// <summary>
    /// ���� �����͸� �ε�
    /// </summary>
    public void LoadGameData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            gameData = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game data loaded successfully.");
        }
        else
        {
            // ������ ������ �⺻ ������ �ʱ�ȭ
            InitializeDefaultData();
            SaveGameData();
        }
    }

    /// <summary>
    /// �⺻ �����͸� �ʱ�ȭ
    /// </summary>
    private void InitializeDefaultData()
    {
        gameData = new GameData
        {
            stages = new List<Stage>
            {
                new Stage { stage = 1, episodes = CreateEpisodes(15) },
                new Stage { stage = 2, episodes = CreateEpisodes(15) },
                new Stage { stage = 3, episodes = CreateEpisodes(15) },
            }
        };
    }

    /// <summary>
    /// Ư�� ���� ���Ǽҵ带 ����
    /// </summary>
    private List<Episode> CreateEpisodes(int count)
    {
        var episodes = new List<Episode>();
        for (int i = 1; i <= count; i++)
        {
            episodes.Add(new Episode { episode = i, cleared = false });
        }
        return episodes;
    }

    /// <summary>
    /// Ư�� ���Ǽҵ带 Ŭ���� ó��
    /// </summary>
    public void MarkEpisodeCleared(int stage, int episode)
    {
        var targetStage = gameData.stages.Find(s => s.stage == stage);
        if (targetStage != null)
        {
            var targetEpisode = targetStage.episodes.Find(e => e.episode == episode);
            if (targetEpisode != null)
            {
                targetEpisode.cleared = true;
                Debug.Log("Ep" + targetEpisode.cleared);
                SaveGameData(); // Ŭ���� ���� ����
                
            }
        }
    }

    /// <summary>
    /// Ư�� ���Ǽҵ尡 Ŭ���� �Ǿ����� Ȯ��
    /// </summary>
    public bool IsEpisodeCleared(int stage, int episode)
    {
        var targetStage = gameData.stages.Find(s => s.stage == stage);
        return targetStage?.episodes.Find(e => e.episode == episode)?.cleared ?? false;
    }

    /// <summary>
    /// ���� ��������, ���Ǽҵ�
    /// </summary>
    public void SetSelectedEpisode(int stage, int episode)
    {
        selectedStage = stage;
        selectedEpisode = episode;
    }

}



