using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimingZone : MonoBehaviour
{
    public RectTransform indicator;
    public RectTransform perfectZone;
    public TMP_Text resultText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float distance = Mathf.Abs(indicator.anchoredPosition.x - perfectZone.anchoredPosition.x);
            float zoneWidth = perfectZone.rect.width / 2;

            if (distance < zoneWidth * 0.3f)
                resultText.text = "🌟 PERFECT!";
            else if (distance < zoneWidth)
                resultText.text = "👍 Good";
            else
                resultText.text = "❌ Bad";
        }
    }
}
