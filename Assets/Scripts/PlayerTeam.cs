using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam
{
    public int TeamId { get; }
    public Color Color { get; }
    private Dictionary<int, Attitude> attitudes;

    public PlayerTeam(int teamId, Color color) {
        TeamId = teamId;
        Color = color;
        attitudes = new Dictionary<int, Attitude>();
    }

    public Attitude GetAttitude(int otherTeamId) {
        if(attitudes.ContainsKey(otherTeamId)) {
            return attitudes[otherTeamId];
        }
        return Attitude.NEUTRAL;
    }

    public void SetAttitude(int otherTeamId, Attitude attitude) {
        attitudes[otherTeamId] = attitude;
    }
}
