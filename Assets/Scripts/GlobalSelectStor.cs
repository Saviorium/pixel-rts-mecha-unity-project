using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSelectStor : MonoBehaviour
{
    private List<GameObject> SelectedObjects;
    private List<GameObject> SelectedObjects_box;
    private List<GameObject> SelectedObjectsForAttack;

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
                switch (hit.collider.tag)
                {
                    case "Bot": SelectBot(hit.collider.gameObject); break;
                    // default:   break;
                }
            }else{
                ClearSelectedItems();
            }
        // При нажатии ПКМ - если на юнита - атаковать, если на пустое место - идти
        }else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D  hit = GetHit();
            if (hit.collider != null)
            {
                //TODO: attack
            }else{
                foreach (GameObject bot in SelectedObjects)
                {
                    bot.GetComponent<BotMove>().SetMoveTarget( Camera.main.ScreenToWorldPoint(Input.mousePosition));
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
            var bots = GameObject.FindGameObjectsWithTag("Bot");

            foreach(GameObject bot in bots)
            {
                Debug.Log(bot.transform.position);
                Debug.Log("( "+box_x_1 +" : "+box_x_2+" ) " + " ( "+box_y_1 +" : "+box_y_2+" )");
                if ((bot.transform.position.x >= box_x_1 && bot.transform.position.x <= box_x_2) &&
                   (bot.transform.position.y >= box_y_1 && bot.transform.position.y <= box_y_2))
                {
                    // SelectBot(bot);
                    SelectedObjects_box.Add(bot);
                    bot.GetComponent<BotMove>().selectionSprite.enabled = true;
                }
            }
            if (SelectedObjects_box.Count > 0)
            {
                ClearSelectedItems();
                foreach(GameObject bot in SelectedObjects_box)
                    SelectBot(bot);
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
        foreach (GameObject bot in SelectedObjects)
        {
            bot.GetComponent<BotMove>().selectionSprite.enabled = false;
        }
        SelectedObjects = new List<GameObject>();
    }

    void SelectBot(GameObject bot)
    {
        if (SelectedObjects.Find(x => x == bot) == null)
        {
            SelectedObjects.Add(bot);
            bot.GetComponent<BotMove>().selectionSprite.enabled = true;
        }
        else
        {
            SelectedObjects.Remove(bot);
            bot.GetComponent<BotMove>().selectionSprite.enabled = false;
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
