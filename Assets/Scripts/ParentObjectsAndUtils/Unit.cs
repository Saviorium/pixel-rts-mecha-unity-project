using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : PlayerObject
{
    public string nameStr = "Unnamed Unit";

    protected Queue<Task> taskList;
    protected Task currentTask;

    private Rigidbody2D rigidbody2d;

    void Awake() {
        InitSelectionBorder();
        relationWatcher = GameObject.Find("RelationWatcher").GetComponent<AttitudeStorage>(); //FIXME: только 1 Start может быть. Мы типа разделили на 2 файла,
        SetColor();                                                                           //но они всё равно сильно связаны между собой - ничего не изменилось
        taskList = new Queue<Task>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        AvailableTasks = new List<Task.TaskType>();
        GetAvailableTasks();
        InitComponents();
    }

    protected virtual void InitComponents() {}

    protected T AddModule<T>() where T : UnitModule {
        GameObject moduleObj = new GameObject(typeof(T).ToString());
        moduleObj.transform.parent = transform;
        T module = moduleObj.AddComponent<T>();
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
                if (currentTask.Target.tag == "Untagged")
                    Destroy(currentTask.Target);
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
        foreach(var movingPart in GetUnitModules<MovingPart>()) {
            speed += movingPart.GetSpeed(weight);
        }
        return speed;
    }

    private float GetWeight()
    {
        float weight = 0f;
        foreach (var module in GetUnitModules<UnitModule>()) {
            weight += module.weight;
        }
        return weight;
    }

    private IEnumerator Attack(GameObject target)
    {
        while (target != null) {
            FireGuns(target);
            //StopMoving(); //TODO: attackmove? Атак мув - когда ты указываешь на точку и юнит идет к ней и мочит всех кто попадется, а не возможность атаковать и идти
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void StopAttack()
    {
        //nothing?
    }

    private void FireGuns(GameObject attackTarget)
    {
        foreach (var module in GetUnitModules<Gun>()) {
            module.Fire(attackTarget.transform.position - transform.position, transform.position);
        }
    }

    private List<T> GetUnitModules<T>() where T : UnitModule {
        return new List<T>(GetComponentsInChildren<T>());
    }

    public virtual GameObject SetMarkOnTarget(Vector3 target)
    {
        target.z = 0f;
        ClearTasks();
        GameObject targetObj = (GameObject)Instantiate(Resources.Load("Move Target"));
        targetObj.transform.position = target;
        return  targetObj;
    }

    public virtual void SetMoveTarget(Vector3 target)
    {
        GameObject targetObj = SetMarkOnTarget(target);
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
        if (isSelected) GetAvailableTasks();
    }

    public virtual void Die() {
        ClearTasks();
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
        UnitModule[] modules = GetComponentsInChildren<UnitModule>();
        modules[UnityEngine.Random.Range(0, modules.Length)].TakeDamage(damage);
    }

    public void GetAvailableTasks()
    {
        AvailableTasks.Clear();
        foreach (var module in GetUnitModules<UnitModule>())
        {
            foreach (var taskType in module.AvailableTasks)
                AvailableTasks.Add(taskType);
        }
    }

    
    public override void SendAction(Task.TaskType action)
    {
        if (GetHit().collider != null){
            Task Task = new Task(action, GetHit().collider.gameObject);
            AddTask(Task);
        }else
        {
            Task Task = new Task(action, SetMarkOnTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            AddTask(Task);
        }
    }

    public override void SendAction(GlobalSelectStore.ClickType action)
    {
        switch (action)
        {
            case GlobalSelectStore.ClickType.LMB: 
                if (GetHit().collider != null)
                {
                    if (GetHit().collider.gameObject  == gameObject) 
                        SetSelection(true);
                }
                else
                    SetSelection(false);
                break;
            case GlobalSelectStore.ClickType.RMB: 
                if (GetHit().collider != null) 
                    SetAttackTarget(GetHit().collider.gameObject);
                else    
                    SetMoveTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                break;
            case GlobalSelectStore.ClickType.MMB: 
                break;
        }
    }
}
