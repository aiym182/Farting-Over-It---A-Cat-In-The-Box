Shader "Custom/uiShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Transparency ("Transparency", Range(0.0,1))=1
	}
	SubShader {
		Tags { "Queue" = "Transparent" "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha:fade 



		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

	
		fixed4 _Color;
		float _Transparency;



		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a * _Transparency;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
