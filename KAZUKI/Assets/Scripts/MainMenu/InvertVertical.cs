using UnityEngine;

public class InvertVertical : MonoBehaviour
{
    public Camera targetCamera; // La cámara que será afectada

    private bool isInverted = false;

    public void ToggleInvertVertical(bool value)
    {
        isInverted = value;
        UpdateCameraRotation();
    }

    private void UpdateCameraRotation()
    {
        if (targetCamera != null)
        {
            Vector3 scale = targetCamera.transform.localScale;
            scale.y = isInverted ? -Mathf.Abs(scale.y) : Mathf.Abs(scale.y);
            targetCamera.transform.localScale = scale;
        }
    }
}