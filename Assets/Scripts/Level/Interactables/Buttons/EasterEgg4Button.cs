using UnityEngine;
using System.Collections.Generic;

public class EasterEgg4Button : BaseButton
{
    [SerializeField] AchievementSystem _as;
    [SerializeField] List<Light> lights;
    private void Start()
    {
        repressable = false;
    }
    public override void OnPressDownButton()
    {
        _as.AddProgress(AchievementNames.EasterEgg4, 1);
        foreach (Light l in lights)
        {
            l.gameObject.SetActive(true);
        }
    }

    public override void OnPressUpButton()
    {
        
    }
}
