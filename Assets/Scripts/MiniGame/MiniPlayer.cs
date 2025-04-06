using UnityEngine;

public class MiniPlayer : MonoBehaviour
{
    private RectTransform rectTransform;
    public float moveSpeed = 50f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float moveX = -1.0f;
        Vector2 currentPos = rectTransform.anchoredPosition;
        currentPos.x += moveX * moveSpeed * Time.deltaTime;
        rectTransform.anchoredPosition = currentPos;
    }


}