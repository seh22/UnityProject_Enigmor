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
        // ���� ���õ� ���Ǽҵ� Ŭ����
        DataManager.Instance.MarkEpisodeCleared(stage, episode);
        SceneManager.LoadScene("ChapterScene");

    }
}
