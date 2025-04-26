using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class AchievementController : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _title;
    private Animator animator;
    private bool currentlyUsed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool IsCurrentlyUsed()
    {
        return currentlyUsed;
    }
    public void ShowAchievement(Achievement ach)
    {
        Debug.Log(ach);
        if (ach == null) return;
        currentlyUsed = true;
        _icon.sprite = ach.icon;
        _title.text = ach.title;
        animator.SetTrigger("Show");
        StartCoroutine(HideAfterDelay(3f)); // Показывать 3 секунды
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Hide");
        currentlyUsed = false;
    }
}