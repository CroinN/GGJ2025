using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("DoubleU-DoubleU/MyVolumeComponent", typeof(UniversalRenderPipeline))]
public class MyVolumeComponent : VolumeComponent, IPostProcessComponent
{
    public FloatParameter test = new FloatParameter(0f);

    bool IPostProcessComponent.IsActive() => true;
    bool IPostProcessComponent.IsTileCompatible() => true;
}