using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour
{
    private bool toggle = false;
    public GameObject OptionBar;
    // Start is called before the first frame update
    void Start()
    {
        OptionBar.SetActive(false);
    }

    public void OptionBarOnClick()
    {
        OptionBar.SetActive(true);
    }

    public void OptionBarClose()
    {
        OptionBar.SetActive(false);
    }

    public void ONToggleClick()
    {
        if(toggle == false)
        {

        }
    }

}
