using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable] public class PickableState : ObjectState
{
    public bool picked;
}

[System.Serializable] public class ChaserState : NPCState
{
    public string targetName;
}

[System.Serializable] public class NPCState : KillableState
{
    public int health;
}

[System.Serializable] public class KillableState : ObjectState
{
    public bool alive;
}

[System.Serializable]
public class ElevatorState : ObjectState
{
    public int curFloor;
    public int destFloor;
}

[System.Serializable] public class Elevator1State : ElevatorState
{
    public bool trapIsActive;
}

[System.Serializable]
public class ButtonState : ObjectState
{
    public bool state;
}

[System.Serializable] public class ObjectState
{
    public string objectName;
}

// Класс для хранения данных игры
[System.Serializable]
public class GameData
{
    public int checkpointNumber;
    public int playerHealth;
    public int playerAmmo;
    public float elapsedTime;
    public List<ButtonState> buttons = new();
    public Elevator1State elevator1 = new();
    public ElevatorState elevator2 = new();
    public List<ChaserState> chasers = new();
    public List<KillableState> destructibles = new();
    public List<PickableState> pickables = new();

    public GameData(int checkpointNumber, int playerHealth, int playerAmmo)
    {
        this.checkpointNumber = checkpointNumber;
        this.playerHealth = playerHealth;
        this.playerAmmo = playerAmmo;
    }
}

public class GameDataManager : MonoBehaviour
{
    public Timer timer;
    public List<GameObject> objectsToSave = new List<GameObject>();
    private const string SAVE_FILE_NAME = "/game_data.json";

    // Сохранение данных
    public void SaveGameData(int floorNumber, int playerHealth, int playerAmmo)
    {
        GameData data = new GameData(floorNumber, playerHealth, playerAmmo);
        data.elapsedTime = timer.GetElapsedTime();
        foreach(GameObject obj in objectsToSave)
        {
            BaseButton baseButtonComponent;
            Elevator1Controller elevator1Controller;
            Elevator2Controller elevatorController;
            ChaseController chaseController;
            NPCHealth npc;
            Pickable pickable;
            //если объект - кнопка
            if (obj.TryGetComponent<BaseButton>(out baseButtonComponent))
            {
                ButtonState buttonState = new ButtonState();
                buttonState.objectName = obj.name;
                buttonState.state = baseButtonComponent.isActivated();
                data.buttons.Add(buttonState);
            }
            //если объект - лифт с ловушкой
            else if (obj.TryGetComponent<Elevator1Controller>(out elevator1Controller))
            {
                Elevator1State elevator1State = new Elevator1State();
                elevator1State.objectName = obj.name;
                elevator1State.curFloor = elevator1Controller.GetCurrentFloor();
                elevator1State.destFloor = elevator1Controller.GetDestFloor();
                elevator1State.trapIsActive = elevator1Controller.trapIsActive;
                data.elevator1 = elevator1State;
            }
            //если объект - лифт 2
            else if (obj.TryGetComponent<Elevator2Controller>(out elevatorController)) {
                ElevatorState elevatorState = new ElevatorState();
                elevatorState.objectName = obj.name;
                elevatorState.curFloor = elevatorController.GetCurrentFloor();
                elevatorState.destFloor = elevatorController.GetDestFloor();
                data.elevator2 = elevatorState;
            }
            //если объект - npc, преследующий игрока
            else if (obj.TryGetComponent<ChaseController>(out chaseController))
            {
                ChaserState chaserState = new ChaserState();
                chaserState.objectName = obj.name;
                chaserState.alive = obj.activeInHierarchy;
                chaserState.health = obj.GetComponent<NPCHealth>().getHP();
                chaserState.targetName = (chaseController.GetTarget() != null) ? chaseController.GetTarget().name : null;
                data.chasers.Add(chaserState);
            }
            //если объект разрушаемый
            else if (obj.TryGetComponent<NPCHealth>(out npc))
            {
                KillableState destructible = new();
                destructible.objectName = obj.name;
                destructible.alive = npc.getHP() > 0;
                data.destructibles.Add(destructible);
            }
            //если объект класса pickable
            else if (obj.TryGetComponent<Pickable>(out pickable))
            {
                PickableState pickableState = new PickableState();
                pickableState.objectName = obj.name;
                pickableState.picked = !obj.activeInHierarchy;
                data.pickables.Add(pickableState);
            }
            else
            {
                Debug.LogError("Unexpected object type: " + obj.name);
            }
        }

        string jsonData = JsonUtility.ToJson(data);

        string filePath = Application.persistentDataPath + SAVE_FILE_NAME;
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Данные сохранены");
    }

    // Загрузка данных
    public GameData LoadGameData()
    {
        string filePath = Application.persistentDataPath + SAVE_FILE_NAME;

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);

            Debug.Log("Данные загружены");
            return data;
        }
        else
        {
            Debug.LogError("Файл сохранения не найден");
            return null;
        }
    }
}
