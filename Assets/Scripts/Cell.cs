using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Show(Color color)
    {
        gameObject.SetActive(true);
        spriteRenderer.color = color;
    }

    public void Ghost(Color color)
    {
        color.a = 0.3f;
        Show(color);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
