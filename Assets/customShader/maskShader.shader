Shader "Custom/maskShader" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MasKTex ("Albedo (RGB)", 2D) = "white" {}
		_Transparency("CutOFF Value",Range(0.0,1.0)) = 0



	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha:fade 


		sampler2D _MainTex;
		sampler2D _MaskTex;
		float _Transparency;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MaskTex;
		};



		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 d = tex2D (_MaskTex, IN.uv_MaskTex);
			o.Albedo = lerp(c.rgb,d.rgb,c.a);
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
