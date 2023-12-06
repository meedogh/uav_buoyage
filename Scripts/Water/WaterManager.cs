using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaterManager : MonoBehaviour
{
    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;
        //float randomness = 0.1f; // Adjust the amount of randomness

        for (int i = 0; i < vertices.Length; i++)
        {
            float x = transform.position.x + vertices[i].x;
            float z = transform.position.z + vertices[i].z;

            vertices[i].y = WaveManager.instance.GetWaveHeight(x, z);
        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();
    }
}
