using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace : MonoBehaviour
{
    ShapeGenerator shapeGenerator;
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA, axisB;
    // Start is called before the first frame update
    public TerrainFace(ShapeGenerator shapeGenerator,Mesh mesh,int resolution,Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        this.shapeGenerator = shapeGenerator;
        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp,axisA);
    }

    public void constructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int index = 0;
        for(int y = 0; y < resolution; y++)
        {
            for(int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube =localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = shapeGenerator.calcPointOnPlanet(pointOnUnitSphere);

                if(x != resolution-1 && y != resolution - 1)
                {
                    triangles[index] = i;
                    triangles[index + 1] = i + resolution + 1;
                    triangles[index + 2] = i + resolution;

                    triangles[index + 3] = i;
                    triangles[index + 4] = i + 1;
                    triangles[index + 5] = i + resolution + 1;
                    index += 6;

                }
            }
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
    }
}
