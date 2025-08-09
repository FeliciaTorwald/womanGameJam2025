using UnityEngine;

[ExecuteInEditMode]
public class CRTEffect : MonoBehaviour
{
    public Material crtMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (crtMaterial != null)
            Graphics.Blit(src, dest, crtMaterial);
        else
            Graphics.Blit(src, dest);
    }
}
