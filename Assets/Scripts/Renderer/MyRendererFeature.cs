using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable]
public class MyRendererFeature : ScriptableRendererFeature
{
    [SerializeField] private Shader _myShader;
    private Material _myShaderMaterial;

    private MyRenderPass _myRenderPass;

    ////
    public override void Create()
    {
        _myShaderMaterial = CoreUtils.CreateEngineMaterial(_myShader);

        _myRenderPass = new MyRenderPass(_myShaderMaterial);
    }

    protected override void Dispose(bool disposing)
    {
        CoreUtils.Destroy(_myShaderMaterial);
    }
    ////

    ////
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_myRenderPass);
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        // if (renderingData.cameraData.cameraType != CameraType.Game) return;

        _myRenderPass.ConfigureInput(ScriptableRenderPassInput.Color);
        _myRenderPass.ConfigureInput(ScriptableRenderPassInput.Depth);

        _myRenderPass.SetColorTargetHandle(renderer.cameraColorTargetHandle);
        _myRenderPass.SetDepthTargetHandle(renderer.cameraDepthTargetHandle);
    }
    ////
}


