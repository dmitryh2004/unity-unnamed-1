using System.Collections.Generic;
using UnityEngine;

public class Level2Room2Trigger : MonoBehaviour
{
    bool triggered = false;
    [SerializeField] GameObject player;
    [SerializeField] List<ChaseController> npcs;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        triggered = true;
        if (other.gameObject == player)
        {
            foreach (ChaseController npc in npcs)
            {
                npc.SetTarget(player.transform);
            }
        }
    }
}
