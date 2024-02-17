Shader "Unlit/Grass"
{
    Properties
    {
        // Base Color for the entire mesh
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1)

        // First square properties
        _SquareColor1 ("Square 1 Color", Color) = (0, 1, 0, 1)
        _SquareSize1 ("Square 1 Size", Range(0, 1)) = 0.3

        // Second square properties
        _SquareColor2 ("Square 2 Color", Color) = (0, 0, 1, 1)
        _SquareSize2 ("Square 2 Size", Range(0, 1)) = 0.3

        // Third square properties
        _SquareColor3 ("Square 3 Color", Color) = (1, 0, 0, 1)
        _SquareSize3 ("Square 3 Size", Range(0, 1)) = 0.3

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
            float4 _SquareColor1;
            float _SquareSize1;
            float4 _SquareColor2;
            float _SquareSize2;
            float4 _SquareColor3;
            float _SquareSize3;
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

                // Calculate random position within the cell for each square
                float2 rectMin1 = cellIndex * cellSize + _SquareSize1 * cellSize * 0.5;
                float2 rectMax1 = cellIndex * cellSize + (1 - _SquareSize1 * 0.5) * cellSize;
                float2 randomPos1 = lerp(rectMin1, rectMax1, frac(sin(dot(cellIndex, float2(12.9898, 78.233))) * 43758.5453));
                
                float2 rectMin2 = cellIndex * cellSize + _SquareSize2 * cellSize * 0.5;
                float2 rectMax2 = cellIndex * cellSize + (1 - _SquareSize2 * 0.5) * cellSize;
                float2 randomPos2 = lerp(rectMin2, rectMax2, frac(cos(dot(cellIndex, float2(4.6789, 1.234))) * 43758.5453));
                
                float2 rectMin3 = cellIndex * cellSize + _SquareSize3 * cellSize * 0.5;
                float2 rectMax3 = cellIndex * cellSize + (1 - _SquareSize3 * 0.5) * cellSize;
                float2 randomPos3 = lerp(rectMin3, rectMax3, frac(cos(dot(cellIndex, float2(2.3456, 9.876))) * 43758.5453));

                // Calculate random rotation for each square
                float randomRotation1 = frac(sin(dot(cellIndex, float2(12.9898, 78.233))) * 43758.5453) * 360.0;
                float randomRotation2 = frac(cos(dot(cellIndex, float2(4.6789, 1.234))) * 43758.5453) * 360.0;
                float randomRotation3 = frac(cos(dot(cellIndex, float2(2.3456, 9.876))) * 43758.5453) * 360.0;

                // Rotate UV coordinate based on random rotation for each square
                float2 rotatedUV1 = i.uv - randomPos1;
                rotatedUV1 = float2(rotatedUV1.x * cos(randomRotation1) - rotatedUV1.y * sin(randomRotation1),
                                    rotatedUV1.x * sin(randomRotation1) + rotatedUV1.y * cos(randomRotation1));
                rotatedUV1 += randomPos1;

                float2 rotatedUV2 = i.uv - randomPos2;
                rotatedUV2 = float2(rotatedUV2.x * cos(randomRotation2) - rotatedUV2.y * sin(randomRotation2),
                                    rotatedUV2.x * sin(randomRotation2) + rotatedUV2.y * cos(randomRotation2));
                rotatedUV2 += randomPos2;
                
                float2 rotatedUV3 = i.uv - randomPos3;
                rotatedUV3 = float2(rotatedUV3.x * cos(randomRotation3) - rotatedUV3.y * sin(randomRotation3),
                                    rotatedUV3.x * sin(randomRotation3) + rotatedUV3.y * cos(randomRotation3));
                rotatedUV3 += randomPos3;

                // Check if the fragment is inside each square area
                float insideRect1 = (rotatedUV1.x > rectMin1.x && rotatedUV1.x < rectMax1.x && rotatedUV1.y > rectMin1.y && rotatedUV1.y < rectMax1.y) ? 1.0 : 0.0;
                float insideRect2 = (rotatedUV2.x > rectMin2.x && rotatedUV2.x < rectMax2.x && rotatedUV2.y > rectMin2.y && rotatedUV2.y < rectMax2.y) ? 1.0 : 0.0;
                float insideRect3 = (rotatedUV3.x > rectMin3.x && rotatedUV3.x < rectMax3.x && rotatedUV3.y > rectMin3.y && rotatedUV3.y < rectMax3.y) ? 1.0 : 0.0;

                // Mix colors based on whether the fragment is inside each square
                fixed4 color = _BaseColor * (1.0 - insideRect1) * (1.0 - insideRect2) * (1.0 - insideRect3) +
                               _SquareColor1 * insideRect1 +
                               _SquareColor2 * insideRect2 +
                               _SquareColor3 * insideRect3;

                return color;
            }
            ENDCG
        }
    }
}
