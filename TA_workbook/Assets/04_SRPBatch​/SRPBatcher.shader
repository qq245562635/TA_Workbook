// Shader "Custom/SRPSupport"
// {
//     Properties
//     {
//         _Color1 ("Color 1", Color) = (1,1,1,1)
//         _Color2 ("Color 2", Color) = (1,1,1,1)
//     }
 
//     SubShader
//     {
//         Pass
//         {
//             HLSLPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #include "../Common.hlsl"
 
//             float4 _Color1;
//             float4 _Color2;
 
//             struct Attributes
//             {
//                 float3 positionOS : POSITION;
//             };
 
//             struct Varyings
//             {
//                 float4 positionCS : SV_POSITION;
//             };
 
//             Varyings vert (Attributes input)
//             {
//                 Varyings output;
//                 float3 positionWS = TransformObjectToWorld(input.positionOS);
//                 output.positionCS = TransformWorldToHClip(positionWS);
//                 return output;
//             }
 
//             float4 frag (Varyings input) : SV_TARGET
//             {
//                 return _Color1 * _Color2;
//             }
//             ENDHLSL
//         }
//     }
// }
