using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected float interactRange = 3f;
    public Transform playerCamera;
    public GameObject interactText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHint();
    }

    protected abstract bool CheckConditionForUpdateHint();
    void UpdateHint()
    {
        interactText.SetActive(CameraIsLookingOnObject() && CheckConditionForUpdateHint());
    }

    bool CameraIsLookingOnObject()
    {
        RaycastHit hit;
        if (playerCamera == null)
        {
            Debug.LogError("Камера не назначена");
            return false;
        }
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactRange))
        {
            return hit.transform == transform;
        }
        return false;
    }
}
