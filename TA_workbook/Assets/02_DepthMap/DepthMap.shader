Shader "Custom/URPDisplayDepth"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags{"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;

            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = UnityObjectToUV(v.uv);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // sample the depth texture
                float depth = tex2D(_CameraDepthTexture, i.uv.xy).r;
                // convert to linear depth
                depth = Linear01Depth(depth);
                return half4(depth, depth, depth, 1.0);
            }
            ENDCG
        }
    }
}
