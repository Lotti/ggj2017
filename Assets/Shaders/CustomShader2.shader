
Shader "Custom/roomShader" {
	Properties {
		_MainColor ("Main Color (RGBA)", Color) = (0.7, 1, 1, 0)
		_WaveColor ("Wave Color (RGBA)", Color) = (0.7, 1, 1, 0)
	}
	SubShader {
		Tags {"Queue"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha // Alpha blend

		Pass {
			CGPROGRAM
			#pragma vertex vert             
			#pragma fragment frag

			struct vertInput {
				float4 pos : POSITION;
			};  

			struct vertOutput {
				float4 pos : POSITION;
				fixed3 worldPos : TEXCOORD1;
			};

			vertOutput vert(vertInput input) {
				vertOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, input.pos);
				o.worldPos = mul(unity_ObjectToWorld, input.pos).xyz;
				return o;
			}

			uniform int _Points_Length = 20;
			uniform float3 _Points [20];
			uniform float3 _StartPoints [20];		// (x, y, z) = position
			uniform float2 _Properties [20];	// x = radius, y = intensity

			float4 _MainColor;
			float4 _WaveColor;

			half4 frag(vertOutput output) : COLOR 
			{
				// Loops over all the points
				half h = 0;
				for (int i = 0; i < _Points_Length; i++)
				{
					// Calculates the contribution of each point
					//half di = distance(output.worldPos, _Points[i].xyz);

					half distMesh = distance(output.worldPos, _Points[i].xyz) ;
					half distP = distance(_StartPoints[i].xyz, _Points[i].xyz);

					if( distMesh < distP && (distMesh-distP) < _Properties[i].x )
					{
						//half ri = _Properties[i].x;
						half hi = 1 - saturate( distMesh / distP );
						h += hi * _Properties[i].y;
					}
				}

				h=sin(h);

				return _MainColor + _WaveColor * h;

			}
			ENDCG	
		}
	} 
	Fallback "Diffuse"
}