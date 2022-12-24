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
float hue2rgb(float c, float t1, float t2)
{
	if (c < 0.0)
	{
		c += 1.0;
	}
	if (c > 1.0)
	{
		c -= 1.0;
	}
	if (6.0 * c < 1.0)return t1 + (t2 - t1) * 6.0 * c;
	if (2.0 * c < 1.0)return t2;
	if (3.0 * c < 2.0)return t1 + (t2 - t1) * (0.66 - c) * 6.0;
	return t1;
}
float3 hslToRgb(float Hue, float Saturation, float Luminosity)
{
	if (Saturation == 0)return float3(Luminosity, Luminosity, Luminosity);
	if (Luminosity < 0.5)return float3(hue2rgb(Hue + 0.33, 2.0 * Luminosity - Luminosity * (1.0 + Saturation), Luminosity * (1.0 + Saturation)), hue2rgb(Hue, 2.0 * Luminosity - Luminosity * (1.0 + Saturation), Luminosity * (1.0 + Saturation)), hue2rgb(Hue - 0.33, 2.0 * Luminosity - Luminosity * (1.0 + Saturation), Luminosity * (1.0 + Saturation)));
	return float3(hue2rgb(Hue + 0.33, 2.0 * Luminosity - (Luminosity + Saturation - Luminosity * Saturation), (Luminosity + Saturation - Luminosity * Saturation)), hue2rgb(Hue, 2.0 * Luminosity - (Luminosity + Saturation - Luminosity * Saturation), (Luminosity + Saturation - Luminosity * Saturation)), hue2rgb(Hue - 0.33, 2.0 * Luminosity - (Luminosity + Saturation - Luminosity * Saturation), (Luminosity + Saturation - Luminosity * Saturation)));
}
float3 rgbToHsl(float3 newColor)
{
	float num4 = max(max(newColor.x, newColor.y), newColor.z);
	float num5 = min(min(newColor.x, newColor.y), newColor.z);
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
		if (num7 > 0.5)
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
	}
	return float3(num6 / 6, y, num7);
}
float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0 {
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;
	float3 HSL = rgbToHsl(color.xyz);
	float3 RGB = hslToRgb(uTime / 12,HSL.y,HSL.z);
    return float4(RGB.x,RGB.y,RGB.z,color.a);
}
technique Technique1 {
    pass Rainbow {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}