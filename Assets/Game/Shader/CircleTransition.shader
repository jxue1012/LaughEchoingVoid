Shader "Custom/UI/ScreenTransitionShaderWithCircle"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Center("Center", Vector) = (0.5, 0.5, 0, 0)
        _Radius("Radius", Float) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha
        AlphaTest Greater 0.01
        ColorMask RGBA

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #include "UnityUI.cginc"

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

            sampler2D _MainTex;
            float4 _Color;
            float2 _Center;
            float _Radius;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // 调整UV坐标以适应屏幕比例
                float aspectRatio = _ScreenParams.y / _ScreenParams.x;
                float2 scaledUV = i.uv;
                scaledUV -= 0.5;
                scaledUV.y *= aspectRatio;
                scaledUV += 0.5;

                // 计算距离并保持圆形
                float dist = distance(scaledUV, _Center);
                float alpha = smoothstep(_Radius - 0.01, _Radius, dist);

                // 应用UI Image的颜色
                half4 color = tex2D(_MainTex, i.uv) * _Color;
                color.a *= alpha;

                return color;
            }
            ENDCG
        }
    }
    FallBack "TextMeshPro/Distance Field"
}
