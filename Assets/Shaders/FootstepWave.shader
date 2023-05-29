Shader "Custom/FootstepWave"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _WaveAmp ("Wave Amplitude", Range(0,.5)) = 0.01
        _WaveCount ("Wave Count", Integer) = 5
        _Speed ("Speed", Range(0,1)) = 0.1
        _StepOne ("Step One", Vector) = (0,0,0,0)
        _InnerFadeBlackDistanceOne("Inner Fade Black Distance One", Range(0,1)) = 0
        _OuterFadeBlackDistanceOne ("Outer Fade Black Distance One", Range(0,1)) = 0
        _StepTwo ("Step Two", Vector) = (0,0,0,0)
        _InnerFadeBlackDistanceTwo("Inner Fade Black Distance Two", Range(0,1)) = 0
        _OuterFadeBlackDistanceTwo ("Outer Fade Black Distance Two", Range(0,1)) = 0
        _StepThree ("Step Three", Vector) = (0,0,0,0)
        _InnerFadeBlackDistanceThree("Inner Fade Black Distance Three", Range(0,1)) = 0
        _OuterFadeBlackDistanceThree ("Outer Fade Black Distance Three", Range(0,1)) = 0
        _StepFour ("Step Four", Vector) = (0,0,0,0)
        _InnerFadeBlackDistanceFour("Inner Fade Black Distance Four", Range(0,1)) = 0
        _OuterFadeBlackDistanceFour ("Outer Fade Black Distance Four", Range(0,1)) = 0
        _StepFive ("Step Five", Vector) = (0,0,0,0)
        _InnerFadeBlackDistanceFive("Inner Fade Black Distance Five", Range(0,1)) = 0
        _OuterFadeBlackDistanceFive ("Outer Fade Black Distance Five", Range(0,1)) = 0
        // _FootstepMultiplier ("Footstep Multiplier", Range(0,1)) = 0
        _OuterFadeDistance ("Outer Fade Distance", Range(0,1)) = 1
        _InnerFadeDistance ("Inner Fade Distance", Range(0,1)) = 0
        
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
        int _WaveCount;
        float _Speed;
        float2 _StepOne;
        float _InnerFadeBlackDistanceOne;
        float _OuterFadeBlackDistanceOne;
        float2 _StepTwo;
        float _InnerFadeBlackDistanceTwo;
        float _OuterFadeBlackDistanceTwo;
        float2 _StepThree;
        float _InnerFadeBlackDistanceThree;
        float _OuterFadeBlackDistanceThree;
        float2 _StepFour;
        float _InnerFadeBlackDistanceFour;
        float _OuterFadeBlackDistanceFour;
        float2 _StepFive;
        float _InnerFadeBlackDistanceFive;
        float _OuterFadeBlackDistanceFive;
        // float _FootstepMultiplier; // can maybe kill this.
        float _OuterFadeDistance;
        float _InnerFadeDistance;
        

        float GetWave( float2 uv, float2 stepPosition, float innerBlackDistance, float outerBlackDistance ) {
            float2 dis = 10 * distance(uv, stepPosition);
            float wave = cos( (dis - _Time.y * _Speed ) * TAU * _WaveCount ) * 0.5 + 0.5;
            wave *= saturate(1 - ((1/_OuterFadeDistance) * (dis - outerBlackDistance)));
            wave *= saturate((1/_InnerFadeDistance) * (dis - innerBlackDistance));
            
            // return wave;
            return wave; //* _FootstepMultiplier;

            // float2 uvsCentered = uv * 2 - 1;
            // float2 distanceToPoint = distance(uvsCentered, _Point.xy);

            // float wave = cos( (distanceToPoint - _Time.y * _Speed ) * TAU * _WaveCount ) * 0.5 + 0.5;
            // wave *= saturate(1 - distanceToPoint);

            // return wave;
        }

        void vert(inout appdata_full v) {
            // v.vertex.y = GetWave(v.texcoord) * _WaveAmp * _FootstepMultiplier;  //* saturate(lerp(1, 0, _Time.y/3));

            v.vertex.y = _WaveAmp * (
                GetWave(v.texcoord, _StepOne, _InnerFadeBlackDistanceOne, _OuterFadeBlackDistanceOne) + 
                GetWave(v.texcoord, _StepTwo, _InnerFadeBlackDistanceTwo, _OuterFadeBlackDistanceTwo) +
                GetWave(v.texcoord, _StepThree, _InnerFadeBlackDistanceThree, _OuterFadeBlackDistanceThree) +
                GetWave(v.texcoord, _StepFour, _InnerFadeBlackDistanceFour, _OuterFadeBlackDistanceFour) +
                GetWave(v.texcoord, _StepFive, _InnerFadeBlackDistanceFive, _OuterFadeBlackDistanceFive)
            );
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 tex = tex2D (_MainTex, IN.uv_MainTex);
            // float remappedWave = GetWave(IN.uv_MainTex) *_Brightness + _Brightness;
            o.Albedo = _Color * tex;
            // o.Albedo = _Color * GetWave(IN.uv_MainTex, _StepOne);

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
