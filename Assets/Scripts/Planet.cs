using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    [SerializeField] private ShapeSettings shapeSettings;
    [SerializeField] private ColourSettings colourSettings;
    ShapeGenerator shapeGenerator;
    public bool autoUpdate = true;
    [Range(10,256)]
    [SerializeField] private int resolution = 10;
    [SerializeField,HideInInspector]
    MeshFilter[] meshes;
    TerrainFace[] faces;

    private void OnValidate()
    {
        init();
        generatePlanet();
    }

    void init()
    {


        shapeGenerator = new ShapeGenerator(shapeSettings);

        if (meshes == null || meshes.Length == 0)
        {
            meshes = new MeshFilter[6];
        }

        faces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right,Vector3.forward,Vector3.back };

        for (int i = 0; i < meshes.Length; i++)
        {
            if(meshes[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshes[i] = meshObj.AddComponent<MeshFilter>();
                meshes[i].sharedMesh = new Mesh();

            }
            faces[i] = new TerrainFace(shapeGenerator,meshes[i].sharedMesh,resolution,directions[i]);

        }
    }

    public void generatePlanet()
    {
        init();
        generateMesh();
        generateColours();
    }

    public void onShapeUpdated()
    {
        if (autoUpdate)
        {
            init();
            generateMesh();
        }

    }

    public void onColoursUpdated()
    {
        if (autoUpdate)
        {
            init();
            generateColours();
        }

    }

    void generateMesh()
    {
        foreach (TerrainFace face in faces)
        {
            face.constructMesh();
        }
    }

    void generateColours()
    {
        foreach(MeshFilter m in meshes)
        {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colourSettings.color;
        }
    }

    public ColourSettings GetColourSettings()
    {
        return colourSettings;
    }

    public ShapeSettings GetShapeSettings()
    {
        return shapeSettings;
    }
}
