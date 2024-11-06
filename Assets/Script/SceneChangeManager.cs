using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{

    

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "ChapterScene")
        {

            if (Input.GetMouseButtonUp(0) && !ScrollDetector.isScrolling)
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    SceneManager.LoadScene("EpScene");
                }
            }
        }
        else if(SceneManager.GetActiveScene().name == "EpScene")
        {
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit2D hit = Physics2D.Raycast
                    (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if ((hit.collider != null) && (hit.collider.gameObject == gameObject))
                {
                    SceneManager.LoadScene("ChapterScene");
                }
            }
        }

    }


    /*    void Update()
        {
            if(Input.GetMouseButtonUp(0))
            {
                RaycastHit2D hit = Physics2D.Raycast
                    (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if((hit.collider != null) && (hit.collider.gameObject == gameObject))
                {
                    SceneManager.LoadScene("EpScene");
                }
            }
        }*/
    /*
        public void ChapterSceneChange()
        {
            SceneManager.LoadScene("ChapterScene");
        }
        public void EpSceneChange()
        {
            SceneManager.LoadScene("EpScene");
        }*/
}
