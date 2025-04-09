using System.Collections;
using UnityEngine;

public class CheckpointButton : BaseButton
{
    [SerializeField] int checkpointNumber;
    [SerializeField] Transform player;
    GameDataManager gdm;

    void Start()
    {
        repressable = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gdm = GameObject.FindGameObjectWithTag("GameLoader").GetComponent<GameDataManager>();
    }

    public override void OnPressDownButton()
    {
        PlayerHealth playerHealthComp = player.GetComponent<PlayerHealth>();
        Inventory playerInventory = player.GetComponent<Inventory>();

        Debug.Log("Trying to save data...");
        gdm.SaveGameData(checkpointNumber, playerHealthComp.GetCurrentHealth(), playerInventory.GetAmmoCount());

        StartCoroutine(AutoPressButton());
    }

    private IEnumerator AutoPressButton()
    {
        yield return new WaitForSeconds(0.25f);

        Press();
    }

    public override void OnPressUpButton()
    {
        
    }
}
