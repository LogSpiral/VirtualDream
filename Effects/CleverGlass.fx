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
float3 HSVToRGB(float h,float s,float v)
{
	float r = 0;
	float g = 0;
	float b = 0;
	float t = 6 * h % 1;
	int hue = (int) (6 * h);
	/*switch (hue)
	{
		case 0:
			r = 1;
			g = s * t + 1 - s;
			b = 1 - s;
			break;
		case 1:
			r = 1 - s * t;
			g = 1;
			b = 1 - s;
			break;
		case 2:
			r = 1 - s;
			g = 1;
			b = s * t + 1 - s;
			break;
		case 3:
			r = 1 - s;
			g = 1 - s * t;
			b = 1;
			break;
		case 4:
			r = s * t + 1 - s;
			g = 1 - s;
			b = 1;
			break;
		case 5:
			r = 1;
			g = 1 - s;
			b = 1 - s * t;
			break;
	}*/
	if (hue == 0)
	{
		r = 1;
		g = s * t + 1 - s;
		b = 1 - s;
	}
	else if (hue == 1)
	{
		r = 1 - s * t;
		g = 1;
		b = 1 - s;
	}
	else if(hue == 2)
	{
		r = 1 - s;
		g = 1;
		b = s * t + 1 - s;
	}
	else if(hue == 3)
	{
		r = 1 - s;
		g = 1 - s * t;
		b = 1;
	}
	else if(hue == 4)
	{
		r = s * t + 1 - s;
		g = 1 - s;
		b = 1;
	}
	else
	{
		r = 1;
		g = 1 - s;
		b = 1 - s * t;
	}
	return float3(r, g, b) * v;
}
/*float3 RGBToHSV(float x,float y,float z)
{
	bool flag1 = x >= y;
	bool flag2 = x >= z;
	bool flag3 = y >= z;
	float maxV;
	float midV;
	float minV;
	int degree;
	if (flag1)
	{
		if (flag2)
		{
			maxV = x;
			if (flag3)
			{
				degree = 0;
				midV = y;
				minV = z;
			}
			else
			{
				degree = 5;
				midV = z;
				minV = y;
			}
		}
		else
		{
			degree = 4;
			maxV = z;
			midV = x;
			minV = y;
		}
	}
	else
	{
		if (flag2)
		{
			degree = 1;
			maxV = y;
			midV = x;
			minV = z;
		}
		else
		{
			minV = x;
			if (flag3)
			{
				degree = 2;
				maxV = y;
				midV = z;
			}
			else
			{
				degree = 3;
				maxV = z;
				minV = y;
			}
		}
	}
	float s = (maxV - minV) / maxV;
	if (degree % 2 == 0)
		return float3(((midV - maxV) / s / maxV + 1 + degree) / 6, s, maxV);
	return float3(((maxV - midV) / s / maxV + degree) / 6, s, maxV);
}*/
float GetHue(float num4, float num5, float x, float y, float z)
{
	float num6 = 0;
	float num7 = num4 - num5;
	if (num4 == x)
	{
		if (y < z)
		{
			num6 = (y - z) / num7 + 6;
		}
		else
		{
			num6 = (y - z) / num7;
		}
	}
	if (num4 == y)
	{
		num6 = (z - x) / num7 + 2;
	}
	if (num4 == z)
	{
		num6 = (x - y) / num7 + 4;
	}
	return num6 /= 6;
}
/*float3 RGBToHSV(float3 newColor)
{
	float num4 = max(newColor.x, newColor.y);
	num4 = max(num4, newColor.z);
	float num5 = min(newColor.x, newColor.y);
	num5 = min(num5, newColor.z);
	float y = (num4 - num5) / num4;
	return float3(GetHue(num4, num5, newColor.x, newColor.y, newColor.z), y, num4);
}*/
float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
	if (!any(color))
		return color;
	float maxValue = max(max(color.x, color.y), color.z);
	float minValue = min(min(color.x, color.y), color.z);
	return float4(HSVToRGB(uTime / 6 % 1, (maxValue - minValue) / maxValue, maxValue), color.a);
}

technique Technique1 {
    pass Clever {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
/*sampler uImage0 : register(s0);
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
float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0 {
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;
	float maxValue = max(max(color.x, color.y), color.z);
	float minValue = min(min(color.x, color.y), color.z);
	float3 cyanColor = float3(minValue, maxValue, maxValue);
	return float4(cyanColor, color.a);
}
technique Technique1 {
    pass Clever {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}*/