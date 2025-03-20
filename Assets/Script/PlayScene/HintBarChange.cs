using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBarChange : MonoBehaviour
{
    public GameObject Npc;
    public GameObject Weapon;
    public GameObject Spot;
    public GameObject TextHint;
    GameObject clickedButton;
    private UiController uiController;
    // Update is called once per frame

    private void Start()
    {
        uiController = FindObjectOfType<UiController>();
    }
    public void HintObjOnClick(GameObject button)
    {
        if(uiController.UiLeve==0)
        {
            clickedButton = button;

            Npc.SetActive(false);
            Weapon.SetActive(false);
            Spot.SetActive(false);
            TextHint.SetActive(false);

            clickedButton.SetActive(true);
        }
        
    }

    
}
