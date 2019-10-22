using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSelectStore : MonoBehaviour
{
    private List<GameObject> SelectedObjects;
    private List<GameObject> SelectedObjects_box;

    private Vector3 startPoint = Vector3.zero;
    private Vector3 endPoint = Vector3.zero;
    private LineRenderer lineRenderer;
    // Инициализация всех переменных
    void Start()
    {
        SelectedObjects = new List<GameObject>();

        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }


    void Update()
    {
        // Не забыть переделать с ифной логики
        // При нажаии ЛКМ - если нажал на юнита - селект\деселект, если нажал не на юнита - пока деселект всех юнитов
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D  hit = GetHit();
            startPoint = getMousePos();
            if (hit.collider != null) 
            {   
                ClearSelectedItems();
                SelectUnit(hit.collider.gameObject);
            }else{
                ClearSelectedItems();
            }
        // При нажатии ПКМ - если на юнита - атаковать, если на пустое место - идти
        }else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D  hit = GetHit();
            if (hit.collider != null)
            {
                foreach (GameObject unit in SelectedObjects)
                {
                    if (unit != null)
                        unit.GetComponent<Unit>().SetAttackTarget(hit.collider.gameObject);
                    else
                        SelectedObjects.Remove(unit);
                }
            }else{
                foreach (GameObject unit in SelectedObjects)
                {
                    if (unit != null)
                        unit.GetComponent<Unit>().SetMoveTarget( Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    else
                        SelectedObjects.Remove(unit);
                }
            }
        }

        // В случае задержки ЛКМ - выделение объектов
        if(Input.GetMouseButton(0)) {
            endPoint = getMousePos();
            lineRenderer.enabled = true;
            updateBox();
        }
        if(Input.GetMouseButtonUp(0)) {
            lineRenderer.enabled = false;
            var box_x_1 = endPoint.x >= startPoint.x? startPoint.x : endPoint.x ;
            var box_x_2 = endPoint.x >= startPoint.x? endPoint.x : startPoint.x;
            var box_y_1 = endPoint.y >= startPoint.y? startPoint.y : endPoint.y;
            var box_y_2 = endPoint.y >= startPoint.y? endPoint.y : startPoint.y ;
            SelectedObjects_box = new List<GameObject>();
            var units = GameObject.FindGameObjectsWithTag("Unit");

            foreach(GameObject unit in units)
            {
                if ((unit.transform.position.x >= box_x_1 && unit.transform.position.x <= box_x_2) &&
                   (unit.transform.position.y >= box_y_1 && unit.transform.position.y <= box_y_2))
                {
                    SelectedObjects_box.Add(unit);
                    //unit.GetComponent<Unit>().selectionSprite.enabled = true;
                }
            }
            if (SelectedObjects_box.Count > 0)
            {
                ClearSelectedItems();
                foreach(GameObject unit in SelectedObjects_box)
                    SelectUnit(unit);
            }

        }
    }

    RaycastHit2D GetHit()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D  hit = Physics2D.Raycast(new Vector2( 
                                                          Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                                                          Camera.main.ScreenToWorldPoint(Input.mousePosition).y
                                                         ),
                                              Vector2.zero, 0);
        return hit;
    }

    void ClearSelectedItems()
    {
        foreach (GameObject unit in SelectedObjects)
        {
            unit.GetComponent<PlayerObject>().SetSelection(false);
        }
        SelectedObjects = new List<GameObject>();
    }

    public void SelectUnit(GameObject unit)
    {
        if (SelectedObjects.Find(x => x == unit) == null)
        {
            SelectedObjects.Add(unit);
            unit.GetComponent<Unit>().SetSelection(true);
        }
        else
        {
            SelectedObjects.Remove(unit);
            unit.GetComponent<Unit>().SetSelection(false);
        }
    }

    // Методы для отрисовки выделяющего квадратика
    private void updateBox()
    {
        Vector3[] positions = new Vector3[5];
        positions[0] = startPoint;
        positions[1] = new Vector3(startPoint.x, endPoint.y, 0f);
        positions[2] = endPoint;
        positions[3] = new Vector3(endPoint.x, startPoint.y, 0f);
        positions[4] = startPoint;
        lineRenderer.SetPositions(positions);
    }

    private Vector3 getMousePos() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.Scale(new Vector3(1f, 1f, 0f));
        return pos;
    }
}
