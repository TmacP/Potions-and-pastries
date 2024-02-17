Shader "Unlit/Grass"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1)
        _SquareColor ("Square Color", Color) = (0, 1, 0, 1)
        _SquareSize ("Square Size", Range(0, 1)) = 0.3
        _GridSize ("Grid Size", Range(1, 10)) = 5
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
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
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _BaseColor;
            float4 _SquareColor;
            float _SquareSize;
            float _GridSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Convert vertex position to UV space (0 to 1 range)
                o.uv = v.vertex.xz * 0.1 * _GridSize; // Adjusted UV calculation for terrain
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate grid cell size
                float cellSize = 1.0 / _GridSize;

                // Calculate cell index in grid
                float2 cellIndex = floor(i.uv / cellSize);

                // Calculate position of bottom left corner of the square in UV space
                float2 rectMin = cellIndex * cellSize + float2(0.1, 0.1) * cellSize;
                // Calculate position of top right corner of the square in UV space
                float2 rectMax = rectMin + _SquareSize * cellSize;

                // Check if the fragment is inside the square area
                float insideRect = (i.uv.x > rectMin.x && i.uv.x < rectMax.x && i.uv.y > rectMin.y && i.uv.y < rectMax.y) ? 1.0 : 0.0;

                // Mix colors based on whether the fragment is inside the square
                fixed4 color = _BaseColor * (1.0 - insideRect) + _SquareColor * insideRect;

                return color;
            }
            ENDCG
        }
    }
}
