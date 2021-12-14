// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Toon_Normal" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		_AddColor("Add Color", Color) = (0,0,0,0)
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		pass{

		
		Cull Off
       
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		uniform float4 _Color;
		sampler2D _MainTex;
		float4 _MainTex_ST;
		samplerCUBE _ToonShade;
		float4 _AddColor;


		struct vertOut{
			float4 pos:SV_POSITION;
			float2 texcoord : TEXCOORD0;
			float3 cubenormal : TEXCOORD1;
			float4 color:COLOR;
		};
		vertOut vert(appdata_base v)
		{
			float3 worldN=mul(unity_ObjectToWorld,float4(v.normal,0)).xyz;
			vertOut o;
			o.pos=UnityObjectToClipPos(v.vertex);
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.color.rgb=ShadeSH9(half4(worldN,1.0));
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0));
			o.color.a=1.0;
			return o;
		}
		float4 frag(vertOut i):COLOR
		{
			float4 texc=tex2D(_MainTex,i.texcoord);
			i.color=texc*_Color;
			float4 cube = texCUBE(_ToonShade, i.cubenormal);
			i.color=i.color*2.0*cube;
			i.color += _AddColor;
			return i.color;
		}
		ENDCG
		}//end pass
	} 
	Fallback "VertexLit"
}