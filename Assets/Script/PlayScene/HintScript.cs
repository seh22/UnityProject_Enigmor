using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class HintScript : MonoBehaviour
{
    public bool hintObjIsTure;
    public GameObject hintObj;
    void Start()
    {
        hintObjIsTure = false;
        hintObj.SetActive(false);
        Debug.Log(hintObjIsTure);
    }

    public void HintObjOnClick()
    {
        if(hintObjIsTure == true)
        {
            hintObj.SetActive(false);
            hintObjIsTure = false;
        }
    }

    public void HintTriggerClick()
    {
        if(hintObjIsTure == false)
        {
            hintObj.SetActive(true);
            hintObjIsTure = true;
        }
    }

    public void ChoiceButtonClick()
    {
        hintObj.SetActive(true);
    }
}
