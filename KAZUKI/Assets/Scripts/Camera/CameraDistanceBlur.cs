using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraDistanceBlur : MonoBehaviour
{
    [Header("Distance Settings")]
    public float nearClipPlane = 0.1f; // Distancia mínima de renderizado
    public float farClipPlane = 1000f; // Distancia máxima de renderizado

    [Header("Blur Settings")]
    [Range(0, 10)] public float blurStrength = 1f; // Intensidad del desenfoque
    public Material blurMaterial; // Material con el shader de desenfoque

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        ApplyDistanceSettings();
    }

    void Update()
    {
        // Actualiza los valores dinámicamente si cambian en el Inspector
        ApplyDistanceSettings();
        UpdateBlurEffect();
    }

    void ApplyDistanceSettings()
    {
        cam.nearClipPlane = nearClipPlane;
        cam.farClipPlane = farClipPlane;
    }

    void UpdateBlurEffect()
    {
        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurStrength", blurStrength);
            blurMaterial.SetFloat("_FarDistance", farClipPlane);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un material para el desenfoque.");
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (blurMaterial != null)
        {
            Graphics.Blit(src, dest, blurMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}