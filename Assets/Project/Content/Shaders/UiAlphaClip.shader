// UI shader with alpha clip for invisible fragments.
// Keeps transparent blending and stencil (masks, ScrollRect).
// Discards fragments with alpha below _Cutoff — avoids blend unit work on invisible pixels.
Shader "Project/UI/AlphaClip"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _Cutoff ("Alpha Cutoff", Range(0, 0.5)) = 0.01

        _StencilComp    ("Stencil Comparison", Float) = 8
        _Stencil        ("Stencil ID",         Float) = 0
        _StencilOp      ("Stencil Operation",  Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask  ("Stencil Read Mask",  Float) = 255
        _ColorMask      ("Color Mask", Float) = 15

        _UIMaskSoftnessX ("Mask Softness X", Float) = 1
        _UIMaskSoftnessY ("Mask Softness Y", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue"              = "Transparent"
            "IgnoreProjector"    = "True"
            "RenderType"         = "Transparent"
            "PreviewType"        = "Plane"
            "CanUseSpriteAtlas"  = "True"
        }

        Stencil
        {
            Ref       [_Stencil]
            Comp      [_StencilComp]
            Pass      [_StencilOp]
            ReadMask  [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "UIAlphaClip"

            CGPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct Attributes
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 vertex        : SV_POSITION;
                fixed4 color         : COLOR;
                float2 texcoord      : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float4 mask          : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4    _Color;
            fixed4    _TextureSampleAdd;
            float4    _ClipRect;
            float4    _MainTex_ST;
            float     _UIMaskSoftnessX;
            float     _UIMaskSoftnessY;
            half      _Cutoff;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                OUT.worldPosition = IN.vertex;
                OUT.vertex        = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord      = TRANSFORM_TEX(IN.texcoord, _MainTex);
                OUT.color         = IN.color * _Color;

                // Canvas rect clipping mask
                float2 pixelSize = OUT.vertex.w / (abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy)));
                float4 rect      = clamp(_ClipRect, -2e10, 2e10);
                OUT.mask = float4(IN.vertex.xy * 2 - rect.xy - rect.zw,
                                  0.25 / (0.25 * float2(_UIMaskSoftnessX, _UIMaskSoftnessY) + abs(pixelSize)));
                return OUT;
            }

            fixed4 frag(Varyings IN) : SV_Target
            {
                fixed4 col = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                #ifdef UNITY_UI_CLIP_RECT
                col.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                // Discard fully transparent fragments — saves blend unit work
                clip(col.a - _Cutoff);

                return col;
            }
            ENDCG
        }
    }
}
