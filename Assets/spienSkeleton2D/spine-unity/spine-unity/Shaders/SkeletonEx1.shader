Shader "Spine/SkeletonEx1" {
	Properties {
		_TintColor ("Tint Color", Color) = (1,1,1,1)
		_Cutoff ("Shadow alpha cutoff", Range(0,1)) = 0.1
		_MainTex ("Texture to blend", 2D) = "black" {}
		_Landification("Landification",float) = 1
		_Intensity("Intensity",float) = 1
	}
	// 2 texture stage GPUs
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 100

		Cull Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		Lighting Off

		Pass {
//			ColorMaterial AmbientAndDiffuse
//			SetTexture [_MainTex] {
//				Combine texture * primary
//			}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			
			struct v2f { 
			float4 vert : SV_POSITION;
			fixed4 color : COLOR;
			float2  uv : TEXCOORD1;
			};

			uniform float4 _MainTex_ST;

			v2f vert (appdata_t v) {
				v2f o;
				o.vert = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color= v.color;
				o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			uniform sampler2D _MainTex;
			uniform float _Landification;
			uniform fixed4 _TintColor;
			uniform float _Intensity;
			
			float4 frag (v2f i) : COLOR {
				fixed4 texcol = tex2D(_MainTex, i.uv)*i.color*_TintColor*_Intensity;
				if(_Landification == 0)
				{
					float gray = 0.3*texcol.r+0.59*texcol.g+0.11*texcol.b;
					texcol.r=texcol.g=texcol.b=gray*0.6;
				}
				return texcol;
			}
			ENDCG

		}

		Pass {
			Name "Caster"
			Tags { "LightMode"="ShadowCaster" }
			Offset 1, 1
			
			Fog { Mode Off }
			ZWrite On
			ZTest LEqual
			Cull Off
			Lighting Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_shadowcaster
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			struct v2f { 
				V2F_SHADOW_CASTER;
				float2  uv : TEXCOORD1;
			};

			uniform float4 _MainTex_ST;
			
			v2f vert (appdata_base v) {
				v2f o;
				TRANSFER_SHADOW_CASTER(o)
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			uniform sampler2D _MainTex;
			uniform fixed _Cutoff;

			float4 frag (v2f i) : COLOR {
				fixed4 texcol = tex2D(_MainTex, i.uv);
				float gray = 0.3*texcol.r+0.59*texcol.g+0.11*texcol.b;
				texcol.r=texcol.g=texcol.b=0;
				clip(texcol.a - _Cutoff);
				SHADOW_CASTER_FRAGMENT(i)
			}
			ENDCG
		}
	}
	// 1 texture stage GPUs
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 100

		Cull Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		Lighting Off

		Pass {
			ColorMaterial AmbientAndDiffuse
			SetTexture [_MainTex] {
				Combine texture * primary DOUBLE, texture * primary
			}
		}
	}
}