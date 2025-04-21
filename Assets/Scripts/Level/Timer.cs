using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    float elapsedTime = 0.0f;
    bool ticking = true;

    public void SetElapsedTime(float time)
    {
        elapsedTime = time;
    }

    public void StartTimer()
    {
        ticking = true;
    }

    public void StopTimer(bool dropTime)
    {
        if (dropTime) elapsedTime = 0.0f;
        ticking = false;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public string GetText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        float seconds = elapsedTime % 60;

        return (minutes > 0)
        ? $"{minutes}:{seconds.ToString("00.000")}"
        : seconds.ToString("0.000");
    }

    private void Update()
    {
        if (ticking)
        {
            elapsedTime += Time.deltaTime;
        }
        
        timerText.SetText(GetText());
    }
}
