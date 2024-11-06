using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour
{
    public GameObject clickEventCircle;
    float spawnsTime;
    public float defaultTime = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)&&spawnsTime >= defaultTime)
        {
            CircleCreate();
        }
        spawnsTime += Time.deltaTime;
            
    }

    void CircleCreate()
    {
        Vector3 mPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mPosition.z = 0;
        Instantiate(clickEventCircle, mPosition, Quaternion.identity);

    }
    
    
}
