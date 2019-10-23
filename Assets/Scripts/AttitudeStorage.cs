using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttitudeStorage : MonoBehaviour
{
    private Dictionary<int, PlayerTeam> teams;

    void Start()
    {
        teams = new Dictionary<int, PlayerTeam>();
        teams.Add(0, new PlayerTeam(0, Color.red));
        teams.Add(1, new PlayerTeam(1, Color.blue));
        SetAttitude(0, 1, Attitude.ENEMY);
    }

    public bool IsEnemy(GameObject Attaker, GameObject Target)
    {
        int attackerTeam = Attaker.GetComponent<PlayerObject>().team;
        int defenderTeam = Target.GetComponent<PlayerObject>().team;
        return teams[attackerTeam].GetAttitude(defenderTeam) == Attitude.ENEMY;
    }

    public Color GetTeamColor(int team)
    {
        return teams[team].Color;
    }

    public void SetAttitude(int team1, int team2, Attitude attitude) {
        teams[team1].SetAttitude(team2, attitude);
        teams[team2].SetAttitude(team1, attitude);
    }
}

public enum Attitude {
    ENEMY,
    NEUTRAL,
    FREIND
}