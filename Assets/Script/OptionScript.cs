using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour
{
    public bool optionBarIsTrue;
    private bool toggle = false;
    public GameObject OptionBar;
    // Start is called before the first frame update
    void Start()
    {
        optionBarIsTrue = false;
        OptionBar.SetActive(false);
    }

    public void OptionBarOnClick()
    {
        OptionBar.SetActive(true);
        optionBarIsTrue = true;
    }

    public void OptionBarClose()
    {
        OptionBar.SetActive(false);
        optionBarIsTrue= false;
    }

    public void ONToggleClick()
    {
        if(toggle == false)
        {

        }
    }

}
