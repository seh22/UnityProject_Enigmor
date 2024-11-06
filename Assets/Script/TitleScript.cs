using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class TitleScript : MonoBehaviour
{
    Scene scene;
    string a;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        a = scene.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //home button click 클래스
    public void OnHoneButtonClick()
    {
        SceneCheack(a);
    }

    // Scene 변경 클래스
    private void SceneCheack(string objectName)
    {
        switch (a)
        {
            case "PlayScene":
                SceneManager.LoadScene("EpScene");
                scene = SceneManager.GetActiveScene();
                break;
            case "EpScene":
                SceneManager.LoadScene("ChapterScene");
                scene = SceneManager.GetActiveScene();
                break;
            case "ChapterScene":
                Debug.Log("esc");
                Application.Quit();
                break;
        }
    }



}
