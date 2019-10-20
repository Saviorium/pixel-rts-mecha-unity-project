using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationStorage : MonoBehaviour
{
    private List<List<int>> RelationMap;
    private List<int> Teams;

    // Start is called before the first frame update
    void Start()
    {
        //Пока захардкожу ибо не вижу возможности пока как-либо выбрать отношения команд во время игры, если только атаковать - перевести во вражду
        // -1 - Дружба, 1 - Вражда, 0 - Нейтралитет
        Teams = new List<int> {1, 2, 3, 4};
        RelationMap = new List<List<int>>();
        RelationMap.Add(new List<int> { 0,  1,  0, -1});
        RelationMap.Add(new List<int> { 1,  0,  1,  0});
        RelationMap.Add(new List<int> { 0,  1,  0,  1});
        RelationMap.Add(new List<int> {-1,  0,  1,  0});
    }

    public bool IsEnemy(GameObject Attaker, GameObject Target)
    {
        int team_1 = Teams.FindIndex(x => x == Attaker.GetComponent<PlayerObject>().team);
        int team_2 = Teams.FindIndex(x => x == Target.GetComponent<PlayerObject>().team);
        return RelationMap[team_1][team_2] == 1? true:false; 
    }
}
