using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolButton : MonoBehaviour
{
    public GameObject MenuWindow;
    public Task.TaskType Task;
    public GlobalSelectStore GlobalSelectStore;
    
    void Start()
    {
		gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        GlobalSelectStore = GameObject.Find("SelectedItems").GetComponent<GlobalSelectStore>();
    }

    void TaskOnClick()
    {
        GlobalSelectStore.Task = Task;
    }

}
