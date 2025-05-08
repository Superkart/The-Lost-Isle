Shader "Custom/GrassCutout"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
        // _RimColor ("Rim Color", Color) = (1,1,1,1)
        // _RimPower ("Rim Power", Range(0, 10)) = 3.0
        _SwayStrength ("Sway Strength", Range(0, 0.5)) = 0.1
        _SwaySpeed ("Sway Speed", Range(0, 10)) = 1.5
    }

    SubShader
    {
        Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" }
        LOD 200
        Cull Off

        CGPROGRAM
        #pragma surface surf Standard vertex:vert alphatest:_Cutoff


        sampler2D _MainTex;
        fixed4 _RimColor;
        float _RimPower;
        float _SwayStrength;
        float _SwaySpeed;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        void vert(inout appdata_full v)
        {
            // VERTEX SWAY BASED ON WORLD POSITION
            float offset = sin(_Time.y * _SwaySpeed + v.vertex.x * 2.0) * _SwayStrength;
            v.vertex.xz += float2(offset * 0.5, offset);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;

            // float rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
            // rim = pow(rim, _RimPower);
            // o.Emission = rim * _RimColor.rgb;
        }
        ENDCG
    }

    FallBack "Transparent/Cutout/Diffuse"
}
