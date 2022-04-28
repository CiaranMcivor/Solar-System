using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public int numLayers = 4;
    public float amplitude = .5f;
    public float baseRoughness = 1;
    public float strength = 1;
    public float roughness = 2;
    public Vector3 centre;
    public float minValue = 1;
}
