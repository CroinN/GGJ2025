using System;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;

[Serializable]
class MyRenderPass : ScriptableRenderPass
{
    private Material _myShaderMaterial;
    private RTHandle _colorHandle, _depthHandle;

    ////
    public MyRenderPass(Material myShaderMaterial)
    {
        _myShaderMaterial = myShaderMaterial;

        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public void SetColorTargetHandle(RTHandle colorHandle)
    {
        _colorHandle = colorHandle;
    }

    public void SetDepthTargetHandle(RTHandle depthHandle)
    {
        _depthHandle = depthHandle; 
    }
    ////

    ////
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        VolumeStack _volumes;
        MyVolumeComponent _myVolumeComponent;
        CommandBuffer _commandBuffer;

        _volumes = VolumeManager.instance.stack;
        _myVolumeComponent = _volumes.GetComponent<MyVolumeComponent>();

        _commandBuffer = CommandBufferPool.Get();

        using( new ProfilingScope(_commandBuffer, new ProfilingSampler("My Post Process Effect Test")))
        {
            // Setup Passes Shaders to Volume Component Here
        }

        context.ExecuteCommandBuffer(_commandBuffer);
        _commandBuffer.Clear();

        CommandBufferPool.Release(_commandBuffer);
    }
    ////

}