Shader "Blind/Outline"
{
    Properties {
        _NoiseTexture("Noise Texture", 2D) = "white" {}
        _NoiseScale("Noise Scale", Float) = 1
        _NoiseRange("Noise Min Range", Range(0.1, 1)) = 0.8
        _NoiseSpeed("Noise Speed", Vector) = (0, 0, 0, 0)
    }

    SubShader
    {
        Tags {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "Queue"="Transparent+0"
        }

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _Position0;
            float4 _Position1;
            float4 _Position2;
            float4 _Position3;
            float4 _Position4;
            float4 _Position5;
            float4 _Position6;
            float4 _Position7;
            float4 _Position8;
            float4 _Position9;
            float4 _Position10;
            float4 _Position11;
            float4 _Position12;
            float4 _Position13;
            float4 _Position14;
            float4 _Position15;
            float4 _Position16;
            float4 _Position17;
            float4 _Position18;
            float4 _Position19;

            float _ScriptTime0;
            float _ScriptTime1;
            float _ScriptTime2;
            float _ScriptTime3;
            float _ScriptTime4;
            float _ScriptTime5;
            float _ScriptTime6;
            float _ScriptTime7;
            float _ScriptTime8;
            float _ScriptTime9;
            float _ScriptTime10;
            float _ScriptTime11;
            float _ScriptTime12;
            float _ScriptTime13;
            float _ScriptTime14;
            float _ScriptTime15;
            float _ScriptTime16;
            float _ScriptTime17;
            float _ScriptTime18;
            float _ScriptTime19;

            sampler2D _NoiseTexture;
            float4 _NoiseTexture_ST;
            float4 _NoiseSpeed;
            float _NoiseScale;
            float _NoiseRange;


            struct Attributes
            {
                float4 position : POSITION;
                float4 uv : TEXCOORD0;
                float3 normal: NORMAL0;
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float4 position  : SV_POSITION;
                float4 worldPos : TEXCOORD1;
                float3 normal: NORMAL0;
            };

            float InverseLerp ( float a, float b, float v) {
                return (v - a) / (b - a);
            }

            float4 Wave (float4 position, float scriptTime) {
                float interval = scriptTime;
                float4 pos = (position + interval + 1.6969) * .5;
                float4 sinValue = sin(pos) * 4 - 3;
                float4 wave = max(0, sinValue);
                return wave;
            }

            float4 GetFragColor (float4 position, float4 worldPosition, float scriptTime) {
                float4 dis = distance(position.xyz, worldPosition.xyz);
                float4 color = 1 - saturate( dis / position.a );

                return color * Wave(dis, 1 - scriptTime);
            }

            float4 GetSphereNoise(float4 worldPos) {
                float4 sphereRNoise = 0;

                sphereRNoise += GetFragColor( _Position0, worldPos, _ScriptTime0 );
                sphereRNoise += GetFragColor( _Position1, worldPos, _ScriptTime1 );
                sphereRNoise += GetFragColor( _Position2, worldPos, _ScriptTime2 );
                sphereRNoise += GetFragColor( _Position3, worldPos, _ScriptTime3 );
                sphereRNoise += GetFragColor( _Position4, worldPos, _ScriptTime4 );
                sphereRNoise += GetFragColor( _Position5, worldPos, _ScriptTime5 );
                sphereRNoise += GetFragColor( _Position6, worldPos, _ScriptTime6 );
                sphereRNoise += GetFragColor( _Position7, worldPos, _ScriptTime7 );
                sphereRNoise += GetFragColor( _Position8, worldPos, _ScriptTime8 );
                sphereRNoise += GetFragColor( _Position9, worldPos, _ScriptTime9 );
                sphereRNoise += GetFragColor( _Position10, worldPos, _ScriptTime10 );
                sphereRNoise += GetFragColor( _Position11, worldPos, _ScriptTime11 );
                sphereRNoise += GetFragColor( _Position12, worldPos, _ScriptTime12 );
                sphereRNoise += GetFragColor( _Position13, worldPos, _ScriptTime13 );
                sphereRNoise += GetFragColor( _Position14, worldPos, _ScriptTime14 );
                sphereRNoise += GetFragColor( _Position15, worldPos, _ScriptTime15 );
                sphereRNoise += GetFragColor( _Position16, worldPos, _ScriptTime16 );
                sphereRNoise += GetFragColor( _Position17, worldPos, _ScriptTime17 );
                sphereRNoise += GetFragColor( _Position18, worldPos, _ScriptTime18 );
                sphereRNoise += GetFragColor( _Position19, worldPos, _ScriptTime19 );

                return sphereRNoise;
            }

            Varyings vert(Attributes IN)
            {
                float4 sphereRNoise = GetSphereNoise(mul (unity_ObjectToWorld, IN.position));
                Varyings OUT;
                OUT.worldPos = mul (unity_ObjectToWorld, IN.position);
                
                IN.position.z += sphereRNoise / 12;
                OUT.position = UnityObjectToClipPos(IN.position);
                OUT.uv = TRANSFORM_TEX(IN.uv, _NoiseTexture) * _NoiseScale;

                OUT.normal = UnityObjectToWorldNormal(IN.normal);

                return OUT;
            }


            float4 frag(Varyings IN) : SV_Target
            {
                float4 sphereRNoise = GetSphereNoise(IN.worldPos);

                float noise = 1 - tex2D(_NoiseTexture, IN.uv + _Time.y * _NoiseSpeed);
                float noiseInverseRange = 1 - _NoiseRange;

                sphereRNoise.a = 1;

                float4 noiseTextureRange = (1 - step(sphereRNoise, noiseInverseRange)) * noise;
                
                float4 gradient = InverseLerp(noiseInverseRange, noiseInverseRange + .15, sphereRNoise);
                gradient.a = 1;

                float4 color = gradient - (1 - noiseTextureRange);

                // return gradient * Wave(sphereRNoise, _ScriptTime);
                return color;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
