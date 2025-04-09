using System.Collections.Generic;
using UnityEngine;

public abstract class BaseButton : Interactable
{
    public List<Transform> linkedButtons;
    protected bool repressable = false;
    private bool activated = false;
    public Animator buttonAnim;

    void Start()
    {
        interactText.SetActive(false);
        interactRange = 3.0f;
    }

    public bool isActivated()
    {
        return activated;
    }

    public void setActivated(bool newActivated)
    {
        if (newActivated != activated)
        {
            buttonAnim.SetBool("pressed", newActivated);
        }
        activated = newActivated;
    }

    public void Press()
    {
        bool old_activated = activated;
        activated = repressable ? !activated : true;
        if (old_activated != activated) //если состояние кнопки изменилось
        {
            buttonAnim.SetBool("pressed", activated);
            if (activated)
            {
                OnPressDownButton();
            }
            else
            {
                OnPressUpButton();
            }
        }

        //обновить состояние связанных кнопок
        foreach (Transform linkedButton in linkedButtons)
        {
            linkedButton.GetComponent<BaseButton>().setActivated(activated);
        }
    }

    protected override bool CheckConditionForUpdateHint()
    {
        return !(activated && !repressable);
    }

    public abstract void OnPressDownButton();
    public abstract void OnPressUpButton();
}
