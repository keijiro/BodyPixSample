#include "Packages/jp.keijiro.bodypix/Shaders/Common.hlsl"

void BodyPixMask_float(UnityTexture2D tex, float2 uv, float thresh, out float output)
{
    uint w, h;
    tex.tex.GetDimensions(w, h);
    BodyPix_Mask mask = BodyPix_SampleMask(uv, tex.tex, uint2(w, h));
    float seg = BodyPix_EvalSegmentation(mask);
    output = smoothstep(thresh - 0.05, thresh + 0.05, seg);
}

void BodyPixFace_float(UnityTexture2D tex, float2 uv, out float output)
{
    uint w, h;
    tex.tex.GetDimensions(w, h);
    BodyPix_Mask mask = BodyPix_SampleMask(uv, tex.tex, uint2(w, h));
    float lface = BodyPix_EvalPart(mask, BODYPIX_PART_LEFT_FACE);
    float rface = BodyPix_EvalPart(mask, BODYPIX_PART_RIGHT_FACE);
    output = BodyPix_EvalSegmentation(mask) * max(lface, rface);
}
