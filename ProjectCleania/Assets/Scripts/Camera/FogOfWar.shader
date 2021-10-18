Shader "Hidden/FogOfWar"
{
    Properties
    {
        _PrevTexture ("Previous Texture", 2D) = "white" {}
		_CurTexture ("Current Texture", 2D) = "white" {}
		_Color ("Color", Color) = (0, 0, 0, 0)
		_Blend ("Blend", Float) = 0
    }
    SubShader
    {
        Tags
		{
			"Queue" = "Transparent+100"
		}
        Pass
        {
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			ZTest Equal

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

			float4x4 unity_Projector;
			sampler2D _CurTexture;
			sampler2D _PrevTexture;
			fixed4 _Color;
			float _Blend;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = mul(unity_Projector, v.vertex);
                return o;
            }

            //sampler2D _MainTex;
			//sampler2D _SecondaryTex;

            fixed4 frag (v2f i) : SV_Target
            {
				float aPrev = tex2Dproj(_PrevTexture, i.uv).a;
				float aCur = tex2Dproj(_CurTexture, i.uv).a;

				_Color.a = lerp(aPrev, aCur, _Blend);
                //fixed4 col = tex2D(_MainTex, i.uv) + tex2D(_SecondaryTex, i.uv);
				
			//col.a = 2.0f - col.r * 1.5f - col.b * 0.5f;
				return _Color;
                //return fixed4(0, 0, 0, col.a);
            }
            ENDCG
        }
    }
}
