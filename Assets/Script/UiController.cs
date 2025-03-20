using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public int UiLeve;

    void Start()
    {
        //Ui level0 == 첫 시작
        //Ui level1 == Option창
        //Ui level2 == Ep 창, Choice 창 
        //UI level3 == ?
        UiLeve = 0;
    }

}
