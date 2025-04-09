using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public List<Transform> checkpoints;
    public Transform player;

    void Start()
    {
        Debug.LogWarning("Application path: " + Application.persistentDataPath);
        GameDataManager gdm = GameObject.FindGameObjectWithTag("GameLoader").GetComponent<GameDataManager>();
        Transform gameLoader = GameObject.FindGameObjectWithTag("GameLoader").transform;
        gameLoader.GetComponent<GameLoader>().ApplySave(gdm.LoadGameData());
    }

    public void ApplySave(GameData gamedata)
    {
        if (gamedata != null)
        {
            int checkpointNumber = gamedata.checkpointNumber;
            player.transform.position = checkpoints[checkpointNumber].position;
            player.GetComponent<PlayerHealth>().SetCurrentHealth(gamedata.playerHealth);
            player.GetComponent<Inventory>().SetAmmoCount(gamedata.playerAmmo);

            if (gamedata.buttons.Count > 0)
            {
                foreach (ButtonState btnState in gamedata.buttons)
                {
                    GameObject button = GameObject.Find(btnState.objectName);
                    BaseButton baseButtonComponent;
                    if (button.TryGetComponent<BaseButton>(out baseButtonComponent))
                    {
                        if (baseButtonComponent.isActivated() != btnState.state)
                        {
                            baseButtonComponent.Press();
                        }
                    }
                    else
                    {
                        Debug.LogError("Object " + button.name + " doesn't have BaseButton derived component");
                    }
                }
            }
            
            GameObject elevator1 = GameObject.Find(gamedata.elevator1.objectName);
            Elevator1Controller elevator1Controller;
            if (elevator1.TryGetComponent<Elevator1Controller>(out elevator1Controller))
            {
                elevator1Controller.SetFloor(gamedata.elevator1.destFloor);
                elevator1Controller.trapIsActive = gamedata.elevator1.trapIsActive;
            }
            else
            {
                Debug.LogError("Object " + elevator1.name + " doesn't have Elevator1Controller component");
            }

            GameObject elevator2 = GameObject.Find(gamedata.elevator2.objectName);
            Elevator2Controller elevator2Controller;
            if (elevator2.TryGetComponent<Elevator2Controller>(out elevator2Controller))
            {
                elevator2Controller.SetFloor(gamedata.elevator2.destFloor);
            }
            else
            {
                Debug.LogError("Object " + elevator2.name + " doesn't have Elevator2Controller component");
            }

            if (gamedata.destructibles.Count > 0)
            {
                foreach (KillableState destrState in gamedata.destructibles)
                {
                    GameObject destructible = GameObject.Find(destrState.objectName);
                    if (destructible)
                        destructible.SetActive(destrState.alive);
                }
            }

            if (gamedata.pickables.Count > 0)
            {
                foreach (PickableState pickableState in gamedata.pickables)
                {
                    GameObject pickable = GameObject.Find(pickableState.objectName);
                    if (pickable)
                    {
                        pickable.SetActive(!pickableState.picked);
                        pickable.GetComponent<Pickable>().interactText.SetActive(!pickableState.picked);
                    }
                }
            }

            if (gamedata.chasers.Count > 0)
            {
                foreach(ChaserState chaserState in gamedata.chasers)
                {
                    GameObject chaser = GameObject.Find(chaserState.objectName);
                    if (chaser)
                    {
                        if (chaserState.alive)
                        {
                            GameObject target = GameObject.Find(chaserState.targetName);
                            if (target != null)
                                chaser.GetComponent<ChaseController>().SetTarget(target.transform);
                            chaser.GetComponent<NPCHealth>().setHP(chaserState.health);
                        }
                        else
                        {
                            chaser.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
