Shader "Custom/customwrap" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color("Color",Color) = (1,1,1,1)
		_RampTex("RampTex",2D) = "black" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf warp noambient


		sampler2D _MainTex;
		sampler2D _RampTex;
		float4 _Color;


		struct Input {
			float2 uv_MainTex;
		};

	

		void surf (Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color;
			o.Alpha = c.a;
		}

		float4 Lightingwarp( SurfaceOutput s, float3 lightDir, float atten){
			float ndot1 = dot(s.Normal,lightDir)* 0.5 + 0.5;
			float4 ramp = tex2D(_RampTex, float2(ndot1,0.5));

			float4 final;
			final.rgb =s.Albedo.rgb * ramp.rgb;
			final.a =s.Alpha;
			return final;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
