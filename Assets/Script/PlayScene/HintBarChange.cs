using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBarChange : MonoBehaviour
{
    public GameObject Npc;
    public GameObject Weapon;
    public GameObject Location;
    public GameObject TextHint;
    GameObject clickedButton;

    // Update is called once per frame
    public void HintObjOnClick(GameObject button)
    {
        clickedButton = button;

        Npc.SetActive(false);
        Weapon.SetActive(false);
        Location.SetActive(false);
        TextHint.SetActive(false);

        clickedButton.SetActive(true);
    }

    
}
