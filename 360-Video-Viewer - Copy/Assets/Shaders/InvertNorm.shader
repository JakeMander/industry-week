/*
*	Initially, Our Default Shader Has All It's Normals Facing Outward From The Sphere Thus Resulting In Our Camera Culling The 
*	Surface Of The Sphere When Positioned Inside The Object. We Need To Make The Normals Face Inward So The Culling Is Stopped.
*	And The Surface Is Rendered. The Following Code Has Been Constructed Using The Excellent Tutorial From Liv Erickson: 
*	https://livierickson.com/blog/adding-360-video-to-unity/.
*/

Shader "InvertNorm" 
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
	
	SubShader
	{

		Tags{ "RenderType" = "Opaque" }

		Cull Front

		CGPROGRAM

		#pragma surface surf Lambert vertex:vert
		sampler2D _MainTex;

		struct Input
		{
			float2 uv_MainTex;
			float4 color : COLOR;
		};


		void vert(inout appdata_full v)
		{
			v.normal.xyz = v.normal * -1;
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed3 result = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = result.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}

		Fallback "Diffuse"
}
