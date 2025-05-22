using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorMover : MonoBehaviour
{
    public float speed = 300f;
    private int direction = 1;
    private RectTransform rect;
    public RectTransform moveArea;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.anchoredPosition += new Vector2(speed * direction * Time.deltaTime, 0);

        if (Mathf.Abs(rect.anchoredPosition.x) >= moveArea.rect.width / 2)
        {
            direction *= -1;
        }
    }
}
