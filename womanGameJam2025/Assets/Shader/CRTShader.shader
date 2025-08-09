Shader "Custom/CRTPostProcess"
{
    Properties
    {
        _TintColor ("Tint Color", Color) = (0.8, 1, 0.8, 1)
        _FlickerStrength ("Flicker Strength", Range(0, 0.1)) = 0.02
        _MainTex ("Base (Grab)", 2D) = "white" {}
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _TintColor;
            float _FlickerStrength;
            float4 _MainTex_TexelSize;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Slight flicker
                float flicker = 1.0 + (sin(_Time.y * 60.0) * _FlickerStrength);

                // Tint
                col.rgb *= _TintColor.rgb * flicker;

                // Simple vignette
                float2 center = i.uv - 0.5;
                float vignette = 1.0 - dot(center, center) * 2.0;
                vignette = saturate(vignette);
                col.rgb *= vignette;

                return col;
            }
            ENDCG
        }
    }
}
