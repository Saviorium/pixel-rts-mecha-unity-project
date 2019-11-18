using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ToolBar : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GlobalSelectStore GlobalSelectStore;
    private List<GameObject> Buttons = new List<GameObject>();

    void Start()
    {
        GlobalSelectStore = GameObject.Find("SelectedItems").GetComponent<GlobalSelectStore>();
    }

    public void DrawTasks()
    {
        ClearButtons();
        int count = 0;
        List<List<Task.TaskType>> TasksToDraw = new List<List<Task.TaskType>>();

        foreach(GameObject obj in GlobalSelectStore.SelectedObjects)
                TasksToDraw.Add(obj.GetComponent<PlayerObject>().AvailableTasks);

        var intersection = TasksToDraw.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());

        foreach(Task.TaskType task in intersection)
            DrawButton(task, count++);
    }

    void DrawButton(Task.TaskType type, int cnt)
    {
        GameObject button = Instantiate(buttonPrefab, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity, transform);
        button.GetComponentInChildren<Text>().text = ((Task.TaskType)type).ToString();
        button.GetComponent<RectTransform>().anchoredPosition = new Vector3(30 + 100 * cnt, 0, 0); 
        button.GetComponentInChildren<ToolButton>().Task = type;
        Buttons.Add(button);
    }

    void ClearButtons()
    {
        foreach(GameObject button in Buttons)
            Destroy(button);
        Buttons.Clear();
    }

}
