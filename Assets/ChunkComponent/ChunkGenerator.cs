using System;
using UnityEngine;

public static class ChunkGenerator
{
    public static int[,,] Generate(ChunkSize chunkSize)
    {
        int[,,] result = new int[chunkSize.Width, chunkSize.Height, chunkSize.Width];
        for (int x = 0; x < chunkSize.Width; x++)
        {
            for (int z = 0; z < chunkSize.Width; z++)
            {
                float height = Math.Clamp(Mathf.PerlinNoise(x * 0.2f, z * 0.2f) * 5, 1, chunkSize.Height);

                for (int y = 0; y < height; y++)
                {
                    result[x, y, z] = 1;
                }
            }
        }

        return result;
    }
}