using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public float holdTime = 3f; // ����� ��������� � ��������
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

    // ������� ��� ������ ������� ��� �� ������
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isHolding = true;
            timer = 0f;
        }
    }

    // ������� ��� ���������� ���
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isHolding = false;
            timer = 0f;
        }
    }

    // ������� ��� ����� ������� � ������ (������ ���������)
    public void OnPointerExit(PointerEventData eventData)
    {
        isHolding = false;
        timer = 0f;
    }

    // �����, ���������� ����� ��������� ��������� 3 �������
    private void OnHoldComplete()
    {
        Debug.Log("������ ������ ���������� 3 �������!");
        achievementButton.onClick();
    }
}
