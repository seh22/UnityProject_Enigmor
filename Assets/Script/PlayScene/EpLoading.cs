using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EpLoading : MonoBehaviour
{
    public void OnClickLoadingButton()
    {
        int stage = DataManager.Instance.selectedStage;
        int episode = DataManager.Instance.selectedEpisode;

        Debug.Log(stage + "   " + episode);
        // 현재 선택된 에피소드 클리어
        DataManager.Instance.MarkEpisodeCleared(stage, episode);
        SceneManager.LoadScene("ChapterScene");

    }
}
