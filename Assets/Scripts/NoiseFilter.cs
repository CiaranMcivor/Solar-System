using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter
{
    Noise noise = new Noise();
    NoiseSettings settings;

    public NoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        for(int i = 0; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.centre);
            noiseValue += (v + 1) * 0.5f * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.amplitude;
        }

        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue *settings.strength;
    }
}
