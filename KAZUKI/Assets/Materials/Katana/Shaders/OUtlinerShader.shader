Shader "Custom/OutlineShader"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }
            
            // Renderizar el contorno
            ZWrite On
            Cull Front
            Offset 5, 5
            
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            // Color del contorno
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            
            struct v2f
            {
                float4 pos : POSITION;
            };
            
            float _OutlineWidth;
            float4 _OutlineColor;

            v2f vert(appdata_t v)
            {
                v2f o;
                // Desplazar los vértices hacia afuera para crear el contorno
                o.pos = UnityObjectToClipPos(v.vertex + v.normal * _OutlineWidth);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}