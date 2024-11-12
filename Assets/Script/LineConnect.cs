using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class LineConnect : MonoBehaviour
{
    //public OutLineColorScript parentOutlineColorScript;

    // Leve1 -> Level2 ���� Renderer
    public LineRenderer redLine_1;
    public LineRenderer blueLine_1;
    public LineRenderer greenLine_1;

    // Leve2 -> Level3 ���� Renderer
    public LineRenderer redLine_2;
    public LineRenderer blueLine_2;
    public LineRenderer greenLine_2;

    // Ŭ�� Level Ȯ���� ���� ����
    public int LevelCheck;

    // � LineRenderer�� ����Ǿ��ִ���(0 == null, 1 == red, 2 == green, 3 == blue), �ƴ� ������ ������ Ȯ���� ���� ����Ʈ
    private int[,] colorCheck = new int[2, 3]; 

    // Ŭ���� GameObject�� �����ϱ� ���� ����
    private GameObject selectedObject;

    // �׾����� LineRenderer�� ��ô�ϱ� ���� ����
    private LineRenderer activeLineRenderer;
    
    // Level2, Level3 GameObject�� �����ϱ� ���� ����Ʈ
    private List<GameObject> level2Objects;
    private List<GameObject> level3Objects;

    
    private List<GameObject> lineLevel2Objects;
    private List<GameObject> lineLevel3Objects;

    private RaycastHit2D hit;
    private GameObject targetObject;


    void Start()
    {
        // Level1 -> Level2 ���� LineRenderer ���� �ʱ�ȭ
        redLine_1.positionCount = 0;
        blueLine_1.positionCount = 0;
        greenLine_1.positionCount = 0;

        // Level2 -> Level3 ���� LineRenderer ���� �ʱ�ȭ
        redLine_2.positionCount = 0;
        blueLine_2.positionCount = 0;
        greenLine_2.positionCount = 0;

        // "Level2" �±׸� ���� ������Ʈ���� ã�� �̸� ������ �����Ͽ� ����Ʈ�� ����
        level2Objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Level2"));
        level2Objects = level2Objects.OrderBy(obj => obj.name).ToList();

        // "Level3" �±׸� ���� ������Ʈ���� ã�� �̸� ������ �����Ͽ� ����Ʈ�� ����
        level3Objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Level3"));
        level3Objects = level3Objects.OrderBy(obj => obj.name).ToList();

        // "Level2" Line �±� ������Ʈ 
        lineLevel2Objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("LineLevel2"));
        lineLevel2Objects = level2Objects.OrderBy(obj => obj.name).ToList();

        // "Level3" Line �±� ������Ʈ
        lineLevel3Objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("LineLevel3"));
        lineLevel3Objects = level3Objects.OrderBy(obj => obj.name).ToList();

    }

    void Update()
    {
        
        int targetIndex = 0;    // ��ǥ Object�� �ε���
        int lineIndex = 0;  // Ȱ��ȭ�� LineRenderer�� �ε���

        // ���콺�� Ŭ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺�� ���� ��ǥ�� ����ϰ�, �ش� ��ġ�� ������Ʈ�� ����
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // ������ GameObject�� Tag�� Level1�� ��
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Level1"))
            {
                LevelCheck = 1; // LevelCheck 1�� ����(���콺 ��Ӵٿ� �� Ȯ���� ���� ����)

                Debug.Log("hit  "+ LevelCheck);

                // ������ ������Ʈ�� selectedObject�� ����
                selectedObject = hit.collider.gameObject;

                // ������Ʈ �̸��� ���� ������ LineRenderer�� Ȱ��ȭ
                AssignLineRenderer_1(selectedObject.name);


                // LineRenderer�� Ȱ��ȭ �Ǿ����� ��
                if (activeLineRenderer != null)
                {
                    //���� 2�� ���� �� ��ġ 
                    activeLineRenderer.positionCount = 2;
                    activeLineRenderer.SetPosition(0, selectedObject.transform.position);
                    activeLineRenderer.SetPosition(1, selectedObject.transform.position);
                }
            }

            //���콺�� Level2���� Ŭ�� ���� ��
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Level2"))
            {
                LevelCheck = 2; // LevelCheck 2�� ����(���콺 ��Ӵٿ� �� Ȯ���� ���� ����)

                Debug.Log("hit  " + LevelCheck);

                // ������ ������Ʈ�� selectedObject�� ����
                selectedObject = hit.collider.gameObject;

                // targetIndex�� ������ ������Ʈ�� ����� LineRenderer���� �� �Ҵ�, ���� ��� 0
                targetIndex = colorCheck[0, level2Objects.IndexOf(selectedObject)];

                //������Ʈ�� ���� ������ �ִ°��
                if (targetIndex != 0)
                {
                    // ������ ���� ���� LineRenderer �Ҵ�
                    AssignLineRenderer_2(targetIndex);

                    // LineRenderer�� Ȱ��ȭ �Ǿ����� ��
                    if (activeLineRenderer != null)
                    {
                        //���� 2�� ���� �� ��ġ 
                        activeLineRenderer.positionCount = 2;
                        activeLineRenderer.SetPosition(0, selectedObject.transform.position);
                        activeLineRenderer.SetPosition(1, selectedObject.transform.position);
                    }
                }
            }
        }

        // ���콺�� �����̰� ������, selectedObject�� activeLineRenderer�� null�� �ƴ� ��
        if (selectedObject != null && Input.GetMouseButton(0) && activeLineRenderer != null)
        {
            // LineRenderer�� ������ ���콺 ��ġ�� �����Ͽ� �巡���ϴ� ���� ���� ������� ��
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLineRenderer.SetPosition(1, mousePos);
        }

        //���콺�� ������ ��
        if (selectedObject != null && Input.GetMouseButtonUp(0) && activeLineRenderer != null)
        {
            // ���콺 ��ġ�� �������� �浹�� ����
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero);
            //
            LineRenderer connectedLineRenderer;
            // �ش� ������ ���� ������ ������ ���� �� ��ġ�� �˱� ���� �ݺ���, �Լ� ����
            int ColorSearch = 0;

            // Level1 -> Level2 ���콺 ��Ӵٿ� ���� ��
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Level2") && LevelCheck == 1)
            {
                // ���� Ȱ��ȭ�� LineRenderer�� �ε����� ���� �Ҵ�
                lineIndex = GetLineIndex_1(activeLineRenderer);

                // ��ǥ ������Ʈ�� �ε������� �Ҵ�
                GameObject targetObject = hit.collider.gameObject;
                targetIndex = level2Objects.IndexOf(targetObject);
                Debug.Log(lineIndex);
                //LineColorName(lineIndex);

                // Level1 -> Level2 ������ �̹� �ִ� ������ ��
                if (colorCheck[0, targetIndex] != 0 && colorCheck[0, targetIndex] != lineIndex)
                {
                    // Level1 -> Level2 ������ �ִ� �� Ȯ��
                    for (int i = 0; i<3; i++)
                    {
                        if (colorCheck[0,targetIndex] == colorCheck[1,i])
                        {
                            ColorSearch = i;
                            break;
                        }
                    }
                    // ���� Level2 -> Level3 ������ ���� �� 
                    if (colorCheck[1, ColorSearch] != 0 && colorCheck[1, ColorSearch] != lineIndex)
                    {
                        // ����� �� �Ҵ�
                        connectedLineRenderer = GetLineRendererByIndex_2(colorCheck[1, ColorSearch]);

                        if (connectedLineRenderer != null)
                        {
                            connectedLineRenderer.positionCount = 0;
                        }

                        // ������ ���� 0���� �ʱ�ȭ
                        colorCheck[1, ColorSearch] = 0;
                        
                        //Line �ڵ� �ֱ� ������� �ʱ�ȭ
                        

                    }

                    for (int i = 0; i < colorCheck.GetLength(1); i++)
                    {
                        if (colorCheck[1, i] == lineIndex)
                        {
                            GetLineRendererByIndex_2(colorCheck[1, i]).positionCount = 0;
                            colorCheck[1, i] = 0;
                            break;
                        }
                    }
                    
                    // 
                    activeLineRenderer.SetPosition(1, targetObject.transform.position);
                    colorCheck[1, ColorSearch] = lineIndex;


                    connectedLineRenderer = GetLineRendererByIndex_1(colorCheck[0, targetIndex]);

                    if (connectedLineRenderer != null)
                    {
                        connectedLineRenderer.positionCount = 0;
                    }

                    // ������ ���� 0���� �ʱ�ȭ
                    colorCheck[0, targetIndex] = 0;

                    // line �ڵ� �ֱ� ������� �ʱ�ȭ
                    LineColorName(lineIndex);
                }

                for (int i = 0; i < colorCheck.GetLength(1); i++)
                {
                    if (colorCheck[0, i] == lineIndex)
                    {
                        colorCheck[0, i] = 0;
                        break;
                    }
                }
                activeLineRenderer.SetPosition(1, targetObject.transform.position);
                colorCheck[0, targetIndex] = lineIndex;

                // line �ڵ� �ֱ� �ش� �������� ����
                LineColorName(lineIndex);

                Debug.Log( "Level1 -> Level2  1 : " + colorCheck[0, 0] + " / 2 : " + colorCheck[0, 1] + " / 3 : " + colorCheck[0, 2]+"\n" + 
                    "Level2 -> Level3  1 : " + colorCheck[1, 0] + " / 2 : " + colorCheck[1, 1] + " / 3 : " + colorCheck[1, 2]);

            }

            // Level2 -> Level3 ���콺 ��Ӵٿ� ���� ��
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Level3") && LevelCheck == 2)
            {
                // ���� Ȱ��ȭ�� LineRenderer�� �ε����� ���� �Ҵ�
                lineIndex = GetLineIndex_2(activeLineRenderer);

                // ��ǥ ������Ʈ�� �ε������� �Ҵ�
                GameObject targetObject = hit.collider.gameObject;
                targetIndex = level3Objects.IndexOf(targetObject);
                Debug.Log("line" + lineIndex);

                //�̹� ������ �ִ� ������ ��
                if (colorCheck[1, targetIndex] != 0 && colorCheck[1, targetIndex] != lineIndex)
                {

                    connectedLineRenderer = GetLineRendererByIndex_2(colorCheck[1, targetIndex]);

                    if (connectedLineRenderer != null)
                    {
                        connectedLineRenderer.positionCount = 0;
                    }

                    // ������ ���� 0���� �ʱ�ȭ
                    colorCheck[1, targetIndex] = 0;

                    // line �ڵ� �ֱ� ������� �ʱ�ȭ
                }

                for (int i = 0; i < colorCheck.GetLength(1); i++)
                {
                    if (colorCheck[1, i] == lineIndex)
                    {
                        colorCheck[1, i] = 0;
                        break;
                    }
                }

                activeLineRenderer.SetPosition(1, targetObject.transform.position);
                colorCheck[1, targetIndex] = lineIndex;

                // line �ڵ� �ֱ� �ش� �������� ����

                Debug.Log("Level1 -> Level2  1 : " + colorCheck[0, 0] + " / 2 : " + colorCheck[0, 1] + " / 3 : " + colorCheck[0, 2] + "\n"
                    + "Level2 -> Level3  1 : " + colorCheck[1, 0] + " / 2 : " + colorCheck[1, 1] + " / 3 : " + colorCheck[1, 2]);


            }

            // ������Ʈ�� ���� ������ ���콺�� ������ ��
            else
            {
                Debug.Log("Dropped in empty space");
                // ���� Ȱ��ȭ�� LineRenderer�� �ε����� ���� �Ҵ�
               
                //Level1�ϋ�
                if (LevelCheck == 1)
                {
                    lineIndex = GetLineIndex_1(activeLineRenderer);
                    Debug.Log("Color" + lineIndex );
                    activeLineRenderer.positionCount = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        if (colorCheck[0, lineIndex-1] == colorCheck[1, i])
                        {
                            ColorSearch = i;
                            Debug.Log("level2" + ColorSearch);
                            break;
                        }
                    }

                    
                    // ���� Level2 -> Level3 ������ ���� �� 
                    if (colorCheck[1, ColorSearch] != 0 && colorCheck[1, ColorSearch] != lineIndex-1)
                    {
                        // ����� �� �Ҵ�
                        connectedLineRenderer = GetLineRendererByIndex_2(colorCheck[1, ColorSearch]);
                        // �� �ʱ�ȭ
                        connectedLineRenderer.positionCount = 0;
                        // ������ ���� 0���� �ʱ�ȭ
                        colorCheck[1, ColorSearch] = 0;

                        // line �ڵ� �ֱ� ���


                    }

                    Debug.Log("Level1 -> Level2  1 : " + colorCheck[0, 0] + " / 2 : " + colorCheck[0, 1] + " / 3 : " + colorCheck[0, 2] + "\n"
                    + "Level2 -> Level3  1 : " + colorCheck[1, 0] + " / 2 : " + colorCheck[1, 1] + " / 3 : " + colorCheck[1, 2]);
                }
                // level2�϶�
                else if (LevelCheck == 2)
                {
                    lineIndex = GetLineIndex_2(activeLineRenderer);

                    Debug.Log("Color" + lineIndex);
                    colorCheck[1, lineIndex - 1] = 0;
                    
                    // line �ڵ� �ֱ� ���

                    Debug.Log("Level1 -> Level2  1 : " + colorCheck[0, 0] + " / 2 : " + colorCheck[0, 1] + " / 3 : " + colorCheck[0, 2] + "\n"
                    + "Level2 -> Level3  1 : " + colorCheck[1, 0] + " / 2 : " + colorCheck[1, 1] + " / 3 : " + colorCheck[1, 2]);
                    //���� �ʱ�ȭ
                    activeLineRenderer.positionCount = 0;
                }


            }

            selectedObject = null;
            activeLineRenderer = null;
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

    private void LineColorName(int k )
    {

       
        GameObject obj = null;

        if (LevelCheck == 2) { 
            obj = lineLevel2Objects[level2Objects.IndexOf(selectedObject)];
        }
        if(LevelCheck == 3) {
            obj = lineLevel3Objects[level3Objects.IndexOf(selectedObject)];
        }

        if (targetObject.transform != null)
        {
            OutLineColorScript parentOutlineColorScript = obj.GetComponent<OutLineColorScript>();
            switch (k)
            {
                case 0:
                    parentOutlineColorScript.ImageEnable();
                    break;
                case 1:
                    parentOutlineColorScript.ChangeColor(Color.red);
                    break;
                case 2:
                    parentOutlineColorScript.ChangeColor(Color.green);
                    break;
                case 3:
                    parentOutlineColorScript.ChangeColor(Color.blue);
                    break;
                default:
                    break;
            }

        }
    }
}