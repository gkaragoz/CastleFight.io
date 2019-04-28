﻿// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "PDT Shaders/TestGrid" {
	Properties {
		_LineColor ("Line Color", Color) = (1,1,1,1)
		_CellColor ("Cell Color", Color) = (0,0,0,0)
		[PerRendererData] _MainTex ("Albedo (RGB)", 2D) = "white" {}
		[IntRange] _GridSizeX("Grid Size X", Range(1,100)) = 10
		[IntRange] _GridSizeY("Grid Size Y", Range(1,100)) = 10
		_LineSize("Line Size", Range(0,1)) = 0.15
	}
	SubShader {
		Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" }
		LOD 200
	

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness = 0.0;
		half _Metallic = 0.0;
		float4 _LineColor;
		float4 _CellColor;

		float _GridSizeX;
		float _GridSizeY;
		float _LineSize;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color

			float2 uv = IN.uv_MainTex;

			fixed4 c = float4(0.0,0.0,0.0,0.0);

			float brightness = 1.;

			float gsizeX = floor(_GridSizeX);
			float gsizeY = floor(_GridSizeY);

			gsizeX += _LineSize;
			gsizeY += _LineSize;

			float2 id;

			id.x = floor(uv.x/(1.0/gsizeX));
			id.y = floor(uv.y/(1.0/gsizeY));

			float4 color = _CellColor;
			brightness = _CellColor.w;

			if (frac(uv.x*gsizeX) <= _LineSize || frac(uv.y*gsizeY) <= _LineSize)
			{
				brightness = _LineColor.w;
				color = _LineColor;
			}
			

			//Clip transparent spots using alpha cutout
			if (brightness == 0.0) {
				clip(c.a - 1.0);
			}
			

			o.Albedo = float4( color.x*brightness,color.y*brightness,color.z*brightness,brightness);
			// Metallic and smoothness come from slider variables
			o.Metallic = 0.0;
			o.Smoothness = 0.0;
			o.Alpha = 0.0;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
