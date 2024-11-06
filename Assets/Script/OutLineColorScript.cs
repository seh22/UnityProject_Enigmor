using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutLineColorScript : MonoBehaviour
{
    private Image outLineColor;

    void Start()
    {
        outLineColor = GetComponent<Image>();
    }

    public void ChangeColor(Color OutLineColor)
    {
        outLineColor.color = OutLineColor;
    }

    public void ImageEnable()
    {
        outLineColor.enabled = false;
    }

}
