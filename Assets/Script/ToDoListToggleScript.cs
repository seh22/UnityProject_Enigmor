using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ToDoListToggleScript : MonoBehaviour
{
    public GameObject toDoList;

    private void Start()
    {
        toDoList.SetActive(false);
    }
    public void TextOn(TMP_Text text)
    {
        text.fontStyle = FontStyles.Bold | FontStyles.Strikethrough;

    }
    public void OnToggleValueChanged(Toggle toggle)
    {
        toggle.interactable = false;   
    }

    public void ToDoListButtonOnClick()
    {
        Debug.Log("Click");
        toDoList.SetActive(true);
    }

    public void TodoListAllClose()
    {
        toDoList.SetActive(false);
    }

}
