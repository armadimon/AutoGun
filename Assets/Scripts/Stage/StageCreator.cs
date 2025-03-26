using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreator : MonoBehaviour
{
    public int stageWidth, stageLength;
    public int roomWidthMin, roomLengthMin;
    public int maxIterations;
    public int corridorWidth;
    public Material material;
    private void Start()
    {
        CreateStage();
    }

    private void CreateStage()
    {
        StageGenerator generator = new StageGenerator(stageWidth, stageLength);
        var listOfRooms = generator.CalculateRooms(maxIterations, roomWidthMin, roomLengthMin);

        for (int i = 0; i < listOfRooms.Count; i++)
        {
            CreateMesh(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner);
        }
    }

    private void CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        if (bottomLeftCorner.x > topRightCorner.x || bottomLeftCorner.y > topRightCorner.y)
        {
            Debug.LogError("bottomLeftCorner가 topRightCorner보다 큰 값이 포함됨! 좌표를 확인하세요.");
        }
        Debug.Log(bottomLeftCorner);
        Debug.Log(topRightCorner);
        Vector3 bottomLeftV = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRightV = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeftV = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRightV = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        Vector3[] vertices = new Vector3[]
        {
            topLeftV,
            topRightV,
            bottomLeftV,
            bottomRightV,
        };

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        int[] triangles = new int[]
        {
            0, 1, 2,  // 첫 번째 삼각형
            2, 1, 3   // 두 번째 삼각형
        };

        Mesh mesh = new Mesh();
        mesh.name = "StageMesh";
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        GameObject stageFloor = new GameObject("Mesh" + bottomLeftCorner, typeof(MeshFilter), typeof(MeshRenderer));

        stageFloor.transform.position = Vector3.zero;
        stageFloor.transform.localScale = Vector3.one;
        stageFloor.GetComponent<MeshFilter>().mesh = mesh;
        stageFloor.GetComponent<MeshRenderer>().material = material;

        // 디버깅 로그 추가
        Debug.Log($"Mesh '{mesh.name}' created with {vertices.Length} vertices and {triangles.Length / 3} triangles.");
        for (int i = 0; i < vertices.Length; i++)
        {
            Debug.Log($"Vertex {i}: {vertices[i]}");
        }
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Debug.Log($"Triangle {i / 3}: {triangles[i]}, {triangles[i + 1]}, {triangles[i + 2]}");
        }
    }

}