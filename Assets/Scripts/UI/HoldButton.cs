using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public float holdTime = 3f; // Время удержания в секундах
    private float timer = 0f;
    private bool isHolding = false;

    private float maxWidth = 0f;
    private float height = 0f;
    [SerializeField] private Image progressBar;
    [SerializeField] AchievementButton achievementButton;

    void Start()
    {
        maxWidth = progressBar.rectTransform.rect.width;
        height = progressBar.rectTransform.rect.height;
    }

    private void Update()
    {
        float progressValue = Mathf.Clamp01(timer / holdTime);
        float width = progressValue * maxWidth;

        progressBar.rectTransform.sizeDelta = new Vector2(width, height);

        if (isHolding)
        {
            timer += Time.deltaTime;

            if (timer >= holdTime)
            {
                isHolding = false;
                timer = 0f;
                OnHoldComplete();
            }
        }
    }

    // Событие при начале нажатия ЛКМ на кнопку
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isHolding = true;
            timer = 0f;
        }
    }

    // Событие при отпускании ЛКМ
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isHolding = false;
            timer = 0f;
        }
    }

    // Событие при уходе курсора с кнопки (отмена удержания)
    public void OnPointerExit(PointerEventData eventData)
    {
        isHolding = false;
        timer = 0f;
    }

    // Метод, вызываемый после успешного удержания 3 секунды
    private void OnHoldComplete()
    {
        Debug.Log("Кнопка нажата удержанием 3 секунды!");
        achievementButton.onClick();
    }
}
