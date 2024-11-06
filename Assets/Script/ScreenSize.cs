using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake() 
    {
        Screen.SetResolution(720, 1280, false);
    }
}
