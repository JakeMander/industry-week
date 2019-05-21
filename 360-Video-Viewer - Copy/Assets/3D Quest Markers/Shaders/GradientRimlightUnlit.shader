Shader "FrostForged/Gradient Rimlight Unlit" {
    Properties {
        _DiffuseColor1 ("Diffuse Color 1", Color) = (1,0.6413792,0,1)
        _DiffuseColor2 ("Diffuse Color 2", Color) = (1,0.1241379,0,1)
        _RimColor1 ("Rim Color 1", Color) = (0.8308824,0.9790061,1,1)
        _RimColor2 ("Rim Color 2", Color) = (0,0.462069,1,1)
        _RimPower ("Rim Power", Range(0, 1)) = 1
        _RimDistance ("Rim Distance", Range(5, 0)) = 2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform fixed _RimPower;
            uniform fixed _RimDistance;
            uniform float4 _RimColor1;
            uniform float4 _RimColor2;
            uniform float4 _DiffuseColor1;
            uniform float4 _DiffuseColor2;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 emissive = lerp(lerp(_DiffuseColor2.rgb,_DiffuseColor1.rgb,(i.uv0.g*1.25+-0.125)),lerp(_RimColor2.rgb,_RimColor1.rgb,i.uv0.g),(_RimPower*(2.0*pow(1.0-max(0,dot(normalDirection, viewDirection)),_RimDistance))));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Standard"
}
