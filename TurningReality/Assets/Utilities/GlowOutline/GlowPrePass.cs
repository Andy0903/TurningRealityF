using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class GlowPrePass : MonoBehaviour
{
    private static RenderTexture preePass;
    private static RenderTexture blurred;

    private Material blurMaterial;

    [SerializeField]
    Shader glowReplaceShader;
    [SerializeField]
    Shader blurShader;

    private void OnEnable()
    {
        preePass = new RenderTexture(Screen.width, Screen.height, 24);
        preePass.antiAliasing = QualitySettings.antiAliasing;
        blurred = new RenderTexture(Screen.width / 2, Screen.height / 2, 0);

        Camera camera = GetComponent<Camera>();
        //Shader glowShader = Shader.Find("Hidden/GlowReplace");
        camera.targetTexture = preePass;
        camera.SetReplacementShader(glowReplaceShader, "Glowable");
        Shader.SetGlobalTexture("_GlowPrePassTex", preePass);

        Shader.SetGlobalTexture("_GlowBlurredTex", blurred);

        blurMaterial = new Material(blurShader);
        blurMaterial.SetVector("_BlurSize", new Vector2(blurred.texelSize.x * 1.5f, blurred.texelSize.y * 1.5f));
    }

    private void OnRenderImage(RenderTexture aSource, RenderTexture aDestination)
    {
        Graphics.Blit(aSource, aDestination);

        Graphics.SetRenderTarget(blurred);
        GL.Clear(false, true, Color.clear);

        Graphics.Blit(aSource, blurred);

        for (int i = 0; i < 4; i++)
        {
            RenderTexture temp = RenderTexture.GetTemporary(blurred.width, blurred.height);
            Graphics.Blit(blurred, temp, blurMaterial, 0);
            Graphics.Blit(temp, blurred, blurMaterial, 1);
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}
