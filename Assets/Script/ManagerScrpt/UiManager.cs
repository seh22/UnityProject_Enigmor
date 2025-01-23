using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UI level 체크를 위한 코드
public class UiManager : MonoBehaviour
{
    public int UiLevelCheck { get; private set; } = 0;

    private void Start()
    {
        UiLevelCheck = 0;

    }

    // UI 레벨 증가
    public void IncreaseUiLevel()
    {
        UiLevelCheck++;
    }

    // UI 레벨 감소
    public void DecreaseUiLevel()
    {
        if (UiLevelCheck > 0)
        {
            UiLevelCheck--;
        }
    }

    // 하위 UI 클릭 방지 여부 확인
    public bool IsBlockingClick()
    {
        return UiLevelCheck > 0;
    }
}
