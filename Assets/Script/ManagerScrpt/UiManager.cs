using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UI level üũ�� ���� �ڵ�
public class UiManager : MonoBehaviour
{
    public int UiLevelCheck { get; private set; } = 0;

    private void Start()
    {
        UiLevelCheck = 0;

    }

    // UI ���� ����
    public void IncreaseUiLevel()
    {
        UiLevelCheck++;
    }

    // UI ���� ����
    public void DecreaseUiLevel()
    {
        if (UiLevelCheck > 0)
        {
            UiLevelCheck--;
        }
    }

    // ���� UI Ŭ�� ���� ���� Ȯ��
    public bool IsBlockingClick()
    {
        return UiLevelCheck > 0;
    }
}
