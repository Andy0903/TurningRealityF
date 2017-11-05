using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowComposite : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField]
    float intensity = 2;

    private Material compositeMaterial;

    private void OnEnable()
    {
        compositeMaterial = new Material(Shader.Find("Hidden/GlowComposite"));
    }

    private void OnRenderImage(RenderTexture aSource, RenderTexture aDestination)
    {
        compositeMaterial.SetFloat("_Intensity", intensity);
        Graphics.Blit(aSource, compositeMaterial, 0);
    }
}
