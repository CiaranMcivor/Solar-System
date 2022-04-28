using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    CelestialBody[] bodies;
    private void Awake()
    {
        bodies = FindObjectsOfType<CelestialBody>();
        Time.fixedDeltaTime *= 0.01f;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            Vector3 acceleration = CalculateAcceleration(bodies[i].getPosition(), bodies[i]);
            bodies[i].UpdateVelocity(acceleration, Universe.physicsTimeStep);
        }
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdatePosition(Universe.physicsTimeStep);
        }
    }

    public Vector3 CalculateAcceleration(Vector3 point, CelestialBody ignoreBody = null)
    {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in bodies)
        {
            if (body != ignoreBody)
            {
                float sqrDst = (body.getPosition() - point).sqrMagnitude;
                Vector3 forceDir = (body.getPosition() - point).normalized;
                acceleration += forceDir * Universe.G * body.getMass() / sqrDst;
            }
        }
        return acceleration;
    }
}
