using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour
{
    public bool optionBarIsTrue;
    private bool toggle = false;
    public GameObject OptionBar;
    public UiController uiController;
    private int previousUiLevel;
    // Start is called before the first frame update
    void Start()
    {
        optionBarIsTrue = false;
        OptionBar.SetActive(false);
        uiController = FindObjectOfType<UiController>();
        previousUiLevel = 0;
    }

    public void OptionBarOnClick()
    {
        OptionBar.SetActive(true);
        optionBarIsTrue = true;
        previousUiLevel = uiController.UiLeve;
        uiController.UiLeve = 1;

    }

    public void OptionBarClose()
    {
        OptionBar.SetActive(false);
        optionBarIsTrue= false;
        uiController.UiLeve = previousUiLevel;
    }

    public void ONToggleClick()
    {
        if(toggle == false)
        {

        }
    }

}
