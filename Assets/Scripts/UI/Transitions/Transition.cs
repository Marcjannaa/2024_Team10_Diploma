using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public static Transition Instance { get; private set; }

    public GameObject squarePrefab;
    public Vector2 squareSize = new Vector2(120, 120);
    public float delay = 0.001f;
    public static bool play = false;

    private RectTransform canvasRect;
    private int cols, rows;
    private GameObject[,] grid;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
        InitGrid();
        
    }

    private void Update()
    {
        if (Transition.play)
        {
            StartCoroutine(PlayTransition());
            
        }
    }

    void InitGrid()
    {
        cols = 16;
        rows = 9;

        grid = new GameObject[cols, rows];

        Vector2 startPos = new Vector2(-canvasRect.rect.width / 2f, -canvasRect.rect.height / 2f);

        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject square = Instantiate(squarePrefab, transform);
                
                RectTransform rt = square.GetComponent<RectTransform>();
                rt.sizeDelta = squareSize;
                rt.anchoredPosition = startPos + new Vector2(x * squareSize.x, y * squareSize.y);
                square.SetActive(false);
                grid[x, y] = square;
            }
        }
    }

    public IEnumerator PlayTransition()
    {
        ResetGrid();
        yield return SpiralCover();
    }
    
    public IEnumerator SpiralCover(System.Action onComplete = null)
    {
        ResetGrid();
        List<Vector2Int> order = GetSpiralOrder(cols, rows);
        foreach (var pos in order)
        {
            grid[pos.x, pos.y].SetActive(true);
            yield return new WaitForSeconds(delay);
        }

        onComplete?.Invoke();
    }

    List<Vector2Int> GetSpiralOrder(int width, int height)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        int left = 0, right = width - 1;
        int top = 0, bottom = height - 1;

        while (left <= right && top <= bottom)
        {
            for (int x = left; x <= right; x++)
                result.Add(new Vector2Int(x, top));
            top++;
            
            for (int y = top; y <= bottom; y++)
                result.Add(new Vector2Int(right, y));
            right--;
            
            if (top <= bottom)
            {
                for (int x = right; x >= left; x--)
                    result.Add(new Vector2Int(x, bottom));
                bottom--;
            }
            
            if (left <= right)
            {
                for (int y = bottom; y >= top; y--)
                    result.Add(new Vector2Int(left, y));
                left++;
            }
        }

        return result;
    }
    
    void ResetGrid()
    {
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                grid[x, y].SetActive(false);
            }
        }
    }
}
