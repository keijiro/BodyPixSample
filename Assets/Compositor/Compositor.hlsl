#include "Packages/jp.keijiro.bodypix/Shaders/Common.hlsl"

void BodyPixMask_float(UnityTexture2D tex, float2 uv, out float output)
{
    uint w, h;
    tex.tex.GetDimensions(w, h);
    BodyPix_Mask mask = BodyPix_SampleMask(uv, tex.tex, uint2(w, h));
    output = BodyPix_EvalSegmentation(mask);
}
