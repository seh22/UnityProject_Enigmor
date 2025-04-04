using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class LineConnect : MonoBehaviour
{
    [SerializeField] private GameObject fadeInOutObj;
    //public OutLineColorScript parentOutlineColorScript;

    // Leve1 -> Level2 연결 Renderer
    public LineRenderer redLine_1;
    public LineRenderer blueLine_1;
    public LineRenderer greenLine_1;

    // Leve2 -> Level3 연결 Renderer
    public LineRenderer redLine_2;
    public LineRenderer blueLine_2;
    public LineRenderer greenLine_2;

    // 클릭 Level 확인을 위한 변수
    public int LevelCheck;

    // 어떤 LineRenderer와 연결되어있는지(0 == null, 1 == red, 2 == green, 3 == blue), 아님 연결이 없는지 확인을 위한 리스트
    public int[,] colorCheck = new int[2, 3]; 

    // 클릭한 GameObject를 추적하기 위한 변수
    private GameObject selectedObject;

    // 그어지는 LineRenderer를 추척하기 위한 변수
    private LineRenderer activeLineRenderer;
    
    // Level2, Level3 GameObject를 저장하기 위한 리스트
    private List<GameObject> level2Objects;
    private List<GameObject> level3Objects;

    
    private GameObject[,] outLineObjects = new GameObject[2, 3];

    private RaycastHit2D hit;
    private GameObject targetObject;


    private UiController uiController;

    void Start()
    {
        // Level1 -> Level2 연결 LineRenderer 정점 초기화
        redLine_1.positionCount = 0;
        blueLine_1.positionCount = 0;
        greenLine_1.positionCount = 0;

        // Level2 -> Level3 연결 LineRenderer 정점 초기화
        redLine_2.positionCount = 0;
        blueLine_2.positionCount = 0;
        greenLine_2.positionCount = 0;

        // "Level2" 태그를 가진 오브젝트들을 찾고 이름 순으로 정렬하여 리스트에 저장
        level2Objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Level2")).OrderBy(obj => obj.name).ToList();

        // "Level3" 태그를 가진 오브젝트들을 찾고 이름 순으로 정렬하여 리스트에 저장
        level3Objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Level3")).OrderBy(obj => obj.name).ToList();


        // LineLevel2 태그를 가진 오브젝트를 배열 첫 번째 행에 저장
        GameObject[] lineLevel2Objects = GameObject.FindGameObjectsWithTag("LineLevel2")
            .OrderBy(obj => obj.name)   // 이름순으로 정렬
            .ToArray();

        for (int i = 0; i < 3; i++)
        {
            outLineObjects[0, i] = lineLevel2Objects[i];
        }

        // LineLevel3 태그를 가진 오브젝트를 배열 두 번째 행에 저장
        GameObject[] lineLevel3Objects = GameObject.FindGameObjectsWithTag("LineLevel3")
            .OrderBy(obj => obj.name)   // 이름순으로 정렬
            .ToArray(); 

        for (int i = 0; i < 3; i++)
        {
            outLineObjects[1, i] = lineLevel3Objects[i];
        }

        uiController = GetComponent<UiController>();

    }

    void Update()
    {
        
        int targetIndex = 0;    // 목표 Object의 인덱스
        int lineIndex = 0;  // 활성화된 LineRenderer의 인덱스

        if(uiController.UiLeve==0)
        {
            // 마우스를 클릭했을 때
            if (Input.GetMouseButtonDown(0))
            {
                // 마우스의 월드 좌표를 계산하고, 해당 위치의 오브젝트를 감지
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(mousePos, Vector2.zero);

                // 감지된 GameObject의 Tag가 Level1일 때
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Level1"))
                {
                    LevelCheck = 1; 
                    selectedObject = hit.collider.gameObject;
                    AssignLineRenderer_1(selectedObject.name);


                    // LineRenderer가 활성화 되어있을 때
                    if (activeLineRenderer != null)
                    {
                        //정점 2개 설정 및 위치 
                        activeLineRenderer.positionCount = 2;
                        activeLineRenderer.SetPosition(0, selectedObject.transform.position);
                        activeLineRenderer.SetPosition(1, selectedObject.transform.position);
                    }
                }

                //마우스를 Level2에서 클릭 했을 때
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Level2"))
                {
                    LevelCheck = 2; // LevelCheck 2로 변경(마우스 드롭다운 시 확인을 위한 변수)

                    // 감지된 오브젝트를 selectedObject로 설정
                    selectedObject = hit.collider.gameObject;

                    // targetIndex에 감지된 오브젝트에 연결된 LineRenderer색상 값 할당, 없을 경우 0
                    targetIndex = colorCheck[0, level2Objects.IndexOf(selectedObject)];

                    //오브젝트의 상위 연결이 있는경우
                    if (targetIndex != 0)
                    {
                        // 적절한 값에 따라 LineRenderer 할당
                        AssignLineRenderer_2(targetIndex);

                        // LineRenderer가 활성화 되어있을 때
                        if (activeLineRenderer != null)
                        {
                            //정점 2개 설정 및 위치 
                            activeLineRenderer.positionCount = 2;
                            activeLineRenderer.SetPosition(0, selectedObject.transform.position);
                            activeLineRenderer.SetPosition(1, selectedObject.transform.position);
                        }
                    }
                }
            }

            // 마우스가 움직이고 있으며, selectedObject와 activeLineRenderer가 null이 아닐 때
            if (selectedObject != null && Input.GetMouseButton(0) && activeLineRenderer != null)
            {
                // LineRenderer의 끝점을 마우스 위치로 설정하여 드래그하는 동안 선을 따라오게 함
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLineRenderer.SetPosition(1, mousePos);
            }

            //마우스를 떼었을 때
            if (selectedObject != null && Input.GetMouseButtonUp(0) && activeLineRenderer != null)
            {
               
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(mousePos, Vector2.zero);
                
                LineRenderer connectedLineRenderer;
               
                int ColorSearch = 0;

                //
                // Level1 -> Level2 마우스 드롭다운 했을 때
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Level2") && LevelCheck == 1)
                {
                    // 현재 활성화된 LineRenderer의 인덱스의 값을 할당
                    lineIndex = GetLineIndex_1(activeLineRenderer);

                    // 목표 오브젝트의 인덱스값을 할당
                    GameObject targetObject = hit.collider.gameObject;

                    // 인덱스를 할당
                    targetIndex = level2Objects.IndexOf(targetObject);

                    Debug.Log("targetIndex ; " + targetIndex);
                    // Level1 -> Level2 연결이 이미 있는 상태일 때
                    if (colorCheck[0, targetIndex] != 0 && colorCheck[0, targetIndex] != lineIndex)
                    {
                        // Level2 -> Level3 연결이 있는 지 확인
                        for (int i = 0; i < 3; i++)
                        {
                            if (colorCheck[0, targetIndex] == colorCheck[1, i])
                            {
                                ColorSearch = i;
                                break;
                            }
                        }
                        // 만약 Level2 -> Level3 연결이 있을 때 
                        if (colorCheck[1, ColorSearch] != 0 && colorCheck[1, ColorSearch] != lineIndex)
                        {
                            // 연결된 선 할당
                            connectedLineRenderer = GetLineRendererByIndex_2(colorCheck[1, ColorSearch]);

                            if (connectedLineRenderer != null)
                            {
                                connectedLineRenderer.positionCount = 0;
                            }

                            // 기존의 연결 0으로 초기화
                            colorCheck[1, ColorSearch] = 0;
                        }




                        for (int i = 0; i < 3; i++)
                        {
                            if (colorCheck[1, i] == lineIndex)
                            {
                                GetLineRendererByIndex_2(colorCheck[1, i]).positionCount = 0;
                                colorCheck[1, i] = 0;
                                break;
                            }
                        }

                        /* activeLineRenderer.SetPosition(1, targetObject.transform.position);
                         colorCheck[1, ColorSearch] = lineIndex;*/

                        connectedLineRenderer = GetLineRendererByIndex_1(colorCheck[0, targetIndex]);

                        if (connectedLineRenderer != null)
                        {
                            connectedLineRenderer.positionCount = 0;
                        }

                        // 기존의 연결 0으로 초기화
                        colorCheck[0, targetIndex] = 0;
                    }


                    // 오브젝트의 같은 색상의 상위 연결이 이미 있는 경우
                    for (int i = 0; i < 3; i++)
                    {
                        if (colorCheck[0, i] == lineIndex)
                        {
                            colorCheck[0, i] = 0;
                        }
                    }


                    activeLineRenderer.SetPosition(1, targetObject.transform.position);
                    colorCheck[0, targetIndex] = lineIndex;

                }
                // Level2 -> Level3 마우스 드롭다운 했을 때
                else if (hit.collider != null && hit.collider.gameObject.CompareTag("Level3") && LevelCheck == 2)
                {
                    // 현재 활성화된 LineRenderer의 인덱스의 값을 할당
                    lineIndex = GetLineIndex_2(activeLineRenderer);

                    // 목표 오브젝트의 인덱스값을 할당
                    GameObject targetObject = hit.collider.gameObject;
                    targetIndex = level3Objects.IndexOf(targetObject);

                    //이미 연결이 있는 상태일 때
                    if (colorCheck[1, targetIndex] != 0 && colorCheck[1, targetIndex] != lineIndex)
                    {

                        connectedLineRenderer = GetLineRendererByIndex_2(colorCheck[1, targetIndex]);

                        if (connectedLineRenderer != null)
                        {
                            connectedLineRenderer.positionCount = 0;
                        }

                        // 기존의 연결 0으로 초기화
                        colorCheck[1, targetIndex] = 0;
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        if (colorCheck[1, i] == lineIndex)
                        {
                            colorCheck[1, i] = 0;
                        }
                    }

                    activeLineRenderer.SetPosition(1, targetObject.transform.position);
                    colorCheck[1, targetIndex] = lineIndex;

                }
                // 오브젝트가 없는 곳에서 마우스를 떼었을 때
                else
                {
                    Debug.Log("Dropped in empty space");


                    //Level1일떄
                    if (LevelCheck == 1)
                    {
                        // 현재 활성화된 LineRenderer의 인덱스의 값을 할당
                        lineIndex = GetLineIndex_1(activeLineRenderer);

                        //정점 초기화
                        activeLineRenderer.positionCount = 0;

                        // 해당된 colorCheck 배열에서 상위 연결이 있는지 찾기
                        for (int i = 0; i < 3; i++)
                        {
                            if (lineIndex == colorCheck[1, i])
                            {
                                ColorSearch = colorCheck[1, i];
                                colorCheck[1, i] = 0;
                                break;
                            }
                        }

                        // 만약 Level2 -> Level3 연결이 있을 때 (상위 연결이 있을 경우)
                        if (ColorSearch != 0)
                        {
                            // 연결된 선 할당
                            connectedLineRenderer = GetLineRendererByIndex_2(lineIndex);

                            Debug.Log(connectedLineRenderer.name);
                            // 선 초기화
                            connectedLineRenderer.positionCount = 0;
                            // 기존의 연결 0으로 초기화

                        }

                        for (int i = 0; i < 3; i++)
                        {
                            if (lineIndex == colorCheck[0, i])
                            {
                                ColorSearch = i;
                                colorCheck[0, i] = 0;
                                break;
                            }
                        }


                    }

                    // level2일때
                    else if (LevelCheck == 2)
                    {
                        lineIndex = GetLineIndex_2(activeLineRenderer);
                        for (int i = 0; i < 3; i++)
                        {
                            if (lineIndex == colorCheck[1, i])
                            {
                                ColorSearch = i;
                                colorCheck[1, i] = 0;
                                break;
                            }
                        }


                        //정점 초기화
                        activeLineRenderer.positionCount = 0;
                    }
                }
                Debug.Log("Level1 -> Level2  1 : " + colorCheck[0, 0] + " / 2 : " + colorCheck[0, 1] + " / 3 : " + colorCheck[0, 2] + "\n"
                    + "Level2 -> Level3  1 : " + colorCheck[1, 0] + " / 2 : " + colorCheck[1, 1] + " / 3 : " + colorCheck[1, 2]);

                selectedObject = null;
                activeLineRenderer = null;
                connectedLineRenderer = null;
                // colorCheck 를 통해 선 색상 변경

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        LineColorName(outLineObjects[i, j], colorCheck[i, j]);
                    }
                }

            }
        }
        
    }

    private int GetLineIndex_1(LineRenderer line)
    {
        if (line == redLine_1) return 1;
        if (line == greenLine_1) return 2;
        if (line == blueLine_1) return 3;
        return 0;
    }

    private int GetLineIndex_2(LineRenderer line)
    {
        if (line == redLine_2) return 1;
        if (line == greenLine_2) return 2;
        if (line == blueLine_2) return 3;
        return 0;
    }

    private LineRenderer GetLineRendererByIndex_1(int index)
    {
        switch (index)
        {
            case 1:
                return redLine_1;
            case 2:
                return greenLine_1;
            case 3:
                return blueLine_1;
            default:
                return null;
        }
    }

    private LineRenderer GetLineRendererByIndex_2(int index)
    {
        switch (index)
        {
            case 1:
                return redLine_2;
            case 2:
                return greenLine_2;
            case 3:
                return blueLine_2;
            default:
                return null;
        }
    }

    private void AssignLineRenderer_1(string objectName)
    {
        switch (objectName)
        {
            case "Red":
                activeLineRenderer = redLine_1;
                break;
            case "Green":
                activeLineRenderer = greenLine_1;
                break;
            case "Blue":
                activeLineRenderer = blueLine_1;
                break;
            default:
                activeLineRenderer = null;
                break;
        }
    }

    private void AssignLineRenderer_2(int objectNum)
    {
        switch (objectNum)
        {
            case 1:
                activeLineRenderer = redLine_2;
                break;
            case 2:
                activeLineRenderer = greenLine_2;
                break;
            case 3:
                activeLineRenderer = blueLine_2;
                break;
            default:
                activeLineRenderer = null;
                break;
        }
    }

    private void LineColorName(GameObject lineObj ,int k )
    {
        OutLineColorScript ColorChangeScript = lineObj.GetComponent<OutLineColorScript>();
        switch (k)
        {
            case 0:
                ColorChangeScript.ChangeColor(Color.white);
                break;
            case 1:
                ColorChangeScript.ChangeColor(Color.red);
                break;
            case 2:
                ColorChangeScript.ChangeColor(Color.green);
                break;
            case 3:
                ColorChangeScript.ChangeColor(Color.blue);
                break;
            default:
                break;
        }
    }



}