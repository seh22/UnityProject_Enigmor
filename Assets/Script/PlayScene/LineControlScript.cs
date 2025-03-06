using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineControlScript : MonoBehaviour
{

    private LineRenderer Line;
    private int lineLevel;

    // Start is called before the first frame update
    void Start()
    {
        Line = GetComponent<LineRenderer>();
        
        Line.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LineConnect()
    {
        if (gameObject.CompareTag("Renderer1"))
        {

        }
        else
        {

        }

    }

}
