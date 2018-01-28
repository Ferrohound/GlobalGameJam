Shader "Custom/NPCShader" {
   Properties {
      _MainTex ("Texture Image", 2D) = "white" {}
      _ScaleX ("Scale X", Float) = 1.0
      _ScaleY ("Scale Y", Float) = 1.0
	  _OutlineColor ("Outline Color", Color) = (0,0,0,1)
	  _Outline ("Outline width", Range (0.0, 1)) = .005
   }
   
   CGINCLUDE
	#include "UnityCG.cginc"
	
	uniform sampler2D _MainTex;    
	float4 _MainTex_TexelSize;
    
         uniform float _ScaleX;
         uniform float _ScaleY;
		 uniform float _Outline;
		uniform float4 _OutlineColor;
	
	struct vertexInput {
            float4 vertex : POSITION;
            float4 tex : TEXCOORD0;
			float3 normal : NORMAL;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 tex : TEXCOORD0;
         };
		 
		 
   vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;

            output.pos = mul(UNITY_MATRIX_P, 
              mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0))
              + float4(input.vertex.x, input.vertex.y, 0.0, 0.0)
              * float4(_ScaleX, _ScaleY, 1.0, 1.0));
 
            output.tex = input.tex;

            return output;
         }
		ENDCG
		 
   SubShader {
	   Tags { "PerformanceChecks"="False" "Glows"="True" }
	   
	   Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			//Cull Off
			ZWrite Off
			ZTest Always
			ColorMask RGB // alpha not used
			
			/*uniform sampler2D _MainTex;        
			 uniform float _ScaleX;
			 uniform float _ScaleY;
			 uniform float _Outline;
			uniform float4 _OutlineColor;*/
 
			// you can choose what kind of blending mode you want for the outline
			Blend SrcAlpha OneMinusSrcAlpha // Normal
			//Blend One One // Additive
			//Blend One OneMinusDstColor // Soft Additive
			//Blend DstColor Zero // Multiplicative
			//Blend DstColor SrcColor // 2x Multiplicative
			 
			CGPROGRAM
			#pragma vertex v2
			#pragma fragment frag
			
			vertexOutput v2(vertexInput input) 
			 {
				vertexOutput output;

				output.pos = mul(UNITY_MATRIX_P, 
				  mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0))
				  + float4(input.vertex.x, input.vertex.y, 0.0, 0.0)
				  * float4(_ScaleX, _ScaleY, 1.0, 1.0));
	 
				output.tex = input.tex;
				
				//float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, input.normal);
				//float2 offset = TransformViewToProjection(norm.xy);
				//float2 offset = float2(_);
				
				//float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, input.normal);
				//float2 offset = TransformViewToProjection(norm.xy);
			 
				//output.pos.xy += offset * output.pos.z * _Outline;

				return output;
			 }
					 
			half4 frag(vertexOutput i) :COLOR {
				float4 tex = tex2D(_MainTex, i.tex.xy);
				if(tex.a == 0)
					discard;
				return _OutlineColor;
				//return tex;
			}
			ENDCG
		}
		
		
		Pass {   
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag

         // User-specified uniforms            
       /*  uniform sampler2D _MainTex;        
         uniform float _ScaleX;
         uniform float _ScaleY;
		 uniform float _Outline;
		uniform float4 _OutlineColor;*/
 
         
 
         float4 frag(vertexOutput input) : COLOR
         {
			 float4 tex = tex2D(_MainTex, input.tex.xy);
			 
			 				
				//=================================REMOVE THIS ASAP
			if (_Outline > 0 && tex.a != 0) {
                    // Get the neighbouring four pixels.
				fixed4 pixelUp = 
					tex2D(_MainTex, input.tex.xy + fixed2(0, _MainTex_TexelSize.y));
				fixed4 pixelDown = 
					tex2D(_MainTex, input.tex.xy - fixed2(0, _MainTex_TexelSize.y));
				fixed4 pixelRight = 
					tex2D(_MainTex, input.tex.xy + fixed2(_MainTex_TexelSize.x, 0));
				fixed4 pixelLeft = 
					tex2D(_MainTex, input.tex.xy - fixed2(_MainTex_TexelSize.x, 0));

				// If one of the neighbouring pixels is invisible, we render an outline.
				if (pixelUp.a * pixelDown.a * pixelRight.a * pixelLeft.a == 0) 
				{
					tex.rgba = fixed4(1, 1, 1, 1) * _OutlineColor;
				}
			}

			//tex.rgb *= tex.a;

			//return tex;
			 
			 
			 
			if(tex.a == 0)
				discard;
			return tex;
            //return tex2D(_MainTex, float2(input.tex.xy));   
         }
 
         ENDCG
      }
   }
}
