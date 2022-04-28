using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSettings shapeSettings;
    NoiseFilter[] noiseFilters;
    public ShapeGenerator(ShapeSettings shapeSettings)
    {
        this.shapeSettings = shapeSettings;
        noiseFilters = new NoiseFilter[shapeSettings.noiseLayers.Length];
        for(int i = 0; i< noiseFilters.Length; i++)
        {
            noiseFilters[i] = new NoiseFilter(shapeSettings.noiseLayers[i].noiseSettings);
        }
    }


    public Vector3 calcPointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float elevation = 0;
        float firstLayer = 0;
        if(noiseFilters.Length > 0)
        {
            firstLayer = noiseFilters[0].evaluate(pointOnUnitSphere);
            if (shapeSettings.noiseLayers[0].enabled)
            {
                elevation = firstLayer;
            }

        }

        for(int i = 1; i < noiseFilters.Length; i++)
        {
            if (shapeSettings.noiseLayers[i].enabled)
            {
                float mask = (shapeSettings.noiseLayers[i].useFirstLayerAsMask ? firstLayer : 1);
                elevation += noiseFilters[i].evaluate(pointOnUnitSphere) * mask;

            }
        }
        return pointOnUnitSphere * shapeSettings.radius * (1 + elevation);
    }
}
