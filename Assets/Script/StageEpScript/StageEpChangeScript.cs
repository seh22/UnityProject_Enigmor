using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageEpChangeScript : MonoBehaviour
{
    [SerializeField]
    private GameObject epBarObject;
    [SerializeField]
    private ScrollRect epBar;

    public void OnClickStageButton()
    {
        

        if (epBar != null)
        {
            epBar.verticalNormalizedPosition = 1f;
        }
        epBarObject.SetActive(true);
    }

    public void OnClickCloseButton()
    {
        epBarObject.SetActive(false);
    }
}
