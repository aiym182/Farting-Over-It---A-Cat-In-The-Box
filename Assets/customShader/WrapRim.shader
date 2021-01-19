Shader "Custom/WrapRim" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_RampTex("RampTex",2D) = "black" {}
		_RimColor("RimColor", Color) =(1,1,1,1)
		_RimPower ("Rim Power", Range(1,10)) = 3 
		[Toggle] _Enable("Rim Enable",float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf warp  


		sampler2D _MainTex;
		sampler2D _RampTex;
		float4 _RimColor;
		float _RimPower;
		float _Enable;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
			
		};

	

		void surf (Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			float rim = dot(o.Normal,IN.viewDir);
			if(_Enable == 1){
			o.Emission = pow(1-rim,_RimPower)* _RimColor;
			}
			else{
			}

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
