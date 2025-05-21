using UnityEngine;

public class InvertHorizontal : MonoBehaviour
{
    public Camera targetCamera; // La cámara que será afectada

    private bool isInverted = false;

    public void ToggleInvertHorizontal(bool value)
    {
        isInverted = value;
        UpdateCameraRotation();
    }

    private void UpdateCameraRotation()
    {
        if (targetCamera != null)
        {
            Vector3 scale = targetCamera.transform.localScale;
            scale.x = isInverted ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            targetCamera.transform.localScale = scale;
        }
    }
}