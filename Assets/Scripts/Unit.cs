using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : PlayerObject
{
    public string nameStr = "Unnamed Unit";
    public List<UnitModule> modules;

    protected Queue<Task> taskList;
    protected Task currentTask;

    private Rigidbody2D rigidbody2d;

    void Start() {
        InitSelectionBorder();
        SetColor();
        relationWatcher = GameObject.Find("RelationWatcher").GetComponent<RelationStorage>();
        taskList = new Queue<Task>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        InitComponents();
    }

    protected abstract void InitComponents();

    protected T AddModule<T>() where T : UnitModule {
        GameObject moduleObj = new GameObject(typeof(T).ToString());
        moduleObj.transform.parent = transform;
        T module = moduleObj.AddComponent<T>();
        modules.Add(module);
        return module;
    }

    void Update()
    {
        if(currentTask == null && taskList.Count > 0) {
            currentTask = taskList.Dequeue();
            StartCoroutine(DoTask(currentTask));
        }
        if(taskList.Count == 0) {
            Task standTask = new Task(Task.TaskType.HoldPosition, gameObject);
            AddTask(standTask);
        }
    }

    private void AddTask(Task task)
    {
        taskList.Enqueue(task);
    }

    IEnumerator DoTask(Task task) {
        Coroutine runningTask = null;
        switch(task.Type) {
            case Task.TaskType.Move:
                runningTask = StartCoroutine(Move(task.Target));
                break;
            case Task.TaskType.HoldPosition:
                runningTask = StartCoroutine(StandStill());
                break;
            case Task.TaskType.Attack:
                runningTask = StartCoroutine(Attack(task.Target));
                break;
            default:
                break;
        }
        yield return runningTask;
        StopCurrentTask();
    }

    private void StopCurrentTask()
    {
        if(currentTask == null) return;
        switch(currentTask.Type) {
            case Task.TaskType.Move:
                Destroy(currentTask.Target);
                StopMoving();
                break;
            case Task.TaskType.Attack:
                StopAttack();
                break;
            default:
                break;
        }
        currentTask = null;
    }

    void ClearTasks() {
        StopAllCoroutines();
        taskList.Clear();
        StopCurrentTask();
    }

    private IEnumerator Move(GameObject target) {
        Vector3 travelVector;
        do {
            travelVector = target.transform.position - transform.position;
            float speed = GetSpeed();
            rigidbody2d.velocity = travelVector.normalized * speed * Time.deltaTime;
            if(travelVector.magnitude > 0.2f) {
                yield return new WaitForSeconds(0.2f); //optimization
            } else {
                yield return null;
            }
        } while (travelVector.magnitude > 0.02f);
    }

    private void StopMoving()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private IEnumerator StandStill() {
        while(true) {
            StopMoving();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private float GetSpeed()
    {
        float speed = 0f;
        float weight = GetWeight();
        foreach (var module in modules) {
            if (module is MovingPart) {
                speed += ((MovingPart)module).GetSpeed(weight);
            }
        }
        return speed;
    }

    private float GetWeight()
    {
        float weight = 0f;
        foreach (var module in modules) {
            weight += module.weight;
        }
        return weight;
    }

    private IEnumerator Attack(GameObject target)
    {
        while (target != null) {
            FireGuns(target);
            StopMoving(); //TODO: attackmove?
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void StopAttack()
    {
        //nothing?
    }

    private void FireGuns(GameObject attackTarget)
    {
        foreach (var module in modules) {
            if (module is Gun) {
                ((Gun)module).Fire(attackTarget.transform.position - transform.position, transform.position);
            }
        }
    }

    public virtual void SetMoveTarget(Vector3 target)
    {
        target.z = 0f;
        ClearTasks();
        GameObject targetObj = (GameObject)Instantiate(Resources.Load("Move Target"));
        targetObj.transform.position = target;
        Task moveTask = new Task(Task.TaskType.Move, targetObj);
        AddTask(moveTask);
    }

    public virtual void SetAttackTarget(GameObject target)
    {
        bool isEnemy = relationWatcher.IsEnemy(gameObject, target);
        if (isEnemy) {
            ClearTasks();
            Task attackTask = new Task(Task.TaskType.Attack, target);
            AddTask(attackTask);
        }
    }


    public override void SetSelection(bool isSelected) 
    {
        selectionSprite.enabled = isSelected;
    }

    public virtual void Die() {
        GameObject.Find("SelectedItems").GetComponent<GlobalSelectStore>().SelectUnit(gameObject);
        Destroy(gameObject);
    }

    protected virtual RaycastHit2D GetHit()
    {
        var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D  hit = Physics2D.Raycast(
            new Vector2(mouse_pos.x, mouse_pos.y),
            Vector2.zero,
            0);
        return hit;
    }

    public override void TakeDamage(float damage) {
        modules[UnityEngine.Random.Range(0, modules.Count)].TakeDamage(damage);
    }
}
