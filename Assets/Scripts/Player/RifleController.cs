using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RifleController : MonoBehaviour
{
    PlayerAudioPlayer audioPlayer;
    Inventory inventory;
    [SerializeField] Camera playerCamera;
    [SerializeField] Animator rifleAnimator;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] float range = 10f;
    PauseMenu pauseMenuHandler;
    bool reloading = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenuHandler = GetComponent<PauseMenu>();
        inventory = GetComponent<Inventory>();
        audioPlayer = GetComponent<PlayerAudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenuHandler.IsPaused())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!reloading)
                {
                    if (inventory.GetAmmoCount() > 0)
                    {
                        inventory.UseAmmo();
                        audioPlayer.PlayShootSound();
                        RaycastHit hit;
                        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
                        {
                            Debug.Log(hit.transform.gameObject.name);
                            NPCHealth npc;
                            if (hit.transform.TryGetComponent<NPCHealth>(out npc))
                            {
                                npc.TakeDamage(1);
                            }
                        }
                        if (inventory.GetAmmoCount() > 0)
                        {
                            reloading = true;
                            StartCoroutine(ReloadCoroutine());
                        }
                    }
                    else
                    {
                        audioPlayer.PlayShootNoAmmoSound();
                    }
                }
            }
            rifleAnimator.SetBool("shoot", reloading);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * range);
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        audioPlayer.PlayReloadSound();
        yield return new WaitForSeconds(reloadTime - 0.25f);
        reloading = false;
    }
}
