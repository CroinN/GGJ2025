using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MyRendererFeature : ScriptableRendererFeature
{
    private MyRenderPass _myRenderPass;
    
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_myRenderPass);
    }

    public override void Create()
    {
        _myRenderPass = new MyRenderPass();
    }

}


