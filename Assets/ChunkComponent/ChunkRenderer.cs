using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class ChunkRenderer : MonoBehaviour
{
    public ChunkSize ChunkSize;
    private List<Vector3> vertices = new List<Vector3>(); 
    private List<int> triangles = new List<int>();
    private MeshFilter meshFilter;
    

    private int[,,] blocks;
    public BlockSideParameters BlockSideParameters;
    
    private void Awake()
    {
        ChunkSize = new ChunkSize()
        {
            Width = 10,
            Height = 10,
        };
        
       

        blocks = ChunkGenerator.Generate(ChunkSize);
        
        InitChunk(ChunkSize.Width, ChunkSize.Height);
        GenerateMesh();
    }

    private void InitChunk(int width, int height)
    {
        meshFilter = GetComponent<MeshFilter>();
        ChunkSize = new ChunkSize()
        {
            Height = height,
            Width = width,
        };
        BlockSideParameters.SetAllSideAsActive();
    }

    public void RegenerateMesh()
    {
        vertices.Clear();
        triangles.Clear();
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        var mesh = new Mesh
        {
            name = "Chunk"
        };

        for (int x = 0; x < ChunkSize.Width; x++)
        {
            for (int y = 0; y < ChunkSize.Height; y++)
            {
                for (int z = 0; z < ChunkSize.Width; z++)
                {
                    int blockType = blocks[x, y, z];
                    
                    if (blockType == 0)
                    {
                        continue;
                    }

                    GenerateBlock(new Vector3Int(x, y, z));
                }
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();


        if (meshFilter == null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }
        
        meshFilter.mesh = mesh;
        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();
    }

    private int GetBlock(Vector3Int blockPosition)
    {
        if (blockPosition.x < 0 || blockPosition.x >= ChunkSize.Width)
        {
            return 0;
        }
        
        if (blockPosition.y < 0 || blockPosition.y >= ChunkSize.Height)
        {
            return 0;
        }
        
        if (blockPosition.z < 0 || blockPosition.z >= ChunkSize.Height)
        {
            return 0;
        }
        
        return blocks[blockPosition.x, blockPosition.y, blockPosition.z];
    }

    private void GenerateBlock(Vector3Int blockPosition)
    {
        if (BlockSideParameters.IsLeftSide)
        {
            var block = GetBlock(blockPosition + Vector3Int.left);

            if (block == 0)
            {
                AddLeftSide(blockPosition);
            }
        }
        
        if (BlockSideParameters.IsRightSide)
        {
            var block = GetBlock(blockPosition + Vector3Int.right);

            if (block == 0)
            {
                AddRightSide(blockPosition);
            }
        }

        if (BlockSideParameters.IsFrontSide)
        {
            var block = GetBlock(blockPosition + Vector3Int.forward);

            if (block == 0)
            {
                AddFrontSide(blockPosition);
            }
        }       
        
        if (BlockSideParameters.IsBackSide)
        {
            var block = GetBlock(blockPosition + Vector3Int.back);

            if (block == 0)
            {
                AddBackSide(blockPosition);
            }
        }

        if (BlockSideParameters.IsBottomSide)
        {
            var block = GetBlock(blockPosition + Vector3Int.down);

            if (block == 0)
            {
                AddBottomSide(blockPosition);
            }
        }        
        
        if (BlockSideParameters.IsTopSide)
        {
            var block = GetBlock(blockPosition + Vector3Int.up);

            if (block == 0)
            {
                AddTopSide(blockPosition);
            }
        }
    }

    private void AddLeftSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        vertices.Add(new Vector3(0, 1, 1) + blockPosition);

        SetOutTriangles();
    }

    private void AddRightSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        vertices.Add(new Vector3(1, 1, 0) + blockPosition);
        vertices.Add(new Vector3(1, 0, 1) + blockPosition);
        vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        SetInTriangles();
    }
    
    private void AddBackSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        vertices.Add(new Vector3(1, 1, 0) + blockPosition);

        SetInTriangles();
    }
    
    private void AddFrontSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        vertices.Add(new Vector3(0, 1, 1) + blockPosition);
        vertices.Add(new Vector3(1, 0, 1) + blockPosition);
        vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        SetOutTriangles();
    }
    
    private void AddBottomSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        vertices.Add(new Vector3(1, 0, 1) + blockPosition);

        SetOutTriangles();
    }
    
    private void AddTopSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        vertices.Add(new Vector3(0, 1, 1) + blockPosition);
        vertices.Add(new Vector3(1, 1, 0) + blockPosition);
        vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        SetInTriangles();
    }

    private void SetOutTriangles()
    {
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 3);

        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);
    }
    
    private void SetInTriangles()
    {
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);

        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 1);
        triangles.Add(vertices.Count - 2);
    }
}
