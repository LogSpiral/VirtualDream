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
float3 rgbToHsl(float3 newColor)
{
	float num4 = max(newColor.x, newColor.y);
	num4 = max(num4, newColor.z);
	float num5 = min(newColor.x, newColor.y);
	num5 = min(num5, newColor.z);
	float num6 = 0;
	float num7 = (num4 + num5) / 2;
	float y;
	if (num4 == num5)
	{
		num6 = (y = 0);
	}
	else
	{
		float num8 = num4 - num5;
		if(num7 > 0.5)
		{
			y = (num8 / (2 - num4 - num5));
		}
		else
		{
			y = (num8 / (num4 + num5));
		}
		if (num4 == newColor.x)
		{
			if (newColor.y < newColor.z)
			{
				num6 = (newColor.y - newColor.z) / num8 + 6;
			}
			else
			{
				num6 = (newColor.y - newColor.z) / num8;
			}
		}
		if (num4 == newColor.y)
		{
			num6 = (newColor.z - newColor.x) / num8 + 2;
		}
		if (num4 == newColor.z)
		{
			num6 = (newColor.x - newColor.y) / num8 + 4;
		}
		num6 /= 6;
	}
	return float3(num6, y, num7);
}
float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0 {
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;
	float3 HSL = rgbToHsl(color.xyz);
	return float4(color.xyz * (0.5 + HSL.z * 1.5), color.a);
}
technique Technique1 {
	pass Contrast
	{
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}