// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Polytope Studio/PT_Woden_Sign_Shader"
{
	Properties
	{
		[NoScaleOffset]_BaseTexture("Base Texture", 2D) = "white" {}
		[Toggle]_CUSTOMCOLORSTINTING("CUSTOM COLORS  TINTING", Float) = 1
		[HDR]_GroundColor("Ground Color", Color) = (0.08490568,0.05234205,0.04846032,1)
		[HDR]_TopColor("Top Color", Color) = (0.4811321,0.4036026,0.2382966,1)
		_Gradient("Gradient ", Range( 0 , 0.2)) = 1
		_GradientPower("Gradient Power", Range( 0 , 10)) = 1
		[NoScaleOffset]_DecalsTexture("Decals Texture", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0.7748996
		_DecalsColor("Decals Color", Color) = (0,0,0,0)
		[Toggle(_SNOWONOFF_ON)] _SNOWONOFF("SNOW ON/OFF", Float) = 0
		_SnowFade("Snow Fade", Range( 0 , 1)) = 0.83
		_SnowCoverage("Snow Coverage", Range( 0 , 1)) = 0.45
		_SnowAmount("Snow Amount", Range( 0 , 1)) = 1
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.5
		#pragma multi_compile_instancing
		#pragma shader_feature_local _SNOWONOFF_ON
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			float2 uv2_texcoord2;
		};

		uniform float _CUSTOMCOLORSTINTING;
		uniform sampler2D _BaseTexture;
		uniform float4 _GroundColor;
		uniform float4 _TopColor;
		uniform float _Gradient;
		uniform float _GradientPower;
		uniform float _SnowAmount;
		uniform float _SnowFade;
		uniform float _SnowCoverage;
		uniform float4 _DecalsColor;
		uniform sampler2D _DecalsTexture;
		uniform float2 _Vector0;
		uniform float _Smoothness;


		float4 CalculateContrast( float contrastValue, float4 colorTarget )
		{
			float t = 0.5 * ( 1.0 - contrastValue );
			return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_BaseTexture2 = i.uv_texcoord;
			float4 tex2DNode2 = tex2D( _BaseTexture, uv_BaseTexture2 );
			float4 ase_vertex4Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 transform631 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float clampResult555 = clamp( pow( ( (0.1 + (transform631.y - 0.0) * (1.0 - 0.1) / (1.0 - 0.0)) * _Gradient ) , _GradientPower ) , 0.0 , 1.0 );
			float4 lerpResult557 = lerp( _GroundColor , _TopColor , clampResult555);
			float4 Gradient558 = lerpResult557;
			float grayscale180 = dot(tex2DNode2.rgb, float3(0.299,0.587,0.114));
			float saferPower568 = max( grayscale180 , 0.0001 );
			float4 temp_cast_1 = (pow( saferPower568 , 0.5 )).xxxx;
			float4 lerpResult595 = lerp( tex2DNode2 , ( Gradient558 * CalculateContrast(1.8,temp_cast_1) ) , float4( 1,1,1,0 ));
			float4 COLOR502 = (( _CUSTOMCOLORSTINTING )?( lerpResult595 ):( tex2DNode2 ));
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float4 color443 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
			float fresnelNdotV454 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode454 = ( 0.11 + 1.0 * pow( 1.0 - fresnelNdotV454, color443.r ) );
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float dotResult450 = dot( ase_normWorldNormal , float3(0,1,0) );
			float smoothstepResult531 = smoothstep( 0.0 , _SnowFade , ( dotResult450 + (-1.0 + (_SnowCoverage - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ));
			float SNOW489 = ( ( (0.0 + (_SnowAmount - 0.0) * (10.0 - 0.0) / (1.0 - 0.0)) * fresnelNode454 ) * smoothstepResult531 );
			#ifdef _SNOWONOFF_ON
				float4 staticSwitch372 = ( SNOW489 + COLOR502 );
			#else
				float4 staticSwitch372 = COLOR502;
			#endif
			float4 decalscolor621 = _DecalsColor;
			float2 uv2_TexCoord626 = i.uv2_texcoord2 + _Vector0;
			float DECALSMASK620 = tex2D( _DecalsTexture, uv2_TexCoord626 ).a;
			float4 lerpResult622 = lerp( staticSwitch372 , decalscolor621 , DECALSMASK620);
			o.Albedo = lerpResult622.rgb;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.5
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float4 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.customPack1.zw = customInputData.uv2_texcoord2;
				o.customPack1.zw = v.texcoord1;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				surfIN.uv2_texcoord2 = IN.customPack1.zw;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
7;1;1906;1029;5164.823;2089.785;4.563278;True;False
Node;AmplifyShaderEditor.PosVertexDataNode;545;-2259.787,-509.075;Inherit;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;544;-1779.585,-1129.231;Inherit;False;1754.419;983.1141;GRADIENT;10;557;556;553;555;572;547;571;573;546;558;GRADIENT;1,1,1,1;0;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;631;-2040.308,-506.654;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;573;-1722.476,-608.7739;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;546;-1744.844,-411.1584;Float;False;Property;_Gradient;Gradient ;4;0;Create;True;0;0;0;False;0;False;1;0.08;0;0.2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;547;-1374.914,-680.115;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;39;-2704.9,-37.7472;Inherit;False;2729.862;955.7487;COLOR;10;502;336;352;180;2;127;567;568;595;615;COLOR;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;571;-1454.28,-426.8416;Inherit;False;Property;_GradientPower;Gradient Power;5;0;Create;True;0;0;0;False;0;False;1;2.92;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;127;-2616.817,68.34903;Inherit;True;Property;_BaseTexture;Base Texture;0;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.PowerNode;572;-1137.034,-682.8345;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;553;-1689.1,-1070.706;Float;False;Property;_TopColor;Top Color;3;1;[HDR];Create;True;0;0;0;False;0;False;0.4811321,0.4036026,0.2382966,1;0.1886792,0.1282646,0.09878959,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;555;-882.071,-704.351;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-2366.745,67.82262;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;556;-1680.195,-881.4604;Float;False;Property;_GroundColor;Ground Color;2;1;[HDR];Create;True;0;0;0;False;0;False;0.08490568,0.05234205,0.04846032,1;0.0627451,0.04117647,0.02941176,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;557;-585.7902,-984.1019;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;363;-1703.223,1063.079;Inherit;False;1693.406;1367.284;Comment;15;489;441;467;458;531;532;452;454;535;446;455;443;445;450;442;SNOW;1,1,1,1;0;0
Node;AmplifyShaderEditor.TFHCGrayscale;180;-2001.596,176.2859;Inherit;True;1;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;441;-1676.107,1623.947;Inherit;True;True;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PowerNode;568;-1764.454,188.4758;Inherit;True;True;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;442;-1474.192,1849.329;Inherit;False;Constant;_SnowDirection;Snow Direction;11;0;Create;True;0;0;0;False;0;False;0,1,0;0,1,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;558;-287.8613,-978.0981;Inherit;False;Gradient;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;445;-1251.172,1890.081;Inherit;True;Property;_SnowCoverage;Snow Coverage;12;0;Create;True;0;0;0;False;0;False;0.45;0.45;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;567;-1507.181,190.6048;Inherit;True;2;1;COLOR;0,0,0,0;False;0;FLOAT;1.8;False;1;COLOR;0
Node;AmplifyShaderEditor.DotProductOpNode;450;-1189.189,1596.455;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;352;-1501.641,20.96606;Inherit;False;558;Gradient;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;446;-1590.046,1155.102;Inherit;False;Property;_SnowAmount;Snow Amount;13;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;455;-918.1221,1892.536;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;443;-1662.408,1417.836;Inherit;False;Constant;_Color1;Color 1;30;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;452;-1201.558,1166.712;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;535;-672.0255,1854.541;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;532;-938.6431,1447.447;Inherit;False;Property;_SnowFade;Snow Fade;11;0;Create;True;0;0;0;False;0;False;0.83;0.83;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;454;-1429.013,1329.544;Inherit;False;Standard;WorldNormal;ViewDir;True;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0.11;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;615;-1137.003,51.1685;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;531;-576.9562,1488.086;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;458;-946.8851,1280.101;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;595;-888.3122,178.4584;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;616;73.78273,-1311.525;Inherit;False;1745.965;569.649;Comment;7;621;620;619;618;617;626;630;DECALS;1,1,1,1;0;0
Node;AmplifyShaderEditor.ToggleSwitchNode;336;-630.8104,37.76899;Inherit;True;Property;_CUSTOMCOLORSTINTING;CUSTOM COLORS  TINTING;1;0;Create;True;0;0;0;False;0;False;1;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;467;-445.4955,1294.26;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;630;92.84077,-947.7274;Inherit;False;Property;_Vector0;Vector 0;14;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TexturePropertyNode;617;123.7828,-1269.525;Inherit;True;Property;_DecalsTexture;Decals Texture;6;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;d294e9544b9eca64188ea9d2482ea8a1;7fb9961d73580384d8908881f5952f73;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TextureCoordinatesNode;626;377.1985,-1030.73;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;489;-269.9945,1278.957;Inherit;True;SNOW;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;502;-200.9671,38.16975;Inherit;True;COLOR;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;618;652.0319,-911.2867;Inherit;False;Property;_DecalsColor;Decals Color;8;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;619;778.2088,-1266.971;Inherit;True;Property;_TextureSample1;Texture Sample 1;17;0;Create;True;0;0;0;False;0;False;-1;None;None;True;1;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;366;886.0975,403.3277;Inherit;False;489;SNOW;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;367;257.0651,120.0585;Inherit;True;502;COLOR;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;620;1207.602,-1167.118;Inherit;True;DECALSMASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;621;1005.032,-909.2867;Inherit;False;decalscolor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;368;1111.06,296.3205;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;624;1461.182,347.0516;Inherit;False;620;DECALSMASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;623;1465.766,36.16666;Inherit;False;621;decalscolor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;372;1282.186,125.3575;Inherit;True;Property;_SNOWONOFF;SNOW ON/OFF;10;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;130;2117.109,-90.25803;Inherit;False;Property;_MaskClipValue;Mask Clip Value;9;1;[HideInInspector];Fetch;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;566;1670.757,391.0894;Inherit;False;Property;_Smoothness;Smoothness;7;0;Create;True;0;0;0;False;0;False;0.7748996;0.7748996;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;622;1664.563,134.6327;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;62;2400.875,252.853;Float;False;True;-1;3;ASEMaterialInspector;0;0;Standard;Polytope Studio/PT_Woden_Sign_Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;True;130;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;631;0;545;0
WireConnection;573;0;631;2
WireConnection;547;0;573;0
WireConnection;547;1;546;0
WireConnection;572;0;547;0
WireConnection;572;1;571;0
WireConnection;555;0;572;0
WireConnection;2;0;127;0
WireConnection;557;0;556;0
WireConnection;557;1;553;0
WireConnection;557;2;555;0
WireConnection;180;0;2;0
WireConnection;568;0;180;0
WireConnection;558;0;557;0
WireConnection;567;1;568;0
WireConnection;450;0;441;0
WireConnection;450;1;442;0
WireConnection;455;0;445;0
WireConnection;452;0;446;0
WireConnection;535;0;450;0
WireConnection;535;1;455;0
WireConnection;454;3;443;0
WireConnection;615;0;352;0
WireConnection;615;1;567;0
WireConnection;531;0;535;0
WireConnection;531;2;532;0
WireConnection;458;0;452;0
WireConnection;458;1;454;0
WireConnection;595;0;2;0
WireConnection;595;1;615;0
WireConnection;336;0;2;0
WireConnection;336;1;595;0
WireConnection;467;0;458;0
WireConnection;467;1;531;0
WireConnection;626;1;630;0
WireConnection;489;0;467;0
WireConnection;502;0;336;0
WireConnection;619;0;617;0
WireConnection;619;1;626;0
WireConnection;620;0;619;4
WireConnection;621;0;618;0
WireConnection;368;0;366;0
WireConnection;368;1;367;0
WireConnection;372;1;367;0
WireConnection;372;0;368;0
WireConnection;622;0;372;0
WireConnection;622;1;623;0
WireConnection;622;2;624;0
WireConnection;62;0;622;0
WireConnection;62;4;566;0
ASEEND*/
//CHKSM=4B975B1218161E6A061EC43125914174453B1BD4