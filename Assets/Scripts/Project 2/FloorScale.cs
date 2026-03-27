using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.LowLevelPhysics;

public class FloorScale : MonoBehaviour
{
    private MeshFilter _filter;
    public int sizeX = 3;
    public int sizeY = 3;
    public int gridSize = 3;
    private void Awake()
    {
        TryGetComponent(out _filter);
    }
    private void Start()
    {
        if (_filter)
        {
            Plane plane = new Plane(_filter.mesh, sizeX, sizeY, gridSize);
        }
    }
}
public abstract class ProceduralShape
{
    protected Mesh mesh_;
    protected Vector3[] vertices;
    protected int[] triangles;
    protected Vector2[] uvs;

    public ProceduralShape(Mesh mesh)
    {
        mesh_ = mesh;
    }
}
public class Plane : ProceduralShape
{
    private int sizeX_;
    private int sizeY_;
    private int gridSize_;

    public Plane(Mesh mesh, int sizeX, int sizeY, int gridSize) : base(mesh)
    {
        sizeX_ = sizeX;
        sizeY_ = sizeY;
        gridSize_ = gridSize;

        CreateMesh();
    }
    private void CreateMesh()
    {
        CreateVertices();
        CreateTriangles();
        CreateUVs();

        mesh_.vertices = vertices;
        mesh_.triangles = triangles;
        mesh_.uv = uvs;
        mesh_.RecalculateNormals();

    }
    private void CreateVertices()
    {
        vertices = new Vector3[sizeX_ * sizeY_];

        for (int y = 0; y < sizeY_; y++)
        {
            for (int x = 0; x < sizeX_; x++)
            {
                vertices[x + y * sizeX_] = new Vector3((x - 0.5f )* gridSize_, 0.0f, (y -0.5f) * gridSize_);
            }
        }
    }
    private void CreateTriangles()
    {
        triangles = new int[3 * 2 * (sizeX_ * sizeY_ - sizeY_ + 1)];
        int triangleVertexCount = 0;
        for (int vertex = 0; vertex < sizeX_ * sizeY_ - sizeX_; vertex++)
        {
            if (vertex % sizeX_ != (sizeX_ - 1))
            {
                //first triange
                int A = vertex;
                int B = A + sizeX_;
                int C = B + 1;
                triangles[triangleVertexCount] = A;
                triangles[triangleVertexCount + 1] = B;
                triangles[triangleVertexCount + 2] = C;
                //secondTriangle
                B += 1;
                C = A + 1;
                triangles[triangleVertexCount + 3] = A;
                triangles[triangleVertexCount + 4] = B;
                triangles[triangleVertexCount + 5] = C;
                triangleVertexCount += 6;
            }
        }
    }
    private void CreateUVs()
    {
        uvs = new Vector2[sizeX_ * sizeY_];
        int uvIndexCount = 0;

        foreach (Vector3 vertex in vertices)
        {
            uvs[uvIndexCount] = new Vector2(vertex.x * gridSize_, vertex.y * gridSize_ );
            uvIndexCount++;

        }
    }
}
