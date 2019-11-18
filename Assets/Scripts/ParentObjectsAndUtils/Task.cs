using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public TaskType Type { get; }
    public GameObject Target { get; }

    public Task(TaskType type, GameObject target) {
        this.Type = type;
        this.Target = target;
    }
    
    public enum TaskType {
        None,
        Move,
        HoldPosition,
        Attack,
        Build,
        Repair
    }
}
