using UnityEngine;

public class MiniPlayer : MonoBehaviour
{
    private RectTransform rectTransform;
    public float moveSpeed = 10f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Debug.Log("Current Position: " + rectTransform.anchoredPosition);

        // Zastosowanie moveSpeed oraz Time.deltaTime
        Vector2 currentPos = rectTransform.anchoredPosition;
        currentPos.x += moveX * moveSpeed * Time.deltaTime;
        currentPos.y += moveY * moveSpeed * Time.deltaTime;

        Debug.Log("New Position: " + currentPos);

        rectTransform.anchoredPosition = currentPos;
    }


}