Shader "Custom/CentralWaveTest"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Brightness ("Brightness", Range(0,1)) = 0.8
        _WaveAmp ("Wave Amplitude", Range(0,2)) = 1
        _WaveCount ("Wave Count", Integer) = 5
        _Speed ("Speed", Range(0,1)) = 0.1
        _DropOne ("Drop One Location", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        #define TAU 6.28318530718

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float _WaveAmp;
        float _Brightness;
        int _WaveCount;
        float _Speed;

        float GetWave( float2 uv ) {
            float2 uvsCentered = uv * 2 - 1;
            float radialDistance = length(uvsCentered);
            float wave = cos( (radialDistance - _Time.y * _Speed ) * TAU * _WaveCount ) * 0.5 + 0.5;
            wave *= saturate(1 - radialDistance);
            return wave;

            // float2 distanceToDropOne = distance(uv, _DropOne.xy);
            // float wave1 = cos( (distanceToDropOne - _Time.y * _Speed ) * TAU * _WaveCount ) * 0.5 + 0.5;
            // wave1 *= (.5 - distanceToDropOne);

            // return wave1;
        }

        void vert(inout appdata_full v) {
            v.vertex.y = GetWave(v.texcoord) * _WaveAmp;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 tex = tex2D (_MainTex, IN.uv_MainTex);
            float remappedWave = GetWave(IN.uv_MainTex);
            o.Albedo = remappedWave * _Color * tex;

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
