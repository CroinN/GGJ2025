using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;

class MyRenderPass : ScriptableRenderPass
{
    public Shader MyShader;
    private Material _myShaderMaterial;

    private RenderTargetIdentifier _source, _result;

    public MyRenderPass()
    {
        if (!_myShaderMaterial) _myShaderMaterial = CoreUtils.CreateEngineMaterial(MyShader);

        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    ////
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        _source = renderingData.cameraData.renderer.cameraColorTargetHandle;
        _result = new RenderTargetIdentifier();
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer _commandBuffer;
        VolumeStack _volumes;
        MyVolumeComponent _myVolumeComponent;

        _commandBuffer = CommandBufferPool.Get("MyRendererFeature");
        _volumes = VolumeManager.instance.stack;
        _myVolumeComponent = _volumes.GetComponent<MyVolumeComponent>();

        if (_myVolumeComponent.active)
        {
            // _myVolumeComponent.properties -> _myShaderMaterial.properties

            Blit(_commandBuffer, _source, _result, _myShaderMaterial, 0);
        }
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {

    }
    ////

}