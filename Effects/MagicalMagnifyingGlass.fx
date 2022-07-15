sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float uOpacity;
float3 uSecondaryColor;
float uTime;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uImageOffset;
float uIntensity;
float uProgress;
float2 uDirection;
float2 uZoom;
float2 uImageSize0;
float2 uImageSize1;
float2 mousePos;
float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0 {
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;
    // pos ����������
    //float2 pos = float2(mousePosX, mousePosY);
    // offset �����ĵ���ǰ�������
    float2 offset = (coords - mousePos);
    // ��Ϊ����Ȳ�ͬ��������
    float2 rpos = offset * float2(uScreenResolution.x / uScreenResolution.y, 1);
    float dis = length(rpos);
    // ������������0.8��
    return tex2D(uImage0, mousePos + offset * sin(5 * dis));
}
technique Technique1 {
    pass Magical {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}