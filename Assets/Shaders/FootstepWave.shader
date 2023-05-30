Shader "Custom/FootstepWave"
{
    Properties
    {
        // Surface properties
        [Header(Surface Attributes)]
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        // Footstep properties
        [Header(Footstep Global)]
        _WaveAmp ("Wave Amplitude", Range(0,.5)) = 0.01
        _WaveCount ("Wave Count", Integer) = 5
        _Speed ("Speed", Range(0,1)) = 0.1
        // _FootstepMultiplier ("Footstep Multiplier", Range(0,1)) = 0
        _OuterFadeDistance ("Outer Fade Distance", Range(0,1)) = 1
        _InnerFadeDistance ("Inner Fade Distance", Range(0,1)) = 0

        // Steps
        [Header(Footsteps)]
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
        _Step6 ("Step 6", Vector) = (0,0,0,0)
        _InnerFadeBlackDistance6("Inner Fade Black Distance 6", Range(0,1)) = 0
        _OuterFadeBlackDistance6 ("Outer Fade Black Distance 6", Range(0,1)) = 0
        _Step7 ("Step 7", Vector) = (0,0,0,0)
        _InnerFadeBlackDistance7("Inner Fade Black Distance 7", Range(0,1)) = 0
        _OuterFadeBlackDistance7 ("Outer Fade Black Distance 7", Range(0,1)) = 0
        _Step8 ("Step 8", Vector) = (0,0,0,0)
        _InnerFadeBlackDistance8("Inner Fade Black Distance 8", Range(0,1)) = 0
        _OuterFadeBlackDistance8 ("Outer Fade Black Distance 8", Range(0,1)) = 0
        _Step9 ("Step 9", Vector) = (0,0,0,0)
        _InnerFadeBlackDistance9("Inner Fade Black Distance 9", Range(0,1)) = 0
        _OuterFadeBlackDistance9 ("Outer Fade Black Distance 9", Range(0,1)) = 0
        _Step10 ("Step 10", Vector) = (0,0,0,0)
        _InnerFadeBlackDistance10("Inner Fade Black Distance 10", Range(0,1)) = 0
        _OuterFadeBlackDistance10 ("Outer Fade Black Distance 10", Range(0,1)) = 0
        
        // Landing properties
        [Header(Landing Global)] 
        _LandWaveAmp ("Landing Wave Amplitude", Range(0,.5)) = 0.01
        _LandWaveCount ("Landing Wave Count", Integer) = 5
        _LandWaveSpeed ("Landing Speed", Range(0,1)) = 0.1
        _LandOuterFadeDistance ("Landing Outer Fade Distance", Range(0,1)) = 1
        _LandInnerFadeDistance ("Landing Inner Fade Distance", Range(0,1)) = 0

        [Header(Landings)] 
        _Land1 ("Landing 1", Vector) = (0,0,0,0)
        _LandInnerBlackDistance1("Landing Inner Black Distance 1", Range(0,1)) = 0
        _LandOuterBlackDistance1 ("Landing Outer Black Distance 1", Range(0,1)) = 0
        
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
        float _OuterFadeDistance;
        float _InnerFadeDistance;
        // float _FootstepMultiplier; // can maybe kill this.

        // Grounded footsteps
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
        float2 _Step6;
        float _InnerFadeBlackDistance6;
        float _OuterFadeBlackDistance6;
        float2 _Step7;
        float _InnerFadeBlackDistance7;
        float _OuterFadeBlackDistance7;
        float2 _Step8;
        float _InnerFadeBlackDistance8;
        float _OuterFadeBlackDistance8;
        float2 _Step9;
        float _InnerFadeBlackDistance9;
        float _OuterFadeBlackDistance9;
        float2 _Step10;
        float _InnerFadeBlackDistance10;
        float _OuterFadeBlackDistance10;
        float2 _Step11;
        float _InnerFadeBlackDistance11;
        float _OuterFadeBlackDistance11;
        float2 _Step12;
        float _InnerFadeBlackDistance12;
        float _OuterFadeBlackDistance12;
        float2 _Step13;
        float _InnerFadeBlackDistance13;
        float _OuterFadeBlackDistance13;
        float2 _Step14;
        float _InnerFadeBlackDistance14;
        float _OuterFadeBlackDistance14;
        float2 _Step15;
        float _InnerFadeBlackDistance15;
        float _OuterFadeBlackDistance15;
        float2 _Step16;
        float _InnerFadeBlackDistance16;
        float _OuterFadeBlackDistance16;
        float2 _Step17;
        float _InnerFadeBlackDistance17;
        float _OuterFadeBlackDistance17;
        float2 _Step18;
        float _InnerFadeBlackDistance18;
        float _OuterFadeBlackDistance18;
        float2 _Step19;
        float _InnerFadeBlackDistance19;
        float _OuterFadeBlackDistance19;
        float2 _Step20;
        float _InnerFadeBlackDistance20;
        float _OuterFadeBlackDistance20;

        // Landing
        float _LandWaveAmp;
        int _LandWaveCount;
        float _LandWaveSpeed;
        float _LandOuterFadeDistance;
        float _LandInnerFadeDistance;

        float2 _Land1;
        float _LandInnerBlackDistance1;
        float _LandOuterBlackDistance1;

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

        float GetLandingWave( float2 uv, float2 stepPosition, float innerBlackDistance, float outerBlackDistance ) {
            float2 dis = 10 * distance(uv, stepPosition);
            float wave = cos( (dis - _Time.y * _LandWaveSpeed ) * TAU * _LandWaveCount ) * 0.5 + 0.5;
            wave *= saturate(1 - ((1/_LandOuterFadeDistance) * (dis - outerBlackDistance)));
            wave *= saturate((1/_LandInnerFadeDistance) * (dis - innerBlackDistance));
            
            return wave;
        }

        void vert(inout appdata_full v) {
            // v.vertex.y = GetWave(v.texcoord) * _WaveAmp * _FootstepMultiplier;  //* saturate(lerp(1, 0, _Time.y/3));

            v.vertex.y = _WaveAmp * (
                GetWave(v.texcoord, _StepOne, _InnerFadeBlackDistanceOne, _OuterFadeBlackDistanceOne) + 
                GetWave(v.texcoord, _StepTwo, _InnerFadeBlackDistanceTwo, _OuterFadeBlackDistanceTwo) +
                GetWave(v.texcoord, _StepThree, _InnerFadeBlackDistanceThree, _OuterFadeBlackDistanceThree) +
                GetWave(v.texcoord, _StepFour, _InnerFadeBlackDistanceFour, _OuterFadeBlackDistanceFour) +
                GetWave(v.texcoord, _StepFive, _InnerFadeBlackDistanceFive, _OuterFadeBlackDistanceFive) +
                GetWave(v.texcoord, _Step6, _InnerFadeBlackDistance6, _OuterFadeBlackDistance6) +
                GetWave(v.texcoord, _Step7, _InnerFadeBlackDistance7, _OuterFadeBlackDistance7) +
                GetWave(v.texcoord, _Step8, _InnerFadeBlackDistance8, _OuterFadeBlackDistance8) +
                GetWave(v.texcoord, _Step9, _InnerFadeBlackDistance9, _OuterFadeBlackDistance9) +
                GetWave(v.texcoord, _Step10, _InnerFadeBlackDistance10, _OuterFadeBlackDistance10) +
                GetWave(v.texcoord, _Step11, _InnerFadeBlackDistance11, _OuterFadeBlackDistance11) +
                GetWave(v.texcoord, _Step12, _InnerFadeBlackDistance12, _OuterFadeBlackDistance12) +
                GetWave(v.texcoord, _Step13, _InnerFadeBlackDistance13, _OuterFadeBlackDistance13) +
                GetWave(v.texcoord, _Step14, _InnerFadeBlackDistance14, _OuterFadeBlackDistance14) +
                GetWave(v.texcoord, _Step15, _InnerFadeBlackDistance15, _OuterFadeBlackDistance15) +
                GetWave(v.texcoord, _Step16, _InnerFadeBlackDistance16, _OuterFadeBlackDistance16) +
                GetWave(v.texcoord, _Step17, _InnerFadeBlackDistance17, _OuterFadeBlackDistance17) +
                GetWave(v.texcoord, _Step18, _InnerFadeBlackDistance18, _OuterFadeBlackDistance18) +
                GetWave(v.texcoord, _Step19, _InnerFadeBlackDistance19, _OuterFadeBlackDistance19) +
                GetWave(v.texcoord, _Step20, _InnerFadeBlackDistance20, _OuterFadeBlackDistance20)
            ) +
            _LandWaveAmp * (
                GetLandingWave(v.texcoord, _Land1, _LandInnerBlackDistance1, _LandOuterBlackDistance1)
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
