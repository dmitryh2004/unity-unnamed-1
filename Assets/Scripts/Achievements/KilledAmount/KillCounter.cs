using UnityEngine;

public class KillCounter : MonoBehaviour
{
    int killedByPlayer = 0, totalKilled = 0;
    [SerializeField] int total = 0;

    public void Kill(bool byPlayer)
    {
        if (byPlayer) killedByPlayer++;
        totalKilled++;
    }

    public void SetKilled(int _playerKills, int _totalKilled)
    {
        killedByPlayer = _playerKills;
        totalKilled = _totalKilled;
    }

    public int GetKilledByPlayer() { return killedByPlayer; }
    public int GetTotalKilled() { return totalKilled; }
    public bool AreAllKilled() { return total == totalKilled; }
}
